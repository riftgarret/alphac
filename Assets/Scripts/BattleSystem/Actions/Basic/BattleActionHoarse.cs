using UnityEngine;
using System.Collections;

public class BattleActionHoarse : BattleActionMagical {
	int mAttackCount = 0;

	public BattleActionHoarse(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {
			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				eventManager.GenerateMagicalEvent(sourceEntity, 
				                                  entity, 
				                                  this, 
				                                  BattleEventOptions.EMPTY,
				                                  new ElementResistModifier(ElementResistType.DARK, 1),
				                                  null);
			}
			mAttackCount++;
		}	
	}
}
