using UnityEngine;
using System.Collections;

public abstract class BattleAction {		

	// time it takes to prepare, configured and set
	public float timePrepare;
	public float timeAction;
	public float timeRecover;
	
	protected BattleAction(float prepare, float action, float recover) {
		timePrepare = prepare;
		timeAction = prepare;
		timeRecover = recover;
	}

	/// <summary>
	/// Important to note action clock should always be called even when the delta time has passed.
	/// the action time threshold, it will be called one last time
	/// </summary>
	/// <param name="actionClock">Action clock.</param>
	public abstract void DoAction(float actionClock);

	/// <summary>
	/// To complete action, not useful in current stage.
	/// </summary>
	/// <value>The total time.</value>
	public float TotalTime {
		get {
			return timeAction + timePrepare + timeRecover;
		}
	}
}
