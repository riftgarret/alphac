//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

public class PCCharacter : Character
{
	/// <summary>
	/// Hotkeys the player has set up
	/// </summary>
	/// <value>The hotkeys.</value>
	public HotKey[] hotkeys {
		get;
		private set;
	}

	public PCCharacter ()
	{
		hotkeys = new HotKey[10];
		for(int i=0; i < hotkeys.Length; i++) {
			hotkeys[i] = new HotKey(new SkillAttack());
		}

	}
}
