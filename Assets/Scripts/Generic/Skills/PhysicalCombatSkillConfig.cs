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
using UnityEngine;
using System.Collections.Generic;

public class PhysicalCombatSkillConfig : CombatSkillConfig
{
	[SerializeField]
	private PhysicalOffensiveModifier [] mOffensiveModifiers = null;
	public PhysicalOffensiveModifier [] offensiveModifiers { get { return mOffensiveModifiers; } }

	public override CombatSkillType combatSkillType {
		get {
			return CombatSkillType.PHYSICAL;
		}
	}

	public override CombatSkill CreateCombatSkill (int level)
	{
		return new PhysicalCombatSkill(this, level);
	}

	void OnEnable() {
		mModifierMap = new Dictionary<PhysicalOffensiveModifierType, float>();
		foreach(PhysicalOffensiveModifier mod in mOffensiveModifiers) {
			mModifierMap[mod.type] = mod.modValue;
		}
	}
	
	private Dictionary<PhysicalOffensiveModifierType, float> mModifierMap;
	
	
	public float this[PhysicalOffensiveModifierType type] {
		get { 
			if(mModifierMap.ContainsKey(type)) {
				return mModifierMap[type]; 
			}
			return 0f;
		}
	}
}

