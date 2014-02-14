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
using System.Collections.Generic;
using UnityEngine;

public class PositiveCombatSkillConfig : CombatSkillConfig
{
	[SerializeField]
	private PositiveOffensiveModifier [] mOffensiveModifiers = null;
	public PositiveOffensiveModifier [] offensiveModifiers { get { return mOffensiveModifiers; } }

	public override CombatSkillType combatSkillType {
		get {
			return CombatSkillType.POSITIVE;
		}
	}

	public override CombatSkill CreateCombatSkill (int level)
	{
		return new PositiveCombatSkill(this, level);
	}

	void OnEnable() {
		mModifierMap = new Dictionary<PositiveOffensiveModifierType, float>();
		foreach(PositiveOffensiveModifier mod in mOffensiveModifiers) {
			mModifierMap[mod.type] = mod.modValue;
		}
	}
	
	private Dictionary<PositiveOffensiveModifierType, float> mModifierMap;
	
	
	public float this[PositiveOffensiveModifierType type] {
		get { 
			if(mModifierMap.ContainsKey(type)) {
				return mModifierMap[type]; 
			}
			return 0f;
		}
	}
}
