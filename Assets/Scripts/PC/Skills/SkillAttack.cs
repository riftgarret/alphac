using UnityEngine;
using System.Collections;

public class SkillAttack : Skill {

	public SkillAttack() :base("ATK", TargetingType.SINGLE, TargetStart.ENEMY) {
	}

	public override BattleAction CreateAction ()
	{
		return new BattleActionAttack(4);
	}
}
