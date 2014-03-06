using UnityEngine;
using System.Collections;

public class BattleActionVocalize : BattleActionPositive {
	int mAttackCount = 0;

	public BattleActionVocalize(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				CombatStatusEffectList options = CombatStatusEffectList.Builder()
					.AddStatusEffect(new StatusEffectVocalize(1,1), entity, StatusEffectRule.StatusEffectHitPredicate.ALWAYS )
						.Build();
				BattleSystem.combatExecutor.ExecutePositive(sourceEntity, entity, this, options);
			}
			mAttackCount++;
		}	
	}
}
