using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCTurnManager {

	private BattleManager manager;
	private Queue<PCBattleEntity> turnList;
	
	public PCTurnManager(BattleManager manager) {
		turnList = new Queue<PCBattleEntity>();
		this.manager = manager;
	}

	/// <summary>
	/// Queues the PC into the turn list.
	/// </summary>
	/// <param name="pc">Pc.</param>
	public void QueuePC(PCBattleEntity pc) {
		turnList.Enqueue(pc);
	}
	
	/// <summary>
	/// Gets a value indicating whether this <see cref="BattleManager+PCTurnManager"/> has P.
	/// </summary>
	/// <value><c>true</c> if has P; otherwise, <c>false</c>.</value>
	public bool isWaitingForInput {
		get { return turnList.Count > 0; }
	}
	
	/// <summary>
	/// Selects next character (if there are any) for the next turn
	/// </summary>
	public void NextTurn() {
		PCBattleEntity cur = turnList.Dequeue();
		turnList.Enqueue(cur);
	}
	
	/// <summary>
	/// Set the action to the current top battle entity selected.
	/// </summary>
	/// <param name="action">Action.</param>
	public void DoAction(BattleAction action) {
		if( turnList.Count == 0 ) {
			// do nothing bad state
			Debug.LogError("Bad state, PCTurnManager.DoAction when no PC available");
			return;
		}
		PCBattleEntity entity = turnList.Dequeue();
		manager.OnPCAction(entity, action);
	}
	
	/// <summary>
	/// Gets the current PC Battle Entity.
	/// </summary>
	/// <value>The current entity.</value>
	public BattleEntity currentEntity {
		get {
			if(turnList.Count > 0) {
				return turnList.Peek();
			}
			return null;
		}
	}
}
