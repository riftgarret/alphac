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

public class EquipmentCombatNode : ConfigurableCombatNode
{
	public EquipmentCombatNode(IEquipment equipment) {
        LoadElementAttackScalar(equipment.OffensiveScalar);
        LoadElementDefenseScalar(equipment.DefensiveScalar);
        LoadAttribute(equipment.AttributeExtra);
	}
 
   
}

