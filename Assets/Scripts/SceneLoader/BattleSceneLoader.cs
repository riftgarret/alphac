//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

/// <summary>
/// Battle scene loader.
/// </summary>
public class BattleSceneLoader
{
	/// <summary>
	/// The enemy party.
	/// </summary>
	private static BattleSceneParameters sParameter;

	/// <summary>
	/// Store the parameters off and load the scene. Its up to the main acting object of that scene to reload these params
	/// </summary>
	/// <param name="enemyParty">Enemy party.</param>
	/// <param name="pcParty">Pc party.</param>
	public static void LoadScene(EnemyPartySO enemyParty, PCPartySO pcParty) {
		sParameter = new BattleSceneParameters();
		sParameter.enemyParty = enemyParty;
		sParameter.pcParty = pcParty;
		Application.LoadLevel("battle");
	}

	/// <summary>
	/// Consumes the parameters. 
	/// </summary>
	/// <returns>The parameters.</returns>
	public static BattleSceneParameters ConsumeParameters() {
		BattleSceneParameters p = sParameter;
		sParameter = null;	// reset so we have consumed it
		return p;
	}

	public class BattleSceneParameters {
		public EnemyPartySO enemyParty;
		public PCPartySO pcParty;
	}
}

