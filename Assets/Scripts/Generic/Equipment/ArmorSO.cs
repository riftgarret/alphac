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

public class ArmorSO : EquipmentSO, IArmor
{

	public ArmorPosition armorPosition = ArmorPosition.TORSO;
	public ArmorType armorType = ArmorType.LIGHT;
	public CombatProperty [] combatProperties;

	protected override void SanityCheck ()
	{
		base.SanityCheck();

		if(combatProperties == null) {
			LogNull("combatProperties");
		}
	}

	public ArmorPosition ArmorPosition {
		get {
			return armorPosition;
		}
	}

	public ArmorType ArmorType {
		get {
			return armorType;
		}
	}

	protected override ICombatNode CreateCombatNode ()
	{
		ArmorCombatNode node =  new ArmorCombatNode(this);
		node.Load(combatProperties);
		return node;
	}
}
 