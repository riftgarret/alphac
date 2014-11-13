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


public class HitChanceLogic : AccuracyEvasionLogic, ICombatLogic {
	
	public void Execute (CombatResolver src, CombatResolver dest)
	{
		CheckExecute ();
		m_Accuracy = src.CombatStats.accuracy;
		m_Evasion = dest.CombatStats.evasion;
		m_ChanceToHit =  m_Accuracy / Math.Max(m_Accuracy + m_Evasion, 1);
		m_RandomValue = UnityEngine.Random.Range(0f, 1f);
		Logger.d (this, this);
	}

	public void GenerateEvents (CombatResolver src, CombatResolver dest, Queue<IBattleEvent> combatEvents)
	{
		if (!Hits) {
			combatEvents.Enqueue(new DodgeEvent(src.entity, dest.entity));
		}
	}
}