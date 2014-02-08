using UnityEngine;
using System.Collections;

public class EnemyBattleEntity : BattleEntity {
	
	public interface INPCActionListener {
		void OnAIDecisionRequired(EnemyBattleEntity npc);
	}
	
	private INPCActionListener listener;
	
	// setup variables
	public EnemyBattleEntity(EnemyCharacter character, INPCActionListener listener) : base(character) {
		this.listener = listener;
	}
	
	public override void OnRequiresInput (TurnState state) {
		this.listener.OnAIDecisionRequired(this);
	}
	
	public EnemyCharacter enemyCharacter {
		get { return (EnemyCharacter) character; }
	}
	
	public override bool isPC ()
	{
		return false;
	}
}
