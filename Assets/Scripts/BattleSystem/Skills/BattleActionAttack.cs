using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleAction {
	
	public BattleActionAttack(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock)
	{	
		if(actionClock >= timeAction) {
			Damage damage = new Damage();
			damage.slashDamage = sourceEntity.character.physicalAttack;
			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {
				entity.TakeDamage(damage, sourceEntity);
			}
		}	
	}
}
