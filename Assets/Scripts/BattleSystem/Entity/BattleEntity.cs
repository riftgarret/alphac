﻿using UnityEngine;
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

	/// <summary>
	/// The status effect manager. Manages status effects so when a new effect is added, 
	/// we can tell if its refresh, new, or canceling something else.
	/// </summary>
	private StatusEffectManager mStatusEffectManager;

	/// <summary>
	/// The combat node factory. Used to generate a NodeBuilder which will bring together
	/// an offensive combat nodes for stat aggregation
	/// </summary>
	private CombatNodeFactory mCombatNodeFactory;

	// setup variables
	public BattleEntity(Character character) {
		mStatusEffectManager = new StatusEffectManager(this);
		mCombatNodeFactory = new CombatNodeFactory (this);
		turnState = new TurnState(this);
		this.character = character;
	}


	public void InitializeBattlePhase() {
		turnState.SetAction(new BattleActionInitiative(Random.Range(1, 5)));
	}

	public void ApplyStatusEffect(IStatusEffect statusEffect) {
		mStatusEffectManager.HandleAddStatus(statusEffect);
	}

	/// <summary>
	/// Is this entity a PC
	/// </summary>
	/// <returns><c>true</c>, if P was ised, <c>false</c> otherwise.</returns>
	public abstract bool isPC {
		get;
	}

	/// <summary>
	/// Raises the requires input event. This should be managed by either a PC to get actions
	/// from the user and pause the game, or from the NPC who should decide automatically based on AI
	/// 
	/// </summary>
	/// <param name="state">State.</param>
	public abstract void OnRequiresInput(TurnState state);

	public void IncrementGameClock(float gameClockDelta, BattleTimeQueue timeQueue) {
		// TODO, we can modify time if we have that buff here
		turnState.IncrementGameClock(gameClockDelta, timeQueue.manager);
		mStatusEffectManager.OnTimeIncrement(gameClockDelta);
	}

	public bool requireUserInput() {
		return turnState.phase == TurnState.Phase.REQUIRES_INPUT;
	}

	public void OnExecuteTurn(TurnState state, BattleEventManager eventManager) {
		// do action against character
		state.action.OnExecuteAction(state.turnClock, eventManager);
	}


	public CombatNodeBuilder CreateCombatNodeBuilder() {
		return null;
	}

	/// <summary>
	/// Overloading Getting a weapon from the character incase there is a status buff that may add weapons. 
	/// Such as summoned weapons
	/// </summary>
	/// <returns>The weapon.</returns>
	/// <param name="weaponIndex">Weapon index.</param>
	public Weapon GetWeapon(int weaponIndex) {
		return character.GetWeapon (weaponIndex);
	}

	/// <summary>
	/// Overload max weapon count to allow summonable weapons
	/// </summary>
	/// <value>The max weapon count.</value>
	public int maxWeaponCount {
		get { return character.maxWeaponCount; }
	}
}
