using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCTurnManager {

	public enum DecisionState {
		SkillSelection,
		TargetSelection,
	}

	private BattleManager manager;
	private Queue<PCBattleEntity> turnQueue;

	/// <summary>
	/// Gets the state of the decision. Either selecting a skill, or targeting with selected skill
	/// </summary>
	/// <value>The state of the decision.</value>
	public DecisionState decisionState {
		get;
		private set;
	}
		
	public PCTurnManager(BattleManager manager) {
		turnQueue = new Queue<PCBattleEntity>();
		this.manager = manager;
	}

	/// <summary>
	/// Queues the PC into the turn list.
	/// </summary>
	/// <param name="pc">Pc.</param>
	public void QueuePC(PCBattleEntity pc) {
		turnQueue.Enqueue(pc);
	}
	
	/// <summary>
	/// Gets a value indicating whether this <see cref="BattleManager+PCTurnManager"/> has P.
	/// </summary>
	/// <value><c>true</c> if has P; otherwise, <c>false</c>.</value>
	public bool isWaitingForInput {
		get { return turnQueue.Count > 0; }
	}
	
	/// <summary>
	/// Selects next character (if there are any) for the next turn
	/// </summary>
	public void NextTurn() {
		PCBattleEntity cur = turnQueue.Dequeue();
		turnQueue.Enqueue(cur);
	}
	
	/// <summary>
	/// Set the action to the current top battle entity selected.
	/// </summary>
	/// <param name="action">Action.</param>
	public void SelectSkill(Skill skill) {
		if( turnQueue.Count == 0 ) {
			// do nothing bad state
			Debug.LogError("Bad state, PCTurnManager.DoAction when no PC available");
			return;
		}


		PCBattleEntity entity = turnQueue.Dequeue();
		manager.OnPCAction(entity, action);
	}
	
	/// <summary>
	/// Gets the current PC Battle Entity.
	/// </summary>
	/// <value>The current entity.</value>
	public PCBattleEntity currentEntity {
		get {
			if(turnQueue.Count > 0) {
				return turnQueue.Peek();
			}
			return null;
		}
	}
}
