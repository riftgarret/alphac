using UnityEngine;
using System.Collections;

public class SkillAttack : Skill, LevelableSkill {

	private static readonly float [] STR_MOD = {1f, 1.5f, 3f};


	public SkillAttack() :base("ATK", TargetingType.SINGLE, TargetStart.ENEMY) {
	}

	public override BattleAction CreateAction (BattleEntity origin)
	{
		return new BattleActionAttack(10f * STR_MOD[level] );
	}
	
	private float strMod(BattleEntity entity) {
		return STR_MOD[level] * entity.character.strength;
	}

	public int level {
		get;
		private set;
	}
}
