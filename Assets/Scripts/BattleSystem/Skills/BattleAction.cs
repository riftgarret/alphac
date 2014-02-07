using UnityEngine;
using System.Collections;

public abstract class BattleAction : IBattleAction {		

	// time it takes to prepare, configured and set
	public float timePrepare { get { return combatSkill.combatSkillConfig.timePrepare; } } 
	public float timeAction { get { return combatSkill.combatSkillConfig.timeAction; } }  
	public float timeRecover { get { return combatSkill.combatSkillConfig.timeRecover; } } 

	protected readonly CombatSkill combatSkill;
			
	/// <summary>
	/// The target entity. This may be null if we are targeting a group.
	/// </summary>
	protected readonly SelectableTarget target;

	protected readonly BattleEntity sourceEntity;
	
	protected BattleAction(CombatSkill skill, BattleEntity sourceEntity, SelectableTarget target) {
		this.combatSkill = skill;
		this.sourceEntity = sourceEntity;
		this.target = target;
	}

	public abstract void OnExecuteAction(float actionClock);

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
