using UnityEngine;
using System.Collections;

[System.Serializable]
public class BattleCharacterConfig {
	// position of where this character should be calculated onto the screen
	public int characterPositionIndex;
	
	// hitpoint properties
	public HitPointProps hitPointProperties;
	
	// portrait texture, probably will be dynamically linked later
	public Texture2D portraitTexture;
	
	// size of the whole character model
	public Size layoutSize = new Size(100, 120);
	
	// rect of localtion inside portrait
	public Rect portraitRect = new Rect(10, 25, 80, 65);

	// ATB gauge
	public Rect atbGaugeRect = new Rect(10, 90, 80, 10);

	// rect of hp inside portrait
	public Rect hitpointRect = new Rect(10, 100, 80, 10);
	
}
