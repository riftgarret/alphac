using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour, PCBattleEntity.IPCActionListener {

	public float unitOfTime = 1f;

	/// <summary>
	/// in game clock thats handled on update when the user interface isnt stalled for current active character	/// </summary>
	/// <value><c>true</c> if active game time; otherwise, <c>false</c>.</value>
	private bool activeGameTime {
		get {
			return !turnManager.isWaitingForInput;
		}
	}

	private BattleTimeQueue battleTimeQueue;

	public PCTurnManager turnManager {
		private set;
		get;
	}

	public BattleEntity[] battleEntities {
		private set;
		get;
	}

	void Awake() {
		battleTimeQueue = new BattleTimeQueue(unitOfTime);
		turnManager = new PCTurnManager(this);

		PCCharacter[] chars = TempCreateCharacters();

		// initialize entities for other methods in start
		BattleEntity [] entities = new BattleEntity[chars.Length];
		for(int i=0; i < entities.Length; i++) {
			entities[i] = new PCBattleEntity(chars[i], this);
		}
		battleTimeQueue.InitEntities(entities);
		battleEntities = entities;
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

	private PCCharacter[] TempCreateCharacters() {
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

		PCCharacter[] chars = new PCCharacter[1];
		chars[0] = pc1;
		//chars[1] = pc2;
		//chars[2] = pc3;

		return chars;
	}

	public void OnPCActionRequired(PCBattleEntity pc) {
		turnManager.QueuePC(pc);
	}

	public void OnPCAction(PCBattleEntity entity, BattleAction action) {
		battleTimeQueue.SetAction(entity, action);
	}
}
