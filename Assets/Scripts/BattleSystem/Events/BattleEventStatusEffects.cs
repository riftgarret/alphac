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

public class BattleEventStatusEffects
{
	public static readonly BattleEventStatusEffects EMPTY = new BattleEventStatusEffects();
	
	public StatusEffectEvent[] statusEffectEvents {
				get;
				private set;
	}
	
	private BattleEventStatusEffects () {	}

	/// <summary>
	/// Builder this instance.
	/// </summary>
	public static StatusEffectBuilder Builder() {
		return new StatusEffectBuilder();
	}

	public class StatusEffectBuilder {
		private List<StatusEffectEvent> statusEffectEvents = null;

		public StatusEffectBuilder AddStatusEffect(IStatusEffect effect, BattleEntity target, StatusEffectEvent.StatusEffectRule rule) {
			if(statusEffectEvents == null) {
				statusEffectEvents = new List<StatusEffectEvent>();
			}
			statusEffectEvents.Add(new StatusEffectEvent(effect, target, rule));
			return this;
		}

		public BattleEventStatusEffects Build() {
			BattleEventStatusEffects ret = new BattleEventStatusEffects();
			if(statusEffectEvents != null) {
				ret.statusEffectEvents = statusEffectEvents.ToArray();
			}
			return ret;
		}
	}
}
