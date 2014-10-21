using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class CharacterLayoutComponent: MonoBehaviour {

    public GameObject characterPortraitPrefab;
    private BattleEntityManagerComponent mEntityManager;    
    private RectTransform mTransform;

    void Awake() {
        mEntityManager = GameObject.FindGameObjectWithTag(Tags.BATTLE_CONTROLLER).GetComponent<BattleEntityManagerComponent>();
		mTransform = GetComponent<RectTransform>();          
    }

    void Start() {
        foreach (PCBattleEntity pc in mEntityManager.pcEntities) {
            GameObject characterPortrait = (GameObject)Instantiate(characterPortraitPrefab);
            RectTransform rect = characterPortrait.GetComponent<RectTransform>();
            CharacterGUIComponent charGUI = characterPortrait.GetComponent<CharacterGUIComponent>();
            charGUI.BattleEntity = pc;
			rect.SetParent(mTransform);
        }
    }

    void OnGUI() {
        
    }

}

