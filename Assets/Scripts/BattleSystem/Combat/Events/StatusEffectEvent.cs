// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;

public class StatusEffectEvent : IBattleEvent
{
	private BattleEntity mSrcEntity;
	private BattleEntity mDestEntity;
	private IStatusEffect mStatusEffect;

	public StatusEffectEvent (BattleEntity srcEntity, BattleEntity destEntity, IStatusEffect statusEffect) 
	{
		mSrcEntity = srcEntity;
		mDestEntity = destEntity;
		mStatusEffect = statusEffect;
	}

	public BattleEntity srcEntity {
		get {
			return mSrcEntity;
		}
	}

	public BattleEntity destEntity {
		get {
			return mDestEntity;
		}
	}

	public IStatusEffect statusEffect {
		get {
			return mStatusEffect;
		}
	}

	public BattleEventType eventType {
		get {
			return BattleEventType.STATUS_EFFECT;
		}
	}

	public override string ToString ()
	{
		return string.Format ("[StatusEffectEvent: mSrcEntity={0}, mDestEntity={1}, mStatusEffect={2}]", mSrcEntity, mDestEntity, mStatusEffect);
	}
	
}
