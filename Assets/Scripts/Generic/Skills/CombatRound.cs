//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

/// <summary>
/// Combat round. To be serialized in setting up combat skills
/// </summary>
[Serializable]
public class CombatRound
{
    public int weaponIndex;
	public CombatOperationType operationType = CombatOperationType.PHYSICAL_ATTACK;
	public StatusEffectRule [] statusEffectRules = new StatusEffectRule[0];
	public CombatProperty [] combatProperties = new CombatProperty[0];
}

