using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleActionPhysical {
	int mAttackCount = 0;

	public BattleActionAttack(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {
			DamageTypeModifier damageTypeModifier = this.sourceEntity.GetWeaponDamageTypeModifier(0);

			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				eventManager.GeneratePhysicalEvent(sourceEntity, entity, this, BattleEventStatusEffects.EMPTY, damageTypeModifier, null);
			}
			mAttackCount++;
		}	
	}
}
