using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class CharacterGUIComponent : MonoBehaviour {

    public Slider actionSlider;
    public Slider hpSlider;
    public Slider resourceSlider;
    public Image portraitImage;
    public Text characterTitle;

    private BattleEntity mBattleEntity;
    public BattleEntity BattleEntity {
        get { return mBattleEntity; }
        set {
            mBattleEntity = value;
			characterTitle.text = mBattleEntity.character.displayName;
            hpSlider.maxValue = mBattleEntity.maxHP;
            hpSlider.minValue = 0;
        }
    }            
    

    void OnGUI() {
        if (mBattleEntity == null) {
            return;
        }

        actionSlider.value = mBattleEntity.turnState.turnPercent;
    }

}

