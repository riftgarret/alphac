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

public class PhysicalCombatSkill : CombatSkill
{
	public PhysicalCombatSkill(CombatSkillConfig config, int level) : base (config, level) {
	}

	public PhysicalCombatSkillConfig physicalCombatSkillConfig {
		get { return (PhysicalCombatSkillConfig) mSkillConfig; } 
	}

	public float this[PhysicalOffensiveModifierType type] {
		get { return physicalCombatSkillConfig[type]; }
	}
}
