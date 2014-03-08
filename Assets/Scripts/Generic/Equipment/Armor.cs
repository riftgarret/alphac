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
public class Armor
{
	public static readonly Armor EMPTY_ARMOR = new Armor();

	[SerializeField]
	private ArmorSO mConfig;
	public ArmorSO config {
		get { return mConfig; }
	}	

	public Armor() {}

	public Armor(ArmorSO config) {
		mConfig = config;
	}

	public string displayName {
		get { return mConfig.displayName; }
	}

	public ArmorPosition slot {
		get { return mConfig.armorPosition; }
	}

	public ArmorType type {
		get { return mConfig.armorType; }
	}

	public ResistProperties resists {
		get { return mConfig.resists; }
	}
}

