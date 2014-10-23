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

public class CharacterSO : SanitySO
{	
	// character name
	public string displayName;
	
	// stats
    public AttributeVector attributes = new AttributeVector();
    public CombatStatsVector combatStats = new CombatStatsVector();
    public ElementVector elementDefense = new ElementVector();
    public ElementVector elementAttack = new ElementVector();    
	
	// level and class
	public int level;
	public CharacterClassSO charClass;
	
	// equipment
	public WeaponSO [] weapons;
	public ArmorSO [] armors;


	protected override void SanityCheck ()
	{
		LogEmptyArray("weapons", weapons);
		LogEmptyArray("armors", armors);
	}

	public Character GenerateCharacter() {
		return null; // TODO Create BUILDER class for Character using Params
	}
}

