using UnityEngine;
using System.Collections;

public class BattleActionThroatSlit : BattleAction {
	int mAttackCount = 0;

	public BattleActionThroatSlit(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {
			BattleEventOptions options = BattleEventOptions.Builder()
				.AddDestStatusEffect(new StatusEffectPrickedThroat(10,9))
					.Build();

			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				eventManager.GenerateAttackEvent(sourceEntity, entity, this, options);
			}
			mAttackCount++;
		}	
	}
}
