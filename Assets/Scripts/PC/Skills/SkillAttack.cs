using UnityEngine;
using System.Collections;

public class SkillAttack : Skill {

	public SkillAttack() :base("ATK") {
	}

	public override BattleAction CreateAction ()
	{
		return new BattleActionAttack(4);
	}
}
