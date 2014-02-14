using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class CombatSkillConfig : SkillConfig {
	[SerializeField]
	private CombatSkillActionEnum mCombatSkillEnum = CombatSkillActionEnum.BASIC_FIGHT;
	public CombatSkillActionEnum combatSkillEnum { get { return mCombatSkillEnum; } }
	
	[SerializeField]
	private TargetFilter mTargetFilter = TargetFilter.REQUIRE_ALIVE;
	public TargetFilter targetFilter { get { return mTargetFilter; } }

	[SerializeField]
	private TargetingType mPrimaryTargetType = TargetingType.SINGLE;
	public TargetingType primaryTargetType { get { return mPrimaryTargetType; } }

	[SerializeField]
	private TargetStart mInitialTarget = TargetStart.ENEMY;
	public TargetStart initialTarget { get { return mInitialTarget; } }
	
	[SerializeField]
	private float mTimePrepare = 1f;
	public float timePrepare { get { return mTimePrepare; } }

	[SerializeField]
	private float mTimeAction = 1f;
	public float timeAction { get { return mTimeAction; } }

	[SerializeField]
	private float mTimeRecover = 1f;
	public float timeRecover { get { return mTimeRecover; } }	

	[SerializeField]
	private StatModifier [] mStatModifiers = null;
	public StatModifier [] statModifiers {
		get { return mStatModifiers; }
	}

	/// <summary>
	/// Gets the type of the combat skill. Useful to determining the event skill to initiate
	/// </summary>
	/// <value>The type of the combat skill.</value>
	public abstract CombatSkillType combatSkillType {
		get;
	}

	/// <summary>
	/// Creates the combat skill.
	/// </summary>
	/// <returns>The combat skill.</returns>
	public abstract CombatSkill CreateCombatSkill(int level);
}
