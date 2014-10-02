﻿using UnityEngine;
using System.Collections;

public class PCBattleEntity : BattleEntity {

	// setup variables
	public PCBattleEntity(PCCharacter character, BattleEntity.OnDecisionRequiredListener listener) : base(character, listener) {		
	}

	public PCCharacter pcCharacter {
		get { return (PCCharacter) character; }
	}

	public override bool isPC 
	{
		get { return true; }
	}
}
