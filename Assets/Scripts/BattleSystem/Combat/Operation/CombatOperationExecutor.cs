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


public class CombatOperationExecutor
{
	public void Execute(BattleEntity src, BattleEntity dest, ICombatSkill combatSkill, CombatRound combatRound) {
		// figure out damage type
		Weapon equipedWeapon = src.equipedWeapons[combatRound.weaponIndex];
		if(equipedWeapon == null) {
			Debug.Log("No equiped weapon, aborting");
			return;
		}

		DamageType damageType = equipedWeapon.damageType;
		if(!combatRound.useWeaponDamageType) {
			damageType = combatRound.damageTypeOverride;
		}

		// get source combat node
		SkillCombatNode skillNode = new SkillCombatNode(combatSkill);
		skillNode.Load(combatRound);

		CombatResolver srcResolver = src.CreateCombatNodeBuilder()
			.SetSkillCombatNode(skillNode)
			.SetWeapon(equipedWeapon)
			.BuildResolver();


		switch(combatRound.operationType) {
		case CombatOperationType.MAGICAL_ATTACK:
			ExecuteMagicalAttack(src, dest, combatRound.statusEffectRules, damageType, srcResolver);
			break;
		case CombatOperationType.PHYSICAL_ATTACK:
			ExecutePhysicalAttack(src, dest, combatRound.statusEffectRules, damageType, srcResolver);
			break;
		case CombatOperationType.MAGICAL_DEBUFF:
			ExecuteMagicalAttack(src, dest, combatRound.statusEffectRules, damageType, srcResolver);
			break;
		case CombatOperationType.POSITIVE_BUFF:
			ExecutePositive(src, dest, combatRound.statusEffectRules);
			break;
		case CombatOperationType.POSITIVE_HEALING:
			ExecuteHealing(src, dest, combatRound.statusEffectRules, srcResolver);
			break;
		}
	}

	/// <summary>
	/// Executes the physical attack. This will apply the damage and execute the status effects according to the specified Rules.
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="statusList">Status list.</param>
	/// <param name="damageType">Damage type.</param>
	/// <param name="physicalCombatNode">Physical combat node.</param>
	public void ExecutePhysicalAttack(BattleEntity src, BattleEntity dest,                     	
	                                StatusEffectRule [] effectRules,
	                                DamageType damageType,
	                                CombatResolver srcResolver) {

		CombatResolver destResolver = new CombatResolver (dest);
		PhysicalAttackOperation attackOperation = new PhysicalAttackOperation(src, dest, damageType);

		ExecuteAttackOperation (attackOperation, srcResolver, destResolver, effectRules);
	}

	/// <summary>
	/// Executes the magical attack. This will apply the damage and execute the status effects according to the specified Rules.
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="statusList">Status list.</param>
	/// <param name="damageType">Damage type.</param>
	/// <param name="magicalCombatNode">Magical combat node.</param>
	public void ExecuteMagicalAttack(BattleEntity src, BattleEntity dest, 
	                                 StatusEffectRule [] effectRules,
	                                 DamageType damageType,
	                                 CombatResolver srcResolver) {
		// TODO, move src, dest to resolver type actions exposing raw battle entity
		// remove battle action from event
		// damage type ok
		CombatResolver destResolver = new CombatResolver (dest);
		MagicAttackOperation magicOperation = new MagicAttackOperation (src, dest, damageType);

		ExecuteAttackOperation (magicOperation, srcResolver, destResolver, effectRules);
	}

	/// <summary>
	/// Executes the healing. This will not be ignored and will execute all status effects
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="options">Options.</param>
	public void ExecuteHealing(BattleEntity src, BattleEntity dest, 
	                           StatusEffectRule [] effectRules,
	                           CombatResolver srcResolver) {
		CombatResolver destResolver = new CombatResolver (dest);
		HealingOperation healOperation = new HealingOperation (src, dest);

		// execute and do the healing
		IBattleEvent battleEvent = healOperation.Execute (srcResolver, destResolver);

		// notify resulting battle event
		BattleSystem.eventManager.NotifyEvent (battleEvent);

		// since we execute everything, lets just do whatever execute positive to finish the workflow
		ExecutePositive (src, dest, effectRules);
	}

	/// <summary>
	/// Executes the positive. No damage happens here, we just apply the status effects.
	/// </summary>
	/// <param name="src">Source.</param>
	/// <param name="dest">Destination.</param>
	/// <param name="action">Action.</param>
	/// <param name="statusList">Status list.</param>
	public void ExecutePositive(BattleEntity src, BattleEntity dest, 
	                            StatusEffectRule [] effectRules) {
		foreach (StatusEffectRule combatStatusEffect in effectRules) {
			ApplyEffect(combatStatusEffect, src);
		}
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
		IBattleEvent battleEvent = operation.Execute (srcResolver, destResolver);
		BattleEventType eventType = battleEvent.eventType;

		// notify resulting battle event
		BattleSystem.eventManager.NotifyEvent (battleEvent);

		// check to see if it was a damage event to see if we killed them
		if (eventType == BattleEventType.DAMAGE && wasAlive && destEntity.currentHP <= 0) {
			destEntity.character.curHP = 0;
			DeathEvent deathEvent = new DeathEvent(destEntity);
			BattleSystem.eventManager.NotifyEvent(deathEvent);		
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
