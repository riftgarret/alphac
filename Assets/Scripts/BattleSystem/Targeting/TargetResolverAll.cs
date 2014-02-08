using System;
using System.Collections;
using System.Collections.Generic;

public class TargetResolverAll : ITargetResolver
{
	// only used when the target is single
	private bool mIsEnemy;
	private BattleEntityManager mBattleEntityManager;
	
	/// <summary>
	/// For single targets
	/// </summary>
	/// <param name="targetEnum">Target enum.</param>
	/// <param name="entityManager">Entity manager.</param>
	public TargetResolverAll (bool isEnemy, BattleEntityManager manager)
	{
		mIsEnemy = isEnemy;
		mBattleEntityManager = manager;
	}	

	private BattleEntity[] targetEntities {
		get {
			if(mIsEnemy) {
				return mBattleEntityManager.enemyEntities;
			}
			return mBattleEntityManager.pcEntities;
		}
	}
	
	public bool isValidTarget (CombatSkill skill)
	{
		foreach(BattleEntity entity in targetEntities) {
			bool passed = TargetConditionFilter.PassesFilter(entity, skill.combatSkillConfig.targetFilter);
			if(passed) {
				return true;
			}
		}
		return false;
	}
	
	public BattleEntity[] GetTargets(CombatSkill skill) {
		List<BattleEntity> filteredEntities = new List<BattleEntity>();
		foreach(BattleEntity entity in targetEntities) {
			bool passed = TargetConditionFilter.PassesFilter(entity, skill.combatSkillConfig.targetFilter);
			if(passed) {
				filteredEntities.Add(entity);
			}
		}
		return filteredEntities.ToArray();
	}
}