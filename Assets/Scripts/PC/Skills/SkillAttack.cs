using UnityEngine;
using System.Collections;

public class SkillAttack : Skill {
	public override BattleAction CreateAction ()
	{
		return new BattleActionAttack(4);
	}
}
