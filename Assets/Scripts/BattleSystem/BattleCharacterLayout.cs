using UnityEngine;
using System.Collections;

public class BattleCharacterLayout {

	public void DrawCharacter(float x, float y, BattleCharacterConfig config, BattleEntity entity) {
		
		// beginning of the character portrait box
		GUI.BeginGroup(new Rect(x, y, config.layoutSize.width, config.layoutSize.height));
		{	
			// the box
			GUI.Box(new Rect(0, 0, config.layoutSize.width, config.layoutSize.height), entity.character.name);
			
			// the portrait
			GUI.DrawTexture(config.portraitRect, config.portraitTexture, ScaleMode.ScaleAndCrop);
			
			// ATB gauge
			
			Rect atbRect = new Rect(config.atbGaugeRect);
			
			switch(entity.turnState.phase) {
			case TurnState.Phase.PREPARE:
				atbRect.width = Mathf.Lerp(0, config.atbGaugeRect.width, entity.turnState.turnPercent);
				break;
			case TurnState.Phase.EXECUTE: 
				break;
			case TurnState.Phase.RECOVER: 
				atbRect.width = Mathf.Lerp(0, config.atbGaugeRect.width, 1f - entity.turnState.turnPercent);
				break;
			case TurnState.Phase.REQUIRES_INPUT: 
				atbRect.width = 0;
				break;
			}
			
			GUI.DrawTexture(atbRect, config.hitPointProperties.lowHPTexture, ScaleMode.StretchToFill);
			
			// HP bar
			if(GUIHelper.MouseIsOver(config.hitpointRect)) {
				Debug.Log("hp texture");
			}
			GUI.DrawTexture(config.hitpointRect, config.hitPointProperties.highHPTexture, ScaleMode.StretchToFill);
			
		}
		GUI.EndGroup();
	}

}
