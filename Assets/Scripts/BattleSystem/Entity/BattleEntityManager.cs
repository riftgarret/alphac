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
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A class to manage the different combinations of entities in fast native arrays
/// </summary>
public class BattleEntityManager : BattleEntity.OnDecisionRequiredListener
{
	private EnemyBattleEntity[] mEnemyEntities;
	public EnemyBattleEntity[] enemyEntities {
		get { return mEnemyEntities; }
	}

	private PCBattleEntity[] mPcEntities;
	public PCBattleEntity[] pcEntities {
		get { return mPcEntities; }
	}

	private BattleEntity[] mAllEntities;
	public BattleEntity[] allEntities {
		get { return mAllEntities; }
	}

	private PCBattleEntity[] mFrontRowEntities;
	public PCBattleEntity[] frontRowEntities {
		get { return mFrontRowEntities; } 
	}

	private PCBattleEntity[] mMiddleRowEntities;
	public PCBattleEntity[] middleRowEntities {
		get { return mMiddleRowEntities; } 
	}

	private PCBattleEntity[] mBackRowEntities;
	public PCBattleEntity[] backRowEntities {
		get { return mBackRowEntities; } 
	}

	/// <summary>
	/// Raises the row update event. Should be called upon listening to row changes.
	/// </summary>
	/// <param name="character">Character.</param>
	public void OnRowUpdate(PCCharacter character) {
		// re-evaluate all rows
		BuildRowEntities();
	}

	/// <summary>
	/// Gets the PCBattleEntities for this row.
	/// </summary>
	/// <returns>The row.</returns>
	/// <param name="rowPos">Row position.</param>
	public PCBattleEntity[] GetRow(PCCharacter.RowPosition rowPos) {
		switch(rowPos) {
		case PCCharacter.RowPosition.FRONT:
			return mFrontRowEntities;
		case PCCharacter.RowPosition.MIDDLE:
			return mMiddleRowEntities;
		case PCCharacter.RowPosition.BACK:
			return mBackRowEntities;
		}
		return null;
	}


	public BattleEntityManager (PCCharacter[] pcChars, EnemyCharacter[] enemyChars) {
		// combine 
		mAllEntities = new BattleEntity[pcChars.Length + enemyChars.Length];		
		mPcEntities = new PCBattleEntity[pcChars.Length];
		mEnemyEntities = new EnemyBattleEntity[enemyChars.Length];

		for(int i=0; i < mPcEntities.Length; i++) {
			mPcEntities[i] = new PCBattleEntity(pcChars[i], this);
			mAllEntities[i] = mPcEntities[i];
		}

		for(int i=0; i < mEnemyEntities.Length; i++) {
			mEnemyEntities[i] = new EnemyBattleEntity(enemyChars[i], this);
			mAllEntities[pcChars.Length + i] = mEnemyEntities[i];
		}
		// create row specifics
		BuildRowEntities();
	}

	/// <summary>
	/// Builds the row entities. This is to optimize our logic for testing if row is still valid
	/// </summary>
	private void BuildRowEntities() {
		List<PCBattleEntity> frontRow = new List<PCBattleEntity>();
		List<PCBattleEntity> midRow = new List<PCBattleEntity>();
		List<PCBattleEntity> backRow = new List<PCBattleEntity>();

		foreach(PCBattleEntity entity in mPcEntities) {
			switch(entity.pcCharacter.rowPosition) {
			case PCCharacter.RowPosition.FRONT:
				frontRow.Add(entity);
				break;
			case PCCharacter.RowPosition.MIDDLE:
				midRow.Add(entity);
				break;
			case PCCharacter.RowPosition.BACK:
				backRow.Add(entity);
				break;

			}
		}
		mFrontRowEntities = frontRow.ToArray();
		mMiddleRowEntities = midRow.ToArray();
		mBackRowEntities = backRow.ToArray();
	}



    public void OnDecisionRequired(BattleEntity entity) {
        BattleSystem.Instance.PostActionRequired(entity);
    }
}