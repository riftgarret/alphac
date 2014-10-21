using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ActionLayoutComponent: MonoBehaviour {

	public GameObject actionPrefab;      
    private RectTransform mTransform;
	private PCBattleEntity mCurrentEntity;
	private PCTurnManagerComponent mTurnManager;

    void Awake() {
		mTurnManager = GameObject.FindGameObjectWithTag(Tags.BATTLE_CONTROLLER).GetComponent<PCTurnManagerComponent>();
		mTransform = GetComponent<RectTransform>();          
    }

    void Start() {
        
    }

    void OnGUI() {
		PCBattleEntity entity = mTurnManager.currentEntity;		        
		if (mCurrentEntity != entity) {
			mCurrentEntity = entity;
			PopulateActions (entity);
		}
    }
					

	void PopulateActions(PCBattleEntity entity) {
		// destroy old buttons
		while (mTransform.childCount > 0) {
			Transform transform = mTransform.GetChild(0);
			transform.SetParent(null);
			GameObject.Destroy(transform.gameObject);
		}


		if (entity == null && mTurnManager) {
			return;
		}


		foreach (ICombatSkill skill in entity.SkillSet.skills) {
			GameObject actionPrefabInstance = (GameObject)Instantiate(actionPrefab);
			RectTransform rect = actionPrefabInstance.GetComponent<RectTransform>();
			ActionGUIComponent actionGUI = actionPrefabInstance.GetComponent<ActionGUIComponent>();
			actionGUI.CombatSkill = skill;
			rect.SetParent(mTransform);
		}
	}


}

