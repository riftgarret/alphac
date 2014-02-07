using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class CombatSkillConfig : SkillConfig {

	[SerializeField]
	private TargetingType mPrimaryTargetType;
	public TargetingType primaryTargetType { get { return mPrimaryTargetType; } }

	[SerializeField]
	private TargetStart mInitialTarget;
	public TargetStart initialTarget { get { return mInitialTarget; } }

	[SerializeField]
	private CombatSkillActionEnum mCombatSkillEnum;
	public CombatSkillActionEnum combatSkillEnum { get { return mCombatSkillEnum; } }

	[SerializeField]
	private float mTimePrepare;
	public float timePrepare { get { return mTimePrepare; } }

	[SerializeField]
	private float mTimeAction;
	public float timeAction { get { return mTimeAction; } }

	[SerializeField]
	private float mTimeRecover;
	public float timeRecover { get { return mTimeRecover; } }	
}
