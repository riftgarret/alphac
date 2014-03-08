using UnityEngine;
using System.Collections;

public class BattleActionHoarse : BattleActionMagical {
	int mAttackCount = 0;

	public BattleActionHoarse(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			foreach(BattleEntity targetEntity in targetResolver.GetTargets(combatSkill)) {
				// create status effect for hoarse
				CombatStatusEffectList effects = CombatStatusEffectList.Builder()
					//.AddStatusEffect(new StatusEffectHoarse(1, 1), targetEntity, StatusEffectRule.StatusEffectHitPredicate.ON_HIT)
					.Build();


				// combat node should just be character, skill

				BattleSystem.combatExecutor.ExecuteMagicalAttack(sourceEntity, 
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
