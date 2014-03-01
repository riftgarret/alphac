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
using System.Collections.Generic;
using UnityEngine;

public class BattleEventMagicAttack : AbstractBattleDamageEvent
{
	private const float CRIT_MULTIPLIER_LOW = 1.5f;
	private const float CRIT_MULTIPLIER_HIGH = 1.8f;
	
	private BattleEntity mSrcEntity;
	private BattleEntity mDestEntity;

	private DamageType mDamageType;
	private bool mHasDamage;
	private bool mIsResisted;
	private bool mIsCrit;
	private float mTotalDamage;
	
	public BattleEventMagicAttack (BattleEntity src, BattleEntity dest, BattleActionMagical action, DamageType damageType, IOffensiveMagicalCombatNode combatNode)
	{
		this.mSrcEntity = src;
		this.mDestEntity = dest;
		this.mDamageType = damageType;
		
		mIsResisted = false;
		mIsCrit = false;

		// check dodge before anything
		float defResistValue = dest.GetResist(damageType);
		float power = combatNode.powerMagicalAdd * combatNode.powerMagicalMultiply;
		float chanceToResist = power / (power + defResistValue);
		
		// TODO add chanceToHit increase
		if(UnityEngine.Random.Range(0f, 1f) > chanceToResist) {
			// missed
			mIsResisted = true;
			return;
		}

		float dmg = combatNode.totalDamageAdd;
		dmg *= combatNode.totalDamageMultiply;

		if(dmg == 0) {
			mHasDamage = false;
			return; // no reason to do additional calculations, no damage applied
		}

		// Calculate damage
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
		// TODO override dmg type if special attack?
		float resistValue = dest.GetResist(damageType);
		
		// result damage should be same type of calculation
		mTotalDamage = damageSum * damageSum / (damageSum + resistValue);
		mTotalDamage = Mathf.Ceil(mTotalDamage);

		/*




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
		*/

		

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
	
	public bool isResisted {
		get {
			return mIsResisted;
		}
	}
	
	public bool isCrit {
		get {
			return mIsCrit;
		}
	}
	
	public override string eventText {
		get {
			if(mIsResisted) {
				return string.Format("{0} resisted {1}", mSrcEntity.character.displayName, mDestEntity.character.displayName);
			}
			return "temp";
			//string format = "{0} scored a {5} hit against {1} with {2} {3} damage, HP: {4}";
			//return string.Format(format, mSrcEntity.character.displayName, mDestEntity.character.displayName, TextUtils.DmgToString(mDmgType), mTotalDamage, mDestEntity.character.curHP, (mIsCrit? "critical " : ""));
		}
	}

	public override void Execute ()
	{
		ExecuteDamage ();
	}
}

