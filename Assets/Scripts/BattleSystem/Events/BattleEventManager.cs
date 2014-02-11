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
	/// Does the attack. 
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="action">Action.</param>
	/// <param name="modifiers">Modifiers.</param>
	public void GenerateAttackEvent(BattleEntity src, BattleEntity dest, BattleAction action, OffensiveModifier [] modifiers) {
		BattleEventAttack attackEvent = new BattleEventAttack(src, dest, action, modifiers);
		ApplyAndNotifyDamageEvent(attackEvent);
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
