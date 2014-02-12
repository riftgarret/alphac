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
	public void GenerateAttackEvent(BattleEntity src, BattleEntity dest, 
	                                BattleAction action, 
	                                BattleEventOptions options) {
		BattleEventAttack attackEvent = new BattleEventAttack(src, dest, action, options.offensiveModifiers);
		ApplyAndNotifyDamageEvent(attackEvent);

		// check to see if we hit for any status effects
		if(options.destStatusEffects != null && !attackEvent.isEvaded) {
			foreach(IStatusEffect effect in options.destStatusEffects) {
				dest.ApplyStatusEffect(effect);
			}
		}

		// check to see if we hit for any status effects
		if(options.srcStatusEffects != null && !attackEvent.isEvaded) {
			foreach(IStatusEffect effect in options.srcStatusEffects) {
				src.ApplyStatusEffect(effect);
			}
		}
	}

	public void GenerateMagicEvent(BattleEntity src, BattleEntity dest, 
	                               BattleAction action, 
	                               BattleEventOptions options) {

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
	private void ApplyAndNotifyDamageEvent(IBattleDamageEvent dmgEvent) {
		BattleEntity destEntity = dmgEvent.destEntity;
		if(destEntity.character.curHP <= 0) {
			return; // character is dead, no need to add more death
		}
		destEntity.character.curHP -= dmgEvent.totalDamage;
		// notify attack event
		NotifyEvent(dmgEvent);

		// if character died, notify death event
		if(destEntity.character.curHP <= 0) {
			destEntity.character.curHP = 0;
			BattleEventDeath deathEvent = new BattleEventDeath(destEntity);
			NotifyEvent(deathEvent);
		}

	}		
}