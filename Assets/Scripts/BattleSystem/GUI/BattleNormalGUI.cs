using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleNormalGUI : MonoBehaviour {

	// overarching system that manages the battle system
	private BattleManager manager;

	// Configuration of how to layout character portraits
	public BattleCharacterConfig pcCharacterLayoutConfig;
	public BattleSkillButtonConfig pcKeyLayoutConfig;

	// ALLY Methods
	// Character preferences that we get our customized skinning for boxes from
	private AllyGUI[] pcGUIs;

	// Logic for laying out each character
	private BattleCharacterLayout pcCharacterLayout;
	private BattleSkillButtonsLayout skillButtonLayout;
	private BattleTargetingLayout targetLayout;

	void Awake() {
		pcCharacterLayout = new BattleCharacterLayout();
		skillButtonLayout = new BattleSkillButtonsLayout();
		targetLayout = new BattleTargetingLayout();

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
		// todo get just ally from here
		PCBattleEntity[] allyCharacters = manager.pcBattleEntities;

		// TODO figure out location formula for where characters are on screen, 
		// for now lets just calculate based on 7 of this size appearing equal size		
		float layoutWidth = pcCharacterLayoutConfig.layoutSize.width;
		pcGUIs = new AllyGUI[allyCharacters.Length];
		
		// initialize each character 
		for(int characterIndex=0; characterIndex < allyCharacters.Length; characterIndex++) {
			AllyGUI gui = new AllyGUI();
			
			gui.x = (Screen.width - (layoutWidth * 0.5f * (float)allyCharacters.Length)) / 2f + (layoutWidth * characterIndex);
			gui.y = Screen.height - (pcCharacterLayoutConfig.layoutSize.height + pcKeyLayoutConfig.layoutSize.height);
			gui.character = allyCharacters[characterIndex];
			
			pcGUIs[characterIndex] = gui;
		}			
	}
	
	private void OnGUIAlly() {
		if(pcGUIs == null) {
			return;
		}

		DrawPCs();
		DrawHotkeys();
		DrawTargets();
	}

	/// <summary>
	/// Draws the PC portraits.
	/// </summary>
	private void DrawPCs() {
		// draw each character
		for(int i=0; i < pcGUIs.Length; i++) {
			AllyGUI gui = pcGUIs[i];
			pcCharacterLayout.DrawCharacter(gui.x, gui.y, pcCharacterLayoutConfig, gui.character);
		}
	}

	/// <summary>
	/// Draw the hotkey buttons only if we have a current character
	/// </summary>
	private void DrawHotkeys() {
		// draw the action bar if its a characters turn
		PCTurnManager turnManager = manager.turnManager;
		if(turnManager.decisionState != PCTurnManager.DecisionState.SKILL) {
			return;					
		}

		skillButtonLayout.DrawButtons(turnManager, pcKeyLayoutConfig);
	}

	/// <summary>
	/// Not sure if we are keeping this. We want to draw targets to select. 
	/// </summary>
	private void DrawTargets()  {
		PCTurnManager turnManager = manager.turnManager;
		if(turnManager.decisionState != PCTurnManager.DecisionState.TARGET) {
			return;
		}

		targetLayout.DrawTargets(turnManager);
	}


	// internal class to encapsulate the potential move state / action or animation this could be in to communicate to
	// the child class to layout
	class AllyGUI {
		public float x;
		public float y;
		public PCBattleEntity character;
		//BattleChara
	}
}
