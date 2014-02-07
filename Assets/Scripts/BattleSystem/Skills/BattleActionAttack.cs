using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleAction {
	
	public BattleActionAttack(CombatSkill skill, BattleEntity source, SelectableTarget target) : base(skill, source, target) {

	}

	public override void OnExecuteAction (float actionClock)
	{	
		if(actionClock >= timeAction) {
			Damage damage = new Damage();
			damage.slashDamage = sourceEntity.character.physicalAttack;
			target.targets[0].TakeDamage(damage);
		}	
	}
}
