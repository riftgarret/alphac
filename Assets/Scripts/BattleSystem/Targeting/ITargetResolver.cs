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


public interface ITargetResolver 
{
	/// <summary>
	/// Gets a value indicating whether this <see cref="ITargetResolver"/> is valid target.
	/// </summary>
	/// <value><c>true</c> if is valid target; otherwise, <c>false</c>.</value>
	bool isValidTarget(CombatSkill skill);

	BattleEntity[] GetTargets(CombatSkill skill);
}
