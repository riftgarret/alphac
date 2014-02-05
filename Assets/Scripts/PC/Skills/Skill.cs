using UnityEngine;
using System.Collections;

public abstract class Skill {
	public Skill(string skillName) {
		this.skillName = skillName;
	}
	public readonly string skillName;
	public abstract BattleAction CreateAction();
}
