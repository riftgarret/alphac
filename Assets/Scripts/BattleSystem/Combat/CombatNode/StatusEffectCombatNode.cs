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

public class StatusEffectCombatNode : ConfigurableCombatNode
{

	/// <summary>
	/// Initializes a new instance of the <see cref="StatusEffectCombatNode"/> class.
	/// </summary>
	/// <param name="statusEffectExecutor">Status effect executor.</param>
	public StatusEffectCombatNode(CombatPropertyStatusEffectExecutor statusEffectExecutor) : base() {
		// TODO track node meta
		StatusEffectSO so = statusEffectExecutor.statusEffectSO;
		CombatPropertyType property = so.statusEffectGroup.combatProperty;
		mPropertyAdd[(int)property] = so.valueModifier.add;
		mPropertyMultiply[(int)property] = so.valueModifier.scalar;
	}
}

