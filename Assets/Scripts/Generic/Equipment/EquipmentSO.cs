//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public abstract class EquipmentSO : SanitySO, IEquipment
{
	public string displayName;
	public Texture2D icon;

	// lazy initialized
	private ICombatNode mCachedCombatNode;

	protected abstract ICombatNode CreateCombatNode();

	protected override void SanityCheck ()
	{
		if(string.IsNullOrEmpty(displayName)) {
			LogNull("displayName");
		}

		if(icon == null) {
			LogNull("icon");
		}
	}

	public string DisplayName {
		get {
			return displayName;
		}
	}

	public Texture2D Icon {
		get {
			return icon;
		}
	}

	public ICombatNode combatNode {
		get {
			if(mCachedCombatNode == null) {
				mCachedCombatNode = CreateCombatNode();
			} 
			return mCachedCombatNode;
		}
	}
}


