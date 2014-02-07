using UnityEngine;
using System.Collections;

public class NPCBattleEntity : BattleEntity {
	
	public interface INPCActionListener {
		void OnAIDecision(NPCBattleEntity npc);
	}
	
	private INPCActionListener listener;
	
	// setup variables
	public NPCBattleEntity(EnemyCharacter character, INPCActionListener listener) : base(character) {
		this.listener = listener;
	}
	
	public override void OnRequiresInput (TurnState state) {
		this.listener.OnAIDecision(this);
	}
	
	public EnemyCharacter pcCharacter {
		get { return (EnemyCharacter) character; }
	}
	
	public override bool isPC ()
	{
		return false;
	}
}
