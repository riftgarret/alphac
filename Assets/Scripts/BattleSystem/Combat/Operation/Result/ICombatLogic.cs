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


public interface ICombatLogic {
	void Execute(CombatResolver src, CombatResolver dest);
	void GenerateEvents(CombatResolver src, CombatResolver dest, Queue<IBattleEvent> combatEvents);
	bool IsExecuted { get; }
}