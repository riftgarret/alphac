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


public class DamageLogic : BaseCombatLogic, ICombatLogic {

	private ElementVector m_Defense;
	private ElementVector m_Damage;

	private CritChanceLogic m_CritResult;
	private ElementVector m_CritDamage;

	private float m_HpBefore;
	private float m_HpAfter;
	

	public void Execute (CombatResolver src, CombatResolver dest)
	{
		CheckExecute ();
		// pull out min and max damage and calculated 'rolled damage'
		ElementVector min = src.DamageMin;        
		ElementVector max = src.DamageMax;
		float rand = UnityEngine.Random.Range(0f, 1f);
		ElementVector diff = max - min;
		ElementVector randomDmg = min + (diff * rand);
		
		// scale damage by vector, this could have been done earlier, same results
		AttributeVector attributes = src.Attributes;
		AttributeVector damageAttributeScalar = src.DamageAttributeScalar;
		AttributeVector resultDamageExtra = attributes * damageAttributeScalar;
		float scaleDamage = resultDamageExtra.Sum;
		
		// scale damage should be < 1 normally, so we want to ensure this is positive on scaling
		m_Damage = randomDmg * (1 + scaleDamage); 
		
		// scale on dmg bonus
		m_Damage += src.ElementAttackRaw;
		m_Damage *= src.ElementAttackScalar;
		
		// if is critical 
		m_CritDamage = new ElementVector();

		m_CritResult = new CritChanceLogic ();
		m_CritResult.Execute (src, dest);

		if (m_CritResult.Hits) {
			float critScale = CombatUtil.CalculateCritDamageScale(src, dest);
			m_CritDamage = (m_Damage * critScale);
		}
		
		m_HpBefore = dest.entity.currentHP;

		m_Defense = dest.ElementDefense;
		
		// apply dmg
		dest.entity.currentHP -= CombatUtil.CalculateDamage (m_Damage, m_CritDamage, m_Defense);
		
		m_HpAfter = dest.entity.currentHP;

		Logger.d (this, this);
	}

	public void GenerateEvents (CombatResolver src, CombatResolver dest, Queue<IBattleEvent> combatEvents)
	{
		// forward to combat events
		m_CritResult.GenerateEvents (src, dest, combatEvents);

		combatEvents.Enqueue(new DamageEvent (src.entity, dest.entity, m_Damage, m_CritDamage, m_Defense));

		if (m_HpBefore > 0 && m_HpAfter <= 0) {
			combatEvents.Enqueue(new DeathEvent(dest.entity));
		}
	}

	public override string ToString ()
	{
		return string.Format ("[DamageLogic: m_Defense={0}, m_Damage={1}, m_CritResult={2}, m_CritDamage={3}, m_HpBefore={4}, m_HpAfter={5}]", m_Defense, m_Damage, m_CritResult, m_CritDamage, m_HpBefore, m_HpAfter);
	}
	
}
