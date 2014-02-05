using UnityEngine;
using System.Collections;

public class PCBattleEntity : BattleEntity {

	public interface IPCActionListener {
		void OnPCActionRequired(PCBattleEntity pc);
	}

	private IPCActionListener listener;

	// setup variables
	public PCBattleEntity(PCCharacter character, IPCActionListener listener) : base(character) {
		this.listener = listener;
	}

	public override void OnRequiresInput (TurnState state) {
		this.listener.OnPCActionRequired(this);
	}

	public PCCharacter pcCharacter {
		get { return (PCCharacter) character; }
	}

	public override bool isPC ()
	{
		return true;
	}
}
