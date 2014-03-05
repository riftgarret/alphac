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

public class MagicAttackOperation : AbstractCombatOperation
{
	private BattleEntity mSrcEntity;
	private BattleEntity mDestEntity;
	private DamageType mDamageType;

	public MagicAttackOperation (BattleEntity src, BattleEntity dest, BattleActionMagical action, DamageType damageType)
	{
		this.mSrcEntity = src;
		this.mDestEntity = dest;
		this.mDamageType = damageType;
	}

	public override IBattleEvent Execute (CombatResolver srcResolver, CombatResolver destResolver)
	{
		// check dodge before anything
		float resistValue = destResolver.GetResist(mDamageType); 
		float power = srcResolver.GetMagicalPower ();
		float chanceToResist = power / (power + resistValue);
		
		// TODO add chanceToHit increase
		if(UnityEngine.Random.Range(0f, 1f) > chanceToResist) {
			// missed
			return new ResistEvent(mSrcEntity, mDestEntity);
		}
		
		float dmg = srcResolver.GetMagicalDamage();

		// TODO: 
		// Move 
		
		if(dmg == 0) {
			return new NonDamageEvent(mSrcEntity, mDestEntity); // no reason to do additional calculations, no damage applied
		}
		
		// Calculate damage
		float damageSum = UnityEngine.Random.Range(dmg * 0.8f, dmg * 1.2f); // tmp
		
		// calculate crit chance
		float srcCritChance = srcResolver.GetCritChance ();
		float destCritDefense = destResolver.GetCritDefense ();
		float critChance = srcCritChance / (srcCritChance + destCritDefense); 

		float critDamage = 0f;
		// TODO factor in other chances
		if(UnityEngine.Random.Range(0f, 1f) <= critChance) {
			// lets separate crit damage from normal for sake of event
			critDamage = Mathf.Ceil(UnityEngine.Random.Range(CRIT_MULTIPLIER_LOW, CRIT_MULTIPLIER_HIGH)); // crit
		}
		
		
		// now calculate damage reduction from opponent
		// TODO override dmg type if special attack?

		
		// result damage should be same type of calculation
		damageSum = damageSum * damageSum / (damageSum + resistValue);
		damageSum = Mathf.Ceil(damageSum);

		float totalDamage = damageSum + critDamage;

		ExecuteDamage (totalDamage, mDestEntity);
		// return damage event
		return new DamageEvent(mSrcEntity, mDestEntity, damageSum, critDamage, mDamageType);
	}
}

