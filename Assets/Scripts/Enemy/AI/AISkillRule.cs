//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System;

[Serializable]
public class AISkillRule
{
	/// <summary>
	/// The m weight of the skill being used after all available skills are evaluated
	/// </summary>
	[SerializeField]
	private float mWeight;

	[SerializeField]
	private CombatSkillConfig mSkill;

	[SerializeField]
	private ConditionTarget mConditionTarget = ConditionTarget.PC;

	[SerializeField]
	private ConditionResolveTarget mResolvedTarget;

	[SerializeField]
	private ConditionType mConditionType = ConditionType.ANY;

	[SerializeField]
	private ClassCondition mClassCondition = ClassCondition.CLASS_FIGHTER;

	[SerializeField]
	private HitPointCondition mHitpointCondition = HitPointCondition.HP_HIGHEST;

	[SerializeField]
	private ResourceCondition mResourceCondition = ResourceCondition.RES_HIGHEST;

	[SerializeField]
	private RowCondition mRowCondition = RowCondition.BACK_COUNT_GT;

	[SerializeField]
	private StatusCondition mStatusCondition = StatusCondition.BUFF_COUNT_GT;

	[SerializeField]
	private PartyCondition mPartyCondition = PartyCondition.PARTY_COUNT_GT;

	[SerializeField]
	private float mConditionValue = 0f;
	                              
	// fields lazy created at runtime
	private IAIFilter mConditionFilter;
	private IAIFilter mTargetFilter;

	public enum ConditionType {
		ANY,
		CLASS,
		ROW,
		HP,
		RES,
		STATUS,
		PARTY
	}

	public enum ConditionTarget {
		SELF,
		ENEMY,
		PC
	}

	public enum ConditionResolveTarget {
		TARGET,
		SELF
	}

	public enum ClassCondition {
		CLASS_FIGHTER,
		CLASS_MAGE,
		CLASS_ROGUE,
		CLASS_SQUIRE
	}

	public enum HitPointCondition {
		HP_GT,
		HP_LT,
		HP_HIGHEST,
		HP_LOWEST,
		HP_DEAD
	}

	public enum ResourceCondition {
		RES_GT,
		RES_LT,
		RES_HIGHEST,
		RES_LOWEST,
		RES_EMPTY
	}

	public enum RowCondition {
		FRONT_COUNT_GT,
		FRONT_COUNT_LT,
		MIDDLE_COUNT_GT,
		MIDDLE_COUNT_LT,
		BACK_COUNT_GT,
		BACK_COUNT_LT
	}

	public enum StatusCondition {
		DEBUFF_COUNT_GT,
		DEBUFF_COUNT_LT,
		BUFF_COUNT_GT,
		BUFF_COUNT_LT,
		SELF_BLIND,
		SELF_HOARSE
	}

	public enum PartyCondition {
		PARTY_COUNT_LT,
		PARTY_COUNT_GT
	}		


	private void LazyInit() {
		if(mConditionFilter == null) {
			mTargetFilter = new AITargetFilter(mConditionTarget);

			switch(mConditionType) {
			case ConditionType.ANY:
				mConditionFilter = new AIAcceptAllFilter();
				break;
			case ConditionType.CLASS:
				mConditionFilter = new AIClassConditionFilter(mClassCondition);
				break;
			case ConditionType.HP:
				mConditionFilter = new AIHipointConditionFilter(mHitpointCondition, mConditionValue);
				break;
			case ConditionType.PARTY:
				mConditionFilter = new AIPartyConditionFilter(mPartyCondition, (int)mConditionValue);
				break;
			case ConditionType.RES:
				mConditionFilter = new AIResourceConditionFilter(mResourceCondition, mConditionValue);
				break;
			case ConditionType.ROW:
				mConditionFilter = new AIRowConditionFilter(mRowCondition, (int)mConditionValue);
				break;
			case ConditionType.STATUS:
				mConditionFilter = new AIAcceptAllFilter();
				break;
			}
		}
	}

	// TODO create a BattleAction after name has been refactored
}


