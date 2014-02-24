//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

public class BattleEventPhysical : AbstractBattleDamageEvent
{
	private const float CRIT_MULTIPLIER_LOW = 1.5f;
	private const float CRIT_MULTIPLIER_HIGH = 1.8f;

	private BattleEntity mSrcEntity;
	private BattleEntity mDestEntity;

	private List<DamageNode> mDamageNodes;
	private DamageType mDmgType;
	private bool mIsEvaded;
	private bool mIsCrit;
	private float mTotalDamage;

	public BattleEventPhysical (BattleEntity src, 
	                            BattleEntity dest, 
	                            BattleActionPhysical action, 
	                            DamageTypeModifier damageTypeModifier, 
	                            IOffensivePhysicalCombatNode combatNode)
	{
		this.mSrcEntity = src;
		this.mDestEntity = dest;

		mIsEvaded = false;
		mIsCrit = false;

		// should be done first to popualte into from auxilary methods
		mDamageNodes = new List<DamageNode>();

		// check dodge before anything
		float srcChanceToHit = combatNode.accuracyAdd * combatNode.accuracyMultiply;
		float chanceToHit = srcChanceToHit / (srcChanceToHit + dest.relfex);

		// TODO add chanceToHit increase
		if(UnityEngine.Random.Range(0f, 1f) > chanceToHit) {
			// missed
			mIsEvaded = true;
			return;
		}

		// Calculate damage
		mDmgType = damageTypeModifier.type;
						
		// TODO set base damage in damage node or use total damage
		float dmg = combatNode.totalDamageAdd;
		dmg *= combatNode.totalDamageMultiply;

		float rolledDmg = UnityEngine.Random.Range(dmg * 0.8f, dmg * 1.2f); // tmp

		float damageSum = rolledDmg;
		// calculate stat modifiers

		// not very straight forward here but the stat Add will be the default stat, and the multiply will be the modifier
		damageSum += combatNode.statSTRAdd * combatNode.statSTRMultiply;
		damageSum += combatNode.statVITAdd * combatNode.statVITMultiply;
		damageSum += combatNode.statDEXAdd * combatNode.statDEXMultiply;
		damageSum += combatNode.statAGIAdd * combatNode.statAGIMultiply;
		damageSum += combatNode.statINTAdd * combatNode.statINTMultiply;
		damageSum += combatNode.statWISAdd * combatNode.statWISMultiply;
		damageSum += combatNode.statLUCKAdd * combatNode.statLUCKMultiply;

		// calculate crit chance
		float srcCritChance = combatNode.critChanceAdd * combatNode.critChanceMultiply;
		float critChance = srcCritChance / (srcCritChance + dest.critDefense);
		// TODO factor in other chances
		if(UnityEngine.Random.Range(0f, 1f) <= critChance) {
			damageSum *= UnityEngine.Random.Range(CRIT_MULTIPLIER_LOW, CRIT_MULTIPLIER_HIGH); // crit
			mIsCrit = true;
		}

		// now calculate damage reduction from opponent
		// TODO override dmg type if special attack
		float resistValue = dest.GetResist(mDmgType);

		// result damage should be same type of calculation
		mTotalDamage = damageSum * damageSum / (damageSum + resistValue);
		mTotalDamage = Mathf.Ceil(mTotalDamage);
	}

	/*
	 * TOTAL_DMG,
	CRIT_MOD,
	CRIT_TOTAL,
	ARMOR_IGNORE,
	DODGE_IGNORE,
	STR_MOD,
	AGI_MOD,
	DEX_MOD,
	INT_MOD,
	WIS_MOD
	 */

	/// <summary>
	/// Calculates the pre damage. this is so we can break out each group up
	/// </summary>
	/// <returns>The pre damage.</returns>
	/// <param name="srcType">Source type.</param>
	/// <param name="initialDamage">Initial damage.</param>
	/// <param name="statModifiers">Stat modifiers.</param>
	/// <param name="c">C.</param>
	private DamageNode CalculatePreDamage(OffensiveSourceType srcType, float initialDamage, StatModifier[] modifiers, BattleEntity entity) {
		// if no modifiers, lets return null to skip in  our log
		if(modifiers == null) {
			return null;
		}

		float moddedDmg = 0;
		foreach(StatModifier mod in modifiers) {
			moddedDmg += entity.GetStat(mod.stat) * mod.mod;
		}

		// if we are still 0, return null so we don't include it in our log
		if(moddedDmg == 0) {
			return null;
		}

		/// create and add it to our list
		DamageNode node = new DamageNode(initialDamage * moddedDmg, srcType);
		mDamageNodes.Add(node);

		return node;
	}

	public class DamageNode {
		public readonly float calculatedDamage;
		public readonly OffensiveSourceType srcType;

		public DamageNode(float dmg, OffensiveSourceType type) {
			this.calculatedDamage = dmg;
			this.srcType = type;
		}
	}

	public override BattleEntity srcEntity {
		get {
			return mSrcEntity;
		}
	}

	public override BattleEntity destEntity {
		get { return mDestEntity; }
	}

	public override BattleEventType eventType {
		get {
			return BattleEventType.ATTACK;
		}
	}

	public override float totalDamage {
		get {
			return mTotalDamage;
		}
	}

	public bool isEvaded {
		get {
			return mIsEvaded;
		}
	}

	public bool isCrit {
		get {
			return mIsCrit;
		}
	}

	public override string eventText {
		get {
			if(mIsEvaded) {
				return string.Format("{0} missed {1}", mSrcEntity.character.displayName, mDestEntity.character.displayName);
			}
			string format = "{0} scored a {5} hit against {1} with {2} {3} damage, HP: {4}";
			return string.Format(format, mSrcEntity.character.displayName, mDestEntity.character.displayName, TextUtils.DmgToString(mDmgType), mTotalDamage, mDestEntity.character.curHP, (mIsCrit? "critical " : ""));
		}
	}

	public override void Execute ()
	{
		ExecuteDamage ();
	}
}


