using UnityEngine;
using System.Collections;

public abstract class Skill {
	public Skill(string skillName, 
	             TargetingType primaryTargetType,
	             TargetStart initialTarget) {
		this.skillName = skillName;
		this.primaryTargetType = primaryTargetType;
		this.initialTarget = initialTarget;
	}

	public readonly TargetingType primaryTargetType;
	public readonly TargetStart initialTarget;

	public readonly string skillName;
	public abstract BattleAction CreateAction(BattleEntity origin);
}
