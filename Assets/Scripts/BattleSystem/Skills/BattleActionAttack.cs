using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleAction {
	
	public BattleActionAttack(float initiativeTime) : base(4,1, 2) {

	}

	public override void DoAction (float actionClock)
	{	
		if(actionClock >= timeAction) {
			Debug.Log("Do damage");
		}	
	}
}
