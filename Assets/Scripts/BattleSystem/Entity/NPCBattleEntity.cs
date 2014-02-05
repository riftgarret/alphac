using UnityEngine;
using System.Collections;

public class NPCBattleEntity : BattleEntity {
	
	public interface INPCActionListener {
		void OnAIDecision(NPCBattleEntity npc);
	}
	
	private INPCActionListener listener;
	
	// setup variables
	public NPCBattleEntity(NPCCharacter character, INPCActionListener listener) : base(character) {
		this.listener = listener;
	}
	
	public override void OnRequiresInput (TurnState state) {
		this.listener.OnAIDecision(this);
	}
	
	public NPCCharacter pcCharacter {
		get { return (NPCCharacter) character; }
	}
	
	public override bool isPC ()
	{
		return false;
	}
}
