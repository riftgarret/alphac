using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour, PCBattleEntity.IPCActionListener, EnemyBattleEntity.INPCActionListener {

	
	public float unitOfTime = 1f;

	/// <summary>
	/// in game clock thats handled on update when the user interface isnt stalled for current active character	/// </summary>
	/// <value><c>true</c> if active game time; otherwise, <c>false</c>.</value>
	private bool activeGameTime {
		get {
			return turnManager.decisionState == PCTurnManager.DecisionState.IDLE;
		}
	}

	/// <summary>
	/// The battle time queue.
	/// </summary>
	private BattleTimeQueue battleTimeQueue;

	/// <summary>
	/// Gets the turn manager.
	/// </summary>
	/// <value>The turn manager.</value>
	public PCTurnManager turnManager {
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
		battleTimeQueue = new BattleTimeQueue(unitOfTime);
		turnManager = new PCTurnManager(this);

		// initialize entities for other methods in start
		EnemyCharacter[] npcChars = enemyParty.CreateUniqueCharacters();
		PCCharacter[] pcChars = pcParty.CreateUniqueCharacters();

		mEntityManager = new BattleEntityManager(this, pcChars, npcChars);				

		battleTimeQueue.InitEntities(mEntityManager.allEntities);
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

		battleTimeQueue.IncrementTimeDelta(Time.deltaTime);
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
		battleTimeQueue.SetAction(entity, action);
	}

	public void OnPCAction(PCBattleEntity entity, IBattleAction action) {
		battleTimeQueue.SetAction(entity, action);
	}

	public void OnBattleChanged(int eventType) {
		// TODO, handle on battle changed
	}
	
}
