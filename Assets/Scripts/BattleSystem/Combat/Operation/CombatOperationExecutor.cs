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


public class CombatOperationExecutor
{    

	public void Execute(ICombatOperation combatOperation) {
        // prepare list
        List<IBattleEvent> events = new List<IBattleEvent>();

        // execute
        combatOperation.Execute(events);

        // process events
        ProcessEvents(events);
	}

    private void ProcessEvents(List<IBattleEvent> events) {
        // do stuff with leftovers?
        List<IBattleEvent> processedEvents = new List<IBattleEvent>();
        foreach (IBattleEvent e in events) {
            PostEvent(e);
        }
    }        

    /// <summary>
    /// Post battle event
    /// </summary>
    /// <param name="e"></param>
    private void PostEvent(IBattleEvent e) {
        BattleSystem.Instance.PostBattleEvent(e);
    }



	/// <summary>
	/// Execute an operation and manage check resulting event with CombatStatusEffect Rules if any
	/// </summary>
	/// <param name="operation">Operation.</param>
	private void ExecuteAttackOperation(ICombatOperation operation, 
	                                    CombatResolver srcResolver, 
	                                    CombatResolver destResolver, 
	                                    StatusEffectRule [] effectRules) {
		// check to see if we were alive before executing the event
		BattleEntity destEntity = destResolver.entity;
		bool wasAlive = destEntity.currentHP > 0;

		// execute and apply damage
		IBattleEvent battleEvent = null;
		BattleEventType eventType = battleEvent.EventType;

		// notify resulting battle event
		BattleSystem.Instance.PostBattleEvent(battleEvent);

		// check to see if it was a damage event to see if we killed them
		if (eventType == BattleEventType.DAMAGE && wasAlive && destEntity.currentHP <= 0) {
			destEntity.character.curHP = 0;
			DeathEvent deathEvent = new DeathEvent(destEntity);
            BattleSystem.Instance.PostBattleEvent(deathEvent);		
		}

		// lets see if we hit the target or not
		bool hitTarget = eventType == BattleEventType.DAMAGE
						|| eventType == BattleEventType.NON_DAMAGE 
						|| eventType == BattleEventType.ITEM;

		bool missedTarget = eventType == BattleEventType.DODGE
						|| eventType == BattleEventType.RESIST;

		// iterate through combnat effects to see what should apply
		foreach (StatusEffectRule combatStatusEffect in effectRules) {
			switch(combatStatusEffect.rule) {			
			case StatusEffectRule.StatusEffectHitPredicate.ON_HIT:
				if(hitTarget) {
					ApplyEffect(combatStatusEffect, srcResolver.entity);
				}
				break;
			case StatusEffectRule.StatusEffectHitPredicate.ON_MISS:
				if(missedTarget) {
					ApplyEffect(combatStatusEffect, srcResolver.entity);
				}
				break;
			case StatusEffectRule.StatusEffectHitPredicate.ALWAYS:
			default:
				ApplyEffect(combatStatusEffect, srcResolver.entity);
				break;
			}
		}
		
	}

	/// <summary>
	/// Applies the effect. Notify the StatusEffect to the event manager
	/// </summary>
	/// <param name="effects">Effects.</param>
	/// <param name="targetEntity">Target entity.</param>
	private void ApplyEffect(StatusEffectRule combatEffect, BattleEntity srcEntity) {
		BattleEntity destEntity = combatEffect.target;
		IStatusEffectExecutor statusEffect = combatEffect.effect;
		// first directly apply the effect, it will notify the event from the StatusEffectManager
		destEntity.ApplyStatusEffect (statusEffect);
	}
}
