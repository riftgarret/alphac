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

	void Awake() {
		battleTimeQueue = new BattleTimeQueue(unitOfTime);
		turnManager = new PCTurnManager(this);

		// initialize entities for other methods in start
		NPCCharacter[] npcChars = TempCreateNPCs();
		PCCharacter[] pcChars = TempCreatePCs();

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

	private PCCharacter[] TempCreatePCs() {
		PCCharacter pc1 = new PCCharacter();
		pc1.name = "Vaten";
		pc1.maxHealth = 100;
		pc1.curHealth = 90;

		PCCharacter pc2 = new PCCharacter();
		pc2.name = "Alphac";
		pc2.maxHealth = 120;
		pc2.curHealth = 30;

		PCCharacter pc3 = new PCCharacter();
		pc3.name = "Silmaria";
		pc3.maxHealth = 103;
		pc3.curHealth = 103;

		PCCharacter[] chars = new PCCharacter[3];
		chars[0] = pc1;
		chars[1] = pc2;
		chars[2] = pc3;

		return chars;
	}

	private NPCCharacter[] TempCreateNPCs() {
		NPCCharacter npc1 = new NPCCharacter();
		npc1.name = "Kefka Palazzo";
		npc1.maxHealth = 100;
		npc1.curHealth = 90;
		
		NPCCharacter npc2 = new NPCCharacter();
		npc2.name = "Emperor Gestahl";
		npc2.maxHealth = 120;
		npc2.curHealth = 30;

		NPCCharacter[] chars = new NPCCharacter[2];
		chars[0] = npc1;
		chars[1] = npc2;
		
		return chars;
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

	public void OnPCAction(PCBattleEntity entity, BattleAction action) {
		battleTimeQueue.SetAction(entity, action);
	}
}
