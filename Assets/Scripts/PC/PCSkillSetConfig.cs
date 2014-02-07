using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// This is mostly a test script to setup parties, will not be used in game
/// </summary>
[Serializable]
public class PCSkillSetConfig : ScriptableObject {

	[SerializeField]
	private SkillConstructor [] skills;

	/// <summary>
	/// populate the skills
	/// </summary>
	/// <param name="skillset">Skillset.</param>
	public void InitSkills(PCSkillSet skillset) {
		skillset.skills = new CombatSkill[skills.Length];
		skillset.hotKeys = new HotKey[skills.Length];

		for(int i=0; i < skills.Length; i++) {
			skillset.skills[i] = new CombatSkill();
			skillset.skills[i].skillConfig = skills[i].skill;
			skillset.skills[i].level = skills[i].level;


			skillset.hotKeys[i] = new HotKey();
			if(skillset.skills[i].level > 0) {
				skillset.hotKeys[i].skill = skillset.skills[i];
			}
		}
	}


	[Serializable]
	public class SkillConstructor {
		public CombatSkillConfig skill;
		public int level;
	}
}