using UnityEngine;
using System.Collections;

public abstract class BattleAction : IBattleAction {		

	// time it takes to prepare, configured and set
	public float timePrepare { get { return combatSkill.combatSkillConfig.timePrepare; } } 
	public float timeAction { get { return combatSkill.combatSkillConfig.timeAction; } }  
	public float timeRecover { get { return combatSkill.combatSkillConfig.timeRecover; } } 

	public readonly CombatSkill combatSkill;
			
	/// <summary>
	/// The target entity. This may be null if we are targeting a group.
	/// </summary>
	public readonly ITargetResolver targetResolver;

	public readonly BattleEntity sourceEntity;
	
	protected BattleAction(CombatSkill skill, BattleEntity sourceEntity, ITargetResolver targetResolver) {
		this.combatSkill = skill;
		this.sourceEntity = sourceEntity;
		this.targetResolver = targetResolver;
	}

	public abstract void OnExecuteAction(float actionClock, BattleEventManager eventManager);

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