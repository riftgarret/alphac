using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleNormalGUI : MonoBehaviour {

	// overarching system that manages the battle system
	private BattleManager manager;

	// ALLY
	private BattleEntity[] allyCharacters;

	// Configuration of how to layout character portraits
	public BattleCharacterConfig allyCharacterLayoutConfig;

	// ALLY Methods
	// Character preferences that we get our customized skinning for boxes from
	private AllyGUI[] allyGUIs;

	// Logic for laying out each character
	private BattleCharacterLayout allyCharacterLayout;


	void Awake() {
		allyCharacterLayout = new BattleCharacterLayout();
		manager = GetComponent<BattleManager>();

	}

	// Use this for initialization
	void Start () {
		OnStartAlly();
	}
	
	// Update is called once per frame
	void OnGUI () {
		OnGUIAlly();
	}


	// ALLY METHODS
	private void OnAwakeAlly() {		

	}

	private void OnStartAlly() {
		List<BattleEntity> entityList = new List<BattleEntity>();
		foreach(BattleEntity entity in manager.battleEntities) {
			if(entity.isPC()) {
				entityList.Add(entity);
			}
		}
		allyCharacters = (BattleEntity[])entityList.ToArray();

		// TODO figure out location formula for where characters are on screen, 
		// for now lets just calculate based on 7 of this size appearing equal size		
		float layoutWidth = allyCharacterLayoutConfig.layoutSize.width;
		allyGUIs = new AllyGUI[allyCharacters.Length];
		
		// initialize each character 
		for(int characterIndex=0; characterIndex < allyCharacters.Length; characterIndex++) {
			AllyGUI gui = new AllyGUI();
			
			gui.x = (Screen.width - (layoutWidth * 0.5f * (float)allyCharacters.Length)) / 2f + (layoutWidth * characterIndex);
			gui.y = Screen.height - allyCharacterLayoutConfig.layoutSize.height;
			gui.character = allyCharacters[characterIndex];
			
			allyGUIs[characterIndex] = gui;
		}			
	}
	
	private void OnGUIAlly() {
		if(allyGUIs == null) {
			return;
		}




	}

	/// <summary>
	/// Draws the PC portraits.
	/// </summary>
	private void DrawPCs() {
		// draw each character
		for(int i=0; i < allyGUIs.Length; i++) {
			AllyGUI gui = allyGUIs[i];
			allyCharacterLayout.DrawCharacter(gui.x, gui.y, allyCharacterLayoutConfig, gui.character);
		}
	}

	/// <summary>
	/// Draw the hotkey buttons only if we have a current character
	/// </summary>
	private void DrawHotkeys() {
		// draw the action bar if its a characters turn
		PCTurnManager turnManager = manager.turnManager;
		if(!turnManager.isWaitingForInput) {
			return;					
		}


	}


	// internal class to encapsulate the potential move state / action or animation this could be in to communicate to
	// the child class to layout
	class AllyGUI {
		public float x;
		public float y;
		public BattleEntity character;
		//BattleChara
	}
}
