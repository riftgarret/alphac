//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI party condition filter. This is under the impression we've already filtered our targets to group
/// </summary>
public class AIPartyConditionFilter : IAIFilter
{
	private AISkillRule.PartyCondition mPartyCondition;
	private int mPartyCount;

	public AIPartyConditionFilter (AISkillRule.PartyCondition partyCondition, int valueCount) 
	{
		mPartyCondition = partyCondition;
		mPartyCount = valueCount;
	}

	public void FilterEntities (BattleEntity sourceEntity, HashSet<BattleEntity> entities)
	{
		// leftover targets should already be in the party, we should just count and filter out the rest if its needed
		switch(mPartyCondition) {
		case AISkillRule.PartyCondition.PARTY_COUNT_GT:
			if(entities.Count <= mPartyCount) {
				entities.Clear();
			}
			break;
		case AISkillRule.PartyCondition.PARTY_COUNT_LT:
			if(entities.Count >= mPartyCount) {
				entities.Clear();
			}
			break;
		}
	}
}