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

public class CharacterCombatNode : ConfigurableCombatNode
{
	public CharacterCombatNode (Character c) : base()
	{
		mPropertyAdd [(int)OffensiveCombatNodeProperty.ACCURACY] = c.accuracy;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.CRIT_CHANCE] = c.critChance;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.STR] = c.strength;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.VIT] = c.vitality;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.DEX] = c.dexerity;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.AGI] = c.agility;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.INT] = c.inteligence;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.WIS] = c.wisdom;
		mPropertyAdd [(int)OffensiveCombatNodeProperty.LUCK] = c.luck;
	}
}

