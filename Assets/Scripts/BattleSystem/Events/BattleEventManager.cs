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

public class BattleEventManager
{
	public IBattleEventListener battleEventListener {
		get;
		set;
	}

	public BattleEventManager ()
	{
	}
	

	/// <summary>
	/// Generates the attack event. If the attack hits, it will assign all dest status effects to dest battle entity, and others
	/// to the src
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="action">Action.</param>
	/// <param name="modifiers">Modifiers.</param>
	/// <param name="srcPhysicalStatusEffects">Source physical status effects.</param>
	/// <param name="destPhysicalStatusEffects">Destination physical status effects.</param>
	public void GeneratePhysicalEvent(BattleEntity src, BattleEntity dest, 
	                                BattleActionPhysical action,                                   	
	                                BattleEventStatusEffects options,
	                                DamageType damageType,
	                                IOffensivePhysicalCombatNode physicalCombatNode) {
		BattleEventPhysical attackEvent = new BattleEventPhysical(src, dest, action, damageType, physicalCombatNode);
		NotifyEvent (attackEvent);
		PostDamageEvent(attackEvent);

		// check to see if we hit for any status effects
		//if(!attackEvent.isEvaded) {
		//	ApplyEffects(options.destStatusEffects, dest);
		//	ApplyEffects(options.srcStatusEffects, src);
		//}
	}

	public void GenerateMagicalEvent(BattleEntity src, BattleEntity dest, 
	                               BattleActionMagical action, 
	                               BattleEventStatusEffects options,
	                                 DamageType damageType,
	                                 IOffensiveMagicalCombatNode magicalCombatNode) {
		BattleEventMagicAttack magicEvent = new BattleEventMagicAttack (src, dest, action, damageType, magicalCombatNode);
		NotifyEvent(magicEvent);
		PostDamageEvent(magicEvent);
	}

	public void GeneratePositiveEvent(BattleEntity src, BattleEntity dest, 
	                                 BattleActionPositive action, 
	                                 BattleEventStatusEffects options) {
		
	}

	/// <summary>
	/// Applies the effects.
	/// </summary>
	/// <param name="effects">Effects.</param>
	/// <param name="targetEntity">Target entity.</param>
	private void ApplyEffects(IStatusEffect [] effects, BattleEntity targetEntity) {
		if(effects != null) {
			foreach(IStatusEffect effect in effects) {
				targetEntity.ApplyStatusEffect(effect);
			}
		}
	}

	/// <summary>
	/// Notifies the event.
	/// </summary>
	/// <param name="battleEvent">Battle event.</param>
	private void NotifyEvent(IBattleEvent battleEvent) {
		if(battleEventListener != null) {
			battleEventListener.OnBattleEvent(battleEvent);
		}
	}

	// calculate and apply damage state (see if they are dead or not)
	private void PostDamageEvent(IBattleDamageEvent dmgEvent) {
		BattleEntity destEntity = dmgEvent.destEntity;
		// if character died, notify death event
		if(destEntity.character.curHP <= 0) {
			destEntity.character.curHP = 0;
			BattleEventDeath deathEvent = new BattleEventDeath(destEntity);
			NotifyEvent(deathEvent);
		}

	}		
}
