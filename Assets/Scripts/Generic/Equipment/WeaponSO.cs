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
public class WeaponSO : EquipmentSO, IWeapon
{
	public CombatProperty [] combatProperties;
	public DamageType damageType = DamageType.COUNT;
	public WeaponType weaponType = WeaponType.COUNT;

	protected override void SanityCheck ()
	{
		base.SanityCheck ();

		if(combatProperties == null) {
			LogNull("combatProperties");
		}

		if(damageType == DamageType.COUNT) {
			LogInvalidEnum("damageType");
		}

		if(weaponType == WeaponType.COUNT) {
			LogInvalidEnum("weaponType");
		}
	}

	public DamageType DamageType {
		get {
			return damageType;
		}
	}

	public WeaponType WeaponType {
		get {
			return weaponType;
		}
	}

	protected override ICombatNode CreateCombatNode ()
	{
		WeaponCombatNode node = new WeaponCombatNode(this);
		node.Load(combatProperties);
		return node;
	}

	// TODO build combat node to pull values from for UI
}