using UnityEngine;
using System.Collections;

public class BattleSkillButtonsLayout {
	public void DrawButton(float x, float y, BattleSkillButtonConfig config, BattleEntity entity, HotKey hotkey) {
		
		// beginning of the character portrait box
		GUI.BeginGroup(new Rect(x, y, config.layoutSize.width, config.layoutSize.height));
		{	
			Rect buttonRect = new Rect(0, 0, config.layoutSize.width, config.layoutSize.height);
			GUI.Box(buttonRect, GUIContent.none);

			if(entity != null && hotkey != null) {
				if(GUI.Button(buttonRect, entity.character.name)) {
					
				}
			}
		}
		GUI.EndGroup();
	}
}
