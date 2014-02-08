using System.Collections;
using UnityEngine;

public class BattleTargetingLayout
{
	public BattleTargetingLayout ()
	{
	}

	public void DrawTargets(PCTurnManager turnManager) {

		// just paint in middle of screen for now
		SelectableTargetManager targetManager = turnManager.currentTargetManager;
		int count = targetManager.targetList.Count;

		float cursor = (Screen.width - (count * 50f) )/2f;
		for(int i=0; i < count; i++) {
			DrawTarget(cursor, 0, turnManager, targetManager.targetList[i]);
			cursor += 50f;
		}

	}

	private void DrawTarget(float x, float y, PCTurnManager turnManager, SelectableTarget selectableTarget) {
		float width = 50f;
		float height = 40f;

		Rect rect = new Rect(x, y, width, height);

		if(GUI.Button(rect, new GUIContent(selectableTarget.targetName))) {
			turnManager.SelectTarget(selectableTarget);
		}
	}
}


