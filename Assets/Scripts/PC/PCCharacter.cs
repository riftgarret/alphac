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

[System.Serializable]
public class PCCharacter : Character
{
	public enum RowPosition {
		FRONT,
		MIDDLE,
		BACK
	}



	/// <summary>
	/// Gets or sets the row position.
	/// </summary>
	/// <value>The row position.</value>
	public RowPosition rowPosition;

	public PCSkillSet skillSet;

	public PCCharacter ()
	{
/*		hotkeys = new HotKey[10];
		for(int i=0; i < hotkeys.Length; i++) {
			hotkeys[i] = new HotKey(new SkillAttack());
		}
*/
	}
	
	public PCCharacter(PCCharacterSO config) : base(config) {
		skillSet = new PCSkillSet();
		config.skillsetConfig.InitSkills(skillSet);
	}
}
