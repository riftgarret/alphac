using UnityEngine;
using System.Collections;

public class BattleSkillButtonsLayout {

	public void DrawButtons(PCTurnManager turnManager, BattleSkillButtonConfig config) {
		HotKey[] hotkeys = turnManager.currentEntity.pcCharacter.skillSet.hotKeys;

		float layoutWidth = config.layoutSize.width;
		for(int hotkeyIndex=0; hotkeyIndex < hotkeys.Length; hotkeyIndex++) {
			float x = (Screen.width - (layoutWidth * 0.5f * (float)hotkeys.Length)) / 2f + (layoutWidth * hotkeyIndex);
			float y = Screen.height - config.layoutSize.height;

			DrawButton(x, y, config, turnManager, hotkeys[hotkeyIndex]);
		}
	}

	private void DrawButton(float x, float y, BattleSkillButtonConfig config, PCTurnManager turnManager, HotKey hotkey) {
		
		// beginning of the character portrait box
		GUI.BeginGroup(new Rect(x, y, config.layoutSize.width, config.layoutSize.height));
		{	
			Rect buttonRect = new Rect(0, 0, config.layoutSize.width, config.layoutSize.height);
			GUI.Box(buttonRect, GUIContent.none);

			if(hotkey.skill != null && hotkey.skill is CombatSkill) {
				if(GUI.Button(buttonRect, hotkey.skill.combatSkillConfig.displayName)) {
					Debug.Log("Skill Picked : " + hotkey.skill.combatSkillConfig.displayName);
					turnManager.SelectSkill(hotkey.skill);
				}
			}
		}
		GUI.EndGroup();
	}
}
