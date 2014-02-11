using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class CombatSkillConfig : SkillConfig {

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
	private CombatSkillActionEnum mCombatSkillEnum = CombatSkillActionEnum.BASIC_FIGHT;
	public CombatSkillActionEnum combatSkillEnum { get { return mCombatSkillEnum; } }

	[SerializeField]
	private OffensiveModifier [] mOffensiveModifiers = null;
	public OffensiveModifier [] offensiveModifiers { get { return mOffensiveModifiers; } }

	[SerializeField]
	private float mTimePrepare = 1f;
	public float timePrepare { get { return mTimePrepare; } }

	[SerializeField]
	private float mTimeAction = 1f;
	public float timeAction { get { return mTimeAction; } }

	[SerializeField]
	private float mTimeRecover = 1f;
	public float timeRecover { get { return mTimeRecover; } }	
}
