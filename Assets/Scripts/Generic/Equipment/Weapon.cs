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


// TODO separate PhysicalWeapon from MagicalWeapon (for caster reasons)
[Serializable]
public class Weapon : IWeapon
{
	public static readonly Weapon EMPTY_WEAPON = new Weapon();
	/// <summary>
	/// Not to be mistaken with EMPTY weapon, unarmed is default for first weapon slot if none
	/// </summary>
	public static readonly Weapon UNARMED_WEAPON = new Weapon();

	// weapon config to setup in the data module

	public Weapon() {
		DamageType = DamageType.SLASH;
		WeaponType = WeaponType.AXE;
		DisplayName = "Uninitialized";
		combatNode = new WeaponCombatNode(this);
	}

	public DamageType DamageType {
		get;
		set;
	}

	public WeaponType WeaponType {
		get; 
		set;
	}

	public string DisplayName {
		get;
		set;
	}

	public Texture2D Icon {
		get;
		set;
	}

	public ICombatNode combatNode {
		get;
		set;
	}
}


