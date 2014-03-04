using UnityEngine;
using System.Collections;

public class BattleActionHoarse : BattleActionMagical {
	int mAttackCount = 0;

	public BattleActionHoarse(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			foreach(BattleEntity targetEntity in targetResolver.GetTargets(combatSkill)) {
				// create status effect for hoarse
				BattleEventStatusEffects effects = BattleEventStatusEffects.Builder()
					.AddStatusEffect(new StatusEffectHoarse(1, 1), targetEntity, StatusEffectEvent.StatusEffectRule.ON_HIT)
					.Build();


				// combat node should just be character, skill

				eventManager.GenerateMagicalEvent(sourceEntity, 
				                                  targetEntity, 
				                                  this, 
				                                  effects,
				                                  DamageType.DARK,
				                                  null);
			}
			mAttackCount++;
		}	
	}
}
