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

public interface IOffensivePhysicalMods
{
	/// <summary>
	/// Power is how strong the attack is, used to determine if it can break a block or something
	/// </summary>
	/// <value>The power.</value>
	float power {
		get;
	}

	/// <summary>
	/// Total Damage modifier
	/// </summary>
	/// <value>The total damage.</value>
	float totalDamage {
		get;
	}

	float critChanceMod {
		get;
	}

	float critChanceTotal {
		get;
	}

	float armorDodge {
		get;
	}

	float armorIgnore {
		get;
	}

	float dodgeIgnore {
		get;
	}
}

