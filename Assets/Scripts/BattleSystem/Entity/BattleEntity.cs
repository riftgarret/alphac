using UnityEngine;
using System.Collections;

public abstract class BattleEntity {



	// turn phase
	public TurnState turnState {
		get;
		protected set;
	}

	/// <summary>
	/// Gets the character associated with this element.
	/// </summary>
	/// <value>The character.</value>
	public Character character {
		get;
		protected set;
	}

	// setup variables
	public BattleEntity(Character character) {
		turnState = new TurnState(this);
		this.character = character;
	}


	public void InitializeBattlePhase() {
		turnState.SetAction(new BattleActionInitiative(Random.Range(1, 5)));
	}

	/// <summary>
	/// Is this entity a PC
	/// </summary>
	/// <returns><c>true</c>, if P was ised, <c>false</c> otherwise.</returns>
	public abstract bool isPC() ;

	/// <summary>
	/// Raises the requires input event. This should be managed by either a PC to get actions
	/// from the user and pause the game, or from the NPC who should decide automatically based on AI
	/// 
	/// </summary>
	/// <param name="state">State.</param>
	public abstract void OnRequiresInput(TurnState state);

	public void IncrementGameClock(float gameClockDelta) {
		// TODO, we can modify time if we have that buff here
		turnState.IncrementGameClock(gameClockDelta);
	}

	public bool requireUserInput() {
		return turnState.phase == TurnState.Phase.REQUIRES_INPUT;
	}

	public void OnExecuteTurn(TurnState state) {
		// do action against character
		state.action.OnExecuteAction(state.turnClock);
	}

	// TODO return DamageResults, or process results to print out
	public void TakeDamage(Damage damage) {
		// calculate resists
		this.character.curHP -= damage.slashDamage;
		Debug.Log("Damage: " + damage.slashDamage + " HP dropped to: " + this.character.curHP);
	}
}
