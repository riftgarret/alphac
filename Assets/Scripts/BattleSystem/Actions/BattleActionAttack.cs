using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleAction {
	int mAttackCount = 0;

	public BattleActionAttack(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {
			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				eventManager.GenerateAttackEvent(sourceEntity, entity, this, null);
			}
			mAttackCount++;
		}	
	}
}
