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

public class CharacterConfig : ScriptableObject
{	
	// character name
	public string displayName;
	
	// stats
	public float strength;
	public float vitality;
	public float dexerity;
	public float agility;
	public float inteligence;
	public float wisdom;
	public float luck;
	
	// level and class
	public int level;
	public CharacterClassData charClass;
	
	// equipment
	public Weapon [] weapons;
	public Armor [] armors;
}

