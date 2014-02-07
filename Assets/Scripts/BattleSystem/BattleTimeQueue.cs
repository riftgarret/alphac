﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleTimeQueue {
	private float unitOfTime;
	private float inGameClock;

	// unit queue
	private BattleEntity [] unitArray;

	// buff queues

	public BattleTimeQueue(float unitOfTime) {
		this.unitOfTime = unitOfTime;
		inGameClock = 0f;
	}

	/// <summary>
	/// Sets the entities that are used for turn based combat
	/// </summary>
	/// <param name="entities">Entities.</param>
	public void InitEntities(BattleEntity [] entities) {
		unitArray = new BattleEntity[entities.Length];
		for(int i=0; i < entities.Length; i++) {
			entities[i].InitializeBattlePhase();
			unitArray[i] = entities[i];
		}
	}

	/// <summary>
	/// Increments the time delta. This will update the internal time
	/// by adjusting it to the unit of time being used.
	/// </summary>
	/// <param name="deltaTime">Delta time.</param>
	public void IncrementTimeDelta(float deltaTime) {
		// reset input counter, we can see which players dont have commands

		float gameClockTic = deltaTime * unitOfTime;
		inGameClock += gameClockTic;

		foreach(BattleEntity unit in unitArray) {
			unit.IncrementGameClock(gameClockTic);
		}
	}

	public void SetAction(BattleEntity entity, IBattleAction action) {
		entity.turnState.SetAction(action);
	}	
}
