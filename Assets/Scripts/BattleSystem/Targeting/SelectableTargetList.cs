using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectableTargetList {

	private const string ALL_ENEMY_TEXT = "All Enemies";
	private const string CURRENT_ROW_TEXT = "Current Row";
	private const string ALL_ALLIES_TEXT = "All Allies";
	
	public static SelectableTargetList CreateAllowedTargets(BattleEntity origin, IBattleTargetProvider provider, CombatSkill skill) {
		List<SelectableTarget> selectableTargets = new List<SelectableTarget>();
		switch(skill.combatSkillConfig.primaryTargetType) {
		case TargetingType.ALL:
			selectableTargets.Add(new SelectableTarget(ALL_ENEMY_TEXT, new List<BattleEntity>(provider.GetTargets(false))));
			selectableTargets.Add(new SelectableTarget(ALL_ALLIES_TEXT, new List<BattleEntity>(provider.GetTargets(true))));
			break;
		case TargetingType.SINGLE: 
			foreach(BattleEntity entity in provider.GetTargets(false)) {
				selectableTargets.Add(new SelectableTarget(entity.character.displayName, new List<BattleEntity>(new BattleEntity[]{entity})));
			}
			foreach(BattleEntity entity in provider.GetTargets(true)) {
				selectableTargets.Add(new SelectableTarget(entity.character.displayName, new List<BattleEntity>(new BattleEntity[]{entity})));
			}
			break;
		case TargetingType.SELF:
			selectableTargets.Add(new SelectableTarget(origin.character.displayName, new List<BattleEntity>(new BattleEntity[]{origin})));
			break;
		default:
			selectableTargets.Add(new SelectableTarget("NOT IMPLEMENTED", new List<BattleEntity>()));
			break;
		}

		return new SelectableTargetList(skill, selectableTargets);
	}

	/// <summary>
	/// Gets the skill.
	/// </summary>
	/// <value>The skill.</value>
	public CombatSkill skill {
		private set;
		get;
	}

	public List<SelectableTarget> selectableTargets {
		private set;
		get;
	}

	private SelectableTargetList(CombatSkill skill, List<SelectableTarget> selectableList) {
		this.skill = skill;
		this.selectableTargets = selectableList;
	}
}
