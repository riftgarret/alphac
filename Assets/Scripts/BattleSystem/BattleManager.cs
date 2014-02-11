using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour, PCBattleEntity.IPCActionListener, EnemyBattleEntity.INPCActionListener, IBattleEventListener {

	
	public float unitOfTime = 1f;

	// a way to we can only be in a certain state when the game is active
	private enum GameState {
		INTRO,
		ACTIVE,
		VICTORY,
		LOSS
	}

	/// <summary>
	/// in game clock thats handled on update when the user interface isnt stalled for current active character	/// </summary>
	/// <value><c>true</c> if active game time; otherwise, <c>false</c>.</value>
	private bool activeGameTime {
		get {
			bool isActive = turnManager.decisionState == PCTurnManager.DecisionState.IDLE;
			isActive &= mGameState == GameState.ACTIVE;
			return isActive;
		}
	}

	/// <summary>
	/// The battle time queue.
	/// </summary>
	private BattleTimeQueue mBattleTimeQueue;

	private GameState mGameState;

	/// <summary>
	/// Gets the turn manager.
	/// </summary>
	/// <value>The turn manager.</value>
	public PCTurnManager turnManager {
		private set;
		get;
	}

	public BattleEventManager eventManager {
		private set;
		get;
	}

	// for managing positions and enemies
	private BattleEntityManager mEntityManager;
	public BattleEntityManager entityManager {
		get { return mEntityManager; }
	}

	public EnemyParty enemyParty;
	public PCParty pcParty;

	private bool mOnBattleChangedFlag;

	void Awake() {
		mBattleTimeQueue = new BattleTimeQueue(unitOfTime, this);
		turnManager = new PCTurnManager(this);
		eventManager = new BattleEventManager();
		eventManager.battleEventListener = this;
		mGameState = GameState.ACTIVE; // eventualyl this will be INTRO

		// initialize entities for other methods in start
		EnemyCharacter[] npcChars = enemyParty.CreateUniqueCharacters();
		PCCharacter[] pcChars = pcParty.CreateUniqueCharacters();

		mEntityManager = new BattleEntityManager(this, pcChars, npcChars);				

		mBattleTimeQueue.InitEntities(mEntityManager.allEntities);
	}

	//private PriorityQueue
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateInGameClock();
	}

	// manage in game clock
	private void UpdateInGameClock() {
		// if we arent in active game time, dont move clock
		if(!activeGameTime) {
			return;
		}

		mBattleTimeQueue.IncrementTimeDelta(Time.deltaTime);
	}

	// ActionListener callback
	public void OnPCActionRequired(PCBattleEntity pc) {
		turnManager.QueuePC(pc);
	}

	// NPC callabck
	public void OnAIDecisionRequired(EnemyBattleEntity npc) {
		npc.enemyCharacter.skillResolver.ResolveAction(this, npc);
	}

	public void OnAIDecision(EnemyBattleEntity entity, IBattleAction action) {
		mBattleTimeQueue.SetAction(entity, action);
	}

	public void OnPCAction(PCBattleEntity entity, IBattleAction action) {
		mBattleTimeQueue.SetAction(entity, action);
	}
	
	// battle event listener
	public void OnBattleEvent (IBattleEvent e)
	{
		Debug.Log(e.eventText);
		// TODO forward to combat log

		// evaluate if the game is over, or we have won
		switch(e.eventType) {
		case BattleEventType.DEATH:
			CheckForVictoryOrAnnilate(!e.srcEntity.isPC); // 
			break;
		}

	}

	private void CheckForVictoryOrAnnilate(bool isEnemies) {
		BattleEntity[] entities = isEnemies? (BattleEntity[])entityManager.enemyEntities : (BattleEntity[])entityManager.pcEntities;


		foreach(BattleEntity entity in entities) {
			if(entity.character.curHP > 0) {
				return; // we found an alive player, no way to achieve either state
			}
		}

		// if we got here, it means everyone is dead
		this.mGameState = isEnemies? GameState.VICTORY : GameState.LOSS;

		// not sure if this is the best place to put this, perhaps in its own script
		if(isEnemies) {
			Debug.Log("Victory");
		}
		else {
			Debug.Log ("Defeat");
		}
	}
}
