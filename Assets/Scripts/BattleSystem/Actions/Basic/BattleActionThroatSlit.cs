using UnityEngine;
using System.Collections;

public class BattleActionThroatSlit : BattleActionPhysical {
	int mAttackCount = 0;

	public BattleActionThroatSlit(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock, BattleEventManager eventManager)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			DamageType damageType = GetWeaponDamageType(0);

			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				CombatStatusEffectList options = CombatStatusEffectList.Builder()
					.AddStatusEffect(new StatusEffectPrickedThroat(10,9), entity, CombatStatusEffect.StatusEffectRule.ON_HIT)
						.Build();
				eventManager.GeneratePhysicalEvent(sourceEntity, entity,  this, options, damageType, null);
			}
			mAttackCount++;
		}	
	}
}
