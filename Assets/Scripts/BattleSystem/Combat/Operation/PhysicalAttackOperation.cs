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

public class PhysicalAttackOperation : AbstractCombatOperation
{
	private BattleEntity mSrcEntity;
	private BattleEntity mDestEntity;
	private DamageType mDamageType;

	public PhysicalAttackOperation (BattleEntity src, 
	                            BattleEntity dest, 
	                            DamageType damageType)
	{
		this.mSrcEntity = src;
		this.mDestEntity = dest;
		mDamageType = damageType;
	}
	
	public override IBattleEvent Execute (CombatResolver srcResolver, CombatResolver destResolver)
	{
		// check dodge before anything
		float srcChanceToHit = srcResolver.GetAccuracy ();
		float reflex = destResolver.GetReflex();
		float chanceToHit = srcChanceToHit / (srcChanceToHit + reflex); 
		
		// TODO add chanceToHit increase
		// if we missed
		if(UnityEngine.Random.Range(0f, 1f) > chanceToHit) {
			return new DodgeEvent(mSrcEntity, mDestEntity);
		}
		
		// TODO set base damage in damage node or use total damage
		float dmg = srcResolver.GetPhysicalDamage ();
		
		float damageSum = UnityEngine.Random.Range(dmg * 0.8f, dmg * 1.2f); // tmp
		
		
		// calculate crit chance
		float srcCritChance = srcResolver.GetCritChance();
		float critDefense = destResolver.GetCritDefense();
		float critChance = srcCritChance / (srcCritChance + critDefense); 
		float critDamage = 0f;
		// TODO factor in other chances
		// Note: crit damage will not be resisted then
		if(UnityEngine.Random.Range(0f, 1f) <= critChance) {
			critDamage = Mathf.Ceil(UnityEngine.Random.Range(CRIT_MULTIPLIER_LOW, CRIT_MULTIPLIER_HIGH)); // crit
		}
		
		// now calculate damage reduction from opponent
		// TODO override dmg type if special attack
		float resistValue = destResolver.GetResist(mDamageType);
		
		// result damage should be same type of calculation
		damageSum = damageSum * damageSum / (damageSum + resistValue);
		damageSum = Mathf.Ceil(damageSum);

		float totalDamage = damageSum + critDamage;
		ExecuteDamage (totalDamage, mDestEntity);

		return new DamageEvent (mSrcEntity, mDestEntity, damageSum, critDamage, mDamageType);

	}
}


