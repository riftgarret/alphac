using UnityEngine;
using System.Collections;

public class BattleActionVocalize : BattleActionPositive {
	int mAttackCount = 0;

	public BattleActionVocalize(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				BattleEventStatusEffects options = BattleEventStatusEffects.Builder()
					.AddStatusEffect(new StatusEffectVocalize(1,1), entity, StatusEffectEvent.StatusEffectRule.ALWAYS )
						.Build();
				eventManager.GeneratePositiveEvent(sourceEntity, entity, this, null);
			}
			mAttackCount++;
		}	
	}
}
