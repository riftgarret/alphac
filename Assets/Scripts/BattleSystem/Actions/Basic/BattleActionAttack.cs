using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleActionPhysical {
	int mAttackCount = 0;

	public BattleActionAttack(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {
			DamageType damageType = GetWeaponDamageType(0);


			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				BattleSystem.combatExecutor.ExecutePhysicalAttack(sourceEntity, entity, this, CombatStatusEffectList.EMPTY, damageType, null);
			}
			mAttackCount++;
		}	
	}
}
