using UnityEngine;
using System.Collections;

public class HotKey {
	public Skill skill {
		get;
		private set;
	}

	public HotKey() {}
	public HotKey(Skill skill) {
		this.skill = skill;
	}

}
