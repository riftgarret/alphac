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
public class Weapon
{
	public static readonly Weapon EMPTY_WEAPON = new Weapon();
	/// <summary>
	/// Not to be mistaken with EMPTY weapon, unarmed is default for first weapon slot if none
	/// </summary>
	public static readonly Weapon UNARMED_WEAPON = new Weapon();

	// weapon config to setup in the data module
	[SerializeField]
	private WeaponData mWeaponConfig;

	// TODO add 'inscriptions'

	public Weapon() {}

	public Weapon (WeaponData config) {
		this.mWeaponConfig = config;
	}

	/// <summary>
	/// Calculates the attack. based on modifiers and base damasge
	/// </summary>
	/// <returns>The attack.</returns>
	/// <param name="character">Character.</param>
	public float CalculateAttack(Character character) {
		float atk = mWeaponConfig.baseDamage;
		if(mWeaponConfig.physicalModifiers != null) {
			foreach(StatModifier stat in mWeaponConfig.statModifiers) {
				atk += character.GetStat(stat.stat) * stat.mod;
			}
		}
		return atk;
	}

	public WeaponData weaponConfig {
		get { return mWeaponConfig; } 
	}
}


