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
public class Armor : Equipment, IArmor 
{
	public static readonly Armor EMPTY_ARMOR = new Armor();

	public ArmorPosition ArmorPosition {
		private set;
		get;
	}

	public ArmorType ArmorType {
		private set;
		get;
	}

	public CombatProperty[] CombatProperties {
		private set;
		get;
	}

}

