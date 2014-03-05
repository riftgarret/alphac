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
public class MoveEvent : IBattleEvent
{
	PCCharacter.RowPosition mSrcRow;
	PCCharacter.RowPosition mDestRow;

	PCBattleEntity mSrcEntity;

	public MoveEvent (PCBattleEntity srcEntity, PCCharacter.RowPosition srcRow, PCCharacter.RowPosition destRow) 
	{
		this.mSrcEntity = srcEntity;
		this.mSrcRow = srcRow;
		this.mDestRow = destRow;
	}

	public BattleEntity srcEntity {
		get {
			return mSrcEntity;
		}
	}

	public BattleEventType eventType {
		get {
			return BattleEventType.MOVE;
		}
	}

	public PCCharacter.RowPosition srcRow {
		get { return mSrcRow; }
	}

	public PCCharacter.RowPosition destRow {
		get { return mDestRow; }
	}

	public override string ToString ()
	{
		return string.Format ("[BattleMoveEvent: srcEntity={0}, eventType={1}]", srcEntity, eventType);
	}
}


