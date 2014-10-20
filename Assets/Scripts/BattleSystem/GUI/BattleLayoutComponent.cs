using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class BattleLayoutComponent: MonoBehaviour {

    public GameObject characterPortraitPrefab;
    private BattleEntityManagerComponent mEntityManager;    
    private GameObject mCharacterPanelObject;

    void Awake() {
        mEntityManager = GetComponent<BattleEntityManagerComponent>();
        mCharacterPanelObject = GameObject.FindGameObjectWithTag(Tags.UI_CHARACTER_PANEL);                        
    }

    void Start() {
        RectTransform layoutRect = mCharacterPanelObject.GetComponent<RectTransform>();
        foreach (PCBattleEntity pc in mEntityManager.pcEntities) {
            GameObject characterPortrait = (GameObject)Instantiate(characterPortraitPrefab);
            RectTransform rect = characterPortrait.GetComponent<RectTransform>();
            CharacterGUIComponent charGUI = characterPortrait.GetComponent<CharacterGUIComponent>();
            charGUI.BattleEntity = pc;
            rect.SetParent(layoutRect);
        }
    }

    void OnGUI() {
        
    }

}

