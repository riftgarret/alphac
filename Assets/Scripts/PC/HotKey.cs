using UnityEngine;
using System.Collections;

public class HotKey {
	public CombatSkill skill {
		get;
		set;
	}

	public HotKey() {}
	public HotKey(CombatSkill skill) {
		this.skill = skill;
	}

}
