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

[Serializable]
public abstract class Skill
{
	[SerializeField]
	protected int mLevel;

	public int level{ 
		get { return mLevel; }
		set { mLevel = value; }
	}	

	[SerializeField]
	protected SkillConfig mSkillConfig;

	public SkillConfig skillConfig { 
		get { return mSkillConfig; } 
		set { mSkillConfig = value; }
	} 
}