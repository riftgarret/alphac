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
using System.Collections;
using System.Collections.Generic;

public class TargetResolverSingle : ITargetResolver
{
	// only used when the target is single
	private BattleEntity [] mTargetEntity;

	/// <summary>
	/// For single targets
	/// </summary>
	/// <param name="targetEnum">Target enum.</param>
	/// <param name="entityManager">Entity manager.</param>
	public TargetResolverSingle (BattleEntity entity)
	{
		this.mTargetEntity = new BattleEntity[]{ entity };
	}	
	
	public bool HasValidTargets (ICombatSkill skill)
	{
		return skill.TargetRule.IsValidTarget(mTargetEntity[0]);
	}

	public BattleEntity[] GetTargets(ICombatSkill skill) {
		if(HasValidTargets(skill)) {
			return mTargetEntity;
		}
		return new BattleEntity[0];
	}
}

