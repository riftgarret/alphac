using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour, PCBattleEntity.IPCActionListener, NPCBattleEntity.INPCActionListener, IBattleTargetProvider {

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

	/// <summary>
	/// Gets the pc battle entities.
	/// </summary>
	/// <value>The pc battle entities.</value>
	public PCBattleEntity[] pcBattleEntities {
		private set;
		get;
	}

	/// <summary>
	/// Gets the npc battle entities.
	/// </summary>
	/// <value>The npc battle entities.</value>
	public NPCBattleEntity[] npcBattleEntities {
		private set;
		get;
	}

	public EnemyParty enemyParty;
	public PCParty pcParty;

	void Awake() {
		battleTimeQueue = new BattleTimeQueue(unitOfTime);
		turnManager = new PCTurnManager(this);

		// initialize entities for other methods in start
		EnemyCharacter[] npcChars = enemyParty.CreateUniqueCharacters();
		PCCharacter[] pcChars = pcParty.CreateUniqueCharacters();

		// combine 
		BattleEntity[] allEntities = new BattleEntity[pcChars.Length + npcChars.Length];

		pcBattleEntities = new PCBattleEntity[pcChars.Length];
		for(int i=0; i < pcBattleEntities.Length; i++) {
			pcBattleEntities[i] = new PCBattleEntity(pcChars[i], this);
			allEntities[i] = pcBattleEntities[i];
		}

		npcBattleEntities = new NPCBattleEntity[npcChars.Length];
		for(int i=0; i < npcBattleEntities.Length; i++) {
			npcBattleEntities[i] = new NPCBattleEntity(npcChars[i], this);
			allEntities[pcChars.Length + i] = npcBattleEntities[i];
		}		

		battleTimeQueue.InitEntities(allEntities);
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

	public BattleEntity[] GetTargets(bool isPCTargets) {
		if(isPCTargets) {
			return pcBattleEntities;
		}
		return npcBattleEntities;
	}

	// ActionListener callback
	public void OnPCActionRequired(PCBattleEntity pc) {
		turnManager.QueuePC(pc);
	}

	// NPC callabck
	public void OnAIDecision(NPCBattleEntity npc) {
		Debug.Log("TODO: AI decisions");
	}

	public void OnPCAction(PCBattleEntity entity, IBattleAction action) {
		battleTimeQueue.SetAction(entity, action);
	}
	
}
