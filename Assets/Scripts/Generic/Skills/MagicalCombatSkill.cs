//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

public class MagicalCombatSkill : CombatSkill
{
	public MagicalCombatSkill(CombatSkillConfig config, int level) : base (config, level) {
	}

	public MagicalCombatSkillConfig magicalCombatSkillConfig {
		get { return (MagicalCombatSkillConfig) mSkillConfig; } 
	}

	public float this[MagicalOffensiveModifierType type] {
		get { return magicalCombatSkillConfig[type]; }
	}
}

