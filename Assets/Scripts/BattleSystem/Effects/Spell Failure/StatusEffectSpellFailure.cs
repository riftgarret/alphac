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

public abstract class StatusEffectSpellFailure : BasicStatusEffect, IModifierStatusEffect
{
	private float mModValue;


	public StatusEffectSpellFailure(float modValue, float totalDuration) : base(totalDuration) {
		mModValue = modValue;
	}


	public override EffectType effectType {
		get {
			return EffectType.SPELL_FAILURE;
		}
	}
	
	public float modValue {
		get {
			return mModValue;
		}
	}
}
