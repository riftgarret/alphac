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

public class StatusEffectHoarse : StatusEffectSpellFailure
{
	public const string STATUS_NAME = "spell failure magic";
	private const string EFFECT_NAME = "Hoarse";

	public StatusEffectHoarse(float modValue, float duration) : base(modValue, duration) {

	}

	public override string GetTextOnAttach (BattleEntity entity)
	{
		return string.Format("{0} feels his/her voice go hoarse.");
	}

	public override string GetTextOnDettach (BattleEntity entity)
	{
		return string.Format("{0} feels his/her voice go return to normal.");
	}

	public override EffectSourceType sourceType {
		get {
			return EffectSourceType.NEGATIVE_MAGICAL;
		}
	}

	public override string statusName {
		get {
			return EFFECT_NAME;
		}
	}

	public override bool isCurable {
		get {
			return true;
		}
	}

	public override string statusKey {
		get {
			return STATUS_NAME;
		}
	}

	public override bool isDispellable {
		get {
			return true;
		}
	}	
}
