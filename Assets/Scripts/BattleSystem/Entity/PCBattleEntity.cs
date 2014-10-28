using UnityEngine;
using System.Collections;

public class PCBattleEntity : BattleEntity {

	// setup variables
	public PCBattleEntity(PCCharacter character, BattleEntity.BattleEntityDelegate listener) : base(character, listener) {		
	}

	public PCCharacter pcCharacter {
		get { return (PCCharacter) character; }
	}

	public PCSkillSet SkillSet {
		get { return pcCharacter.skillSet; }
	}

	public override bool isPC 
	{
		get { return true; }
	}
}
