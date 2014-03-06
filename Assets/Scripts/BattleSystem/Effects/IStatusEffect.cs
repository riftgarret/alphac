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
using UnityEngine;

public interface IStatusEffect
{
	/// <summary>
	/// Gets the name of the status.
	/// </summary>
	/// <value>The name of the status.</value>
	string statusName {
		get;
	}

	string statusKey {
		get;
	}

	// TODO tooltip text and icon
	
	/// <summary>
	/// The Source Type is used to determine which resist to check to see if 
	/// the effect is resisted
	/// </summary>
	/// <value>The type of the source.</value>
	StatusEffectType sourceType {
		get;
	}

	/// <summary>
	/// Gets the type of the effect. Used to store into maps for fast access to determine effect
	/// </summary>
	/// <value>The type of the effect.</value>
	EffectType effectType {
		get;
	}

	/// <summary>
	/// Gets a value indicating whether this <see cref="IStatusEffect"/> is dispellable. This is ignored if sourceType is NEGATIVE_PHYSICAL
	/// </summary>
	/// <value><c>true</c> if is dispellable; otherwise, <c>false</c>.</value>
	bool isDispellable {
		get;
	}

	/// <summary>
	/// Gets a value indicating whether this <see cref="IStatusEffect"/> is curable. This is ignored if sourceType is POSITIVE
	/// </summary>
	/// <value><c>true</c> if is curable; otherwise, <c>false</c>.</value>
	bool isCurable {
		get;
	}

	/// <summary>
	/// Gets the total length of the duration. How long should it exist before it removes naturally
	/// </summary>
	/// <value>The total length of the duration.</value>
	float totalDurationLength {
		get;
	}	

	/// <summary>
	/// Gets the length of the current duration.
	/// </summary>
	/// <value>The length of the current duration.</value>
	float currentDurationLength {
		get;
	}

	/// <summary>
	/// Texts that should appear in the combat log
	/// </summary>
	/// <returns>The on attach.</returns>
	/// <param name="entity">Entity.</param>
	string GetTextOnAttach(BattleEntity entity);

	/// <summary>
	/// Texts the on dettach.
	/// </summary>
	/// <returns>The on dettach.</returns>
	/// <param name="entity">Entity.</param>
	string GetTextOnDettach(BattleEntity entity);

	/// <summary>
	/// Increments the duration time.
	/// </summary>
	/// <param name="timeDelta">Time delta.</param>
	void IncrementDurationTime(float timeDelta);	
}


