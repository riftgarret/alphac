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

	private StatusEffectManager mStatusEffectManager;

	// setup variables
	public BattleEntity(Character character) {
		mStatusEffectManager = new StatusEffectManager(this);
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

	
	///////////////////
	// proxy all character attributes / abilities so they can be adjusted by status effects
	///////////////////////

	// attributes
	// current stats
	public float curHP {
		get { return this.character.curHP; }
		set { this.character.curHP = value; }
	}
	public float curResource {
		get { return this.character.curResource; }
		set { this.character.curResource = value; }
	}
	
	//
	public float maxHP {
		get { return this.character.maxHP; }
	}

	public float maxResource {
		get { return this.character.maxResource; }
	}
	
	// character name
	public string displayName {
		get { return this.character.displayName; }
	}
	
	// stats
	public float strength {
		get { return this.character.strength; }
	}

	public float vitality {
		get { return this.character.vitality; }
	}

	public float dexerity {
		get { return this.character.dexerity; }
	}

	public float agility  {
		get { return this.character.agility; }
	}

	public float inteligence {
		get { return this.character.inteligence; }
	}

	public float wisdom {
		get { return this.character.wisdom; }
	}

	public float luck {
		get { return this.character.luck; }
	}


	// calculated attributes

	public float physicalAttack {
		get {
			return this.character.physicalAttack;
		}
	}

	public float magicAttack {
		get { return this.character.magicAttack; }
	}
	
	public float accuracy {
		get {
			return this.character.accuracy;
		}
	}
	
	public float relfex {
		get {
			return this.character.relfex;
		}
	}

	public float critChance {
		get { return this.character.critChance; }
	}
	
	public float critDefense {
		get { return this.character.critDefense; }
	}

	public float GetStat(StatType stat) {
		return this.character.GetStat(stat);
	}

	public float GetResist(DamageType dmg) {
		return this.character.GetResist(dmg);
	}

	public float GetResist(ElementResistType resistType) {
		switch(resistType) {
		case ElementResistType.DARK:
			return GetResist(DamageType.DARK);
		case ElementResistType.EARTH:
			return GetResist(DamageType.EARTH);
		case ElementResistType.FIRE:
			return GetResist(DamageType.FIRE);
		case ElementResistType.LIGHT:
			return GetResist(DamageType.LIGHT);
		case ElementResistType.WATER:
			return GetResist(DamageType.WATER);
		case ElementResistType.WIND:
			return GetResist(DamageType.WIND);
		}
		return 0;
	}
}
