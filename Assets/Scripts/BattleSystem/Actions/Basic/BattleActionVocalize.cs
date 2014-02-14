using UnityEngine;
using System.Collections;

public class BattleActionVocalize : BattleActionPositive {
	int mAttackCount = 0;

	public BattleActionVocalize(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			IStatusEffect [] statusEffects = new IStatusEffect[]{new StatusEffectPrickedThroat(10, 9)};
			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				eventManager.GeneratePositiveEvent(sourceEntity, entity, this, null);
			}
			mAttackCount++;
		}	
	}
}
