using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class CustomAssetEditor {

	private static System.Type ProjectWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ObjectBrowser");
	private static EditorWindow projectWindow = null;

	[MenuItem("Assets/Create/Character Class")]	
	public static void CreateClassAsset() {		
		CompleteAssetCreation(X.CreateInstance<CharacterClassSO>(), "CharacterClass");
	}

	[MenuItem("Assets/Create/Equipment/Weapon")]	
	public static void CreateWeaponAsset() {		
		CompleteAssetCreation(X.CreateInstance<WeaponSO>(), "Weapon");
	}

	[MenuItem("Assets/Create/Enemy/Enemy Character Config")]	
	public static void CreateNPCAsset() {
		CompleteAssetCreation(X.CreateInstance<EnemyCharacterSO>(), "EnemyConfig");
	}	

	[MenuItem("Assets/Create/Enemy/Enemy Party")]	
	public static void CreateEnemyPartyAsset() {		
		CompleteAssetCreation(X.CreateInstance<EnemyParty>(), "Enemy Party");
	}

	[MenuItem("Assets/Create/Enemy/Enemy Skill Set")]	
	public static void CreateEnemySkillSetSetAsset() {
		CompleteAssetCreation(X.CreateInstance<EnemySkillSetSO>(), "EnemySkillSet");
	}
	/*
	[MenuItem("Assets/Create/Enemy/Enemy AI Rule")]	
	public static void CreateEnemyAIRuleSetAsset() {
		CompleteAssetCreation(X.CreateInstance<AISkillRule>(), "EnemyAIRule");
	}
	*/

	[MenuItem("Assets/Create/Skill/Physical Combat Skill Config")]	
	public static void CreatePhysicalCombatSkillAsset() {
		CompleteAssetCreation(X.CreateInstance<PhysicalCombatSkillSO>(), "PhysicalCombatSkill");
	}	

	[MenuItem("Assets/Create/Skill/Magical Combat Skill Config")]	
	public static void CreateMagicalCombatSkillAsset() {
		CompleteAssetCreation(X.CreateInstance<MagicalCombatSkillSO>(), "MagicalCombatSkill");
	}

	[MenuItem("Assets/Create/PC/PC Skill Set Config")]	
	public static void CreatePCSkillSetAsset() {
		CompleteAssetCreation(X.CreateInstance<PCSkillSetSO>(), "SkillSet");
	}

	[MenuItem("Assets/Create/Test/PC Character Config")]	
	public static void CreateTestPCAsset() {
		CompleteAssetCreation(X.CreateInstance<PCCharacterSO>(), "NPC");
	}	



	private static void CompleteAssetCreation(ScriptableObject asset, string entityName) {
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);		
		if (path == "") {			
			path = "Assets";		
		}		
		else if (Path.GetExtension(path) != "") {			
			path = path.Replace(Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");		
		}		
		AssetDatabase.CreateAsset (asset, AssetDatabase.GenerateUniqueAssetPath (path + "/New " + entityName + ".asset"));

		Selection.activeObject = asset;  
		EditorUtility.FocusProjectWindow();		
		//StartRenameSelectedAsset();
	}

	public static void StartRenameSelectedAsset()
	{
		if (projectWindow == null) {
			projectWindow = EditorWindow.GetWindow(ProjectWindowType);
		}
		//should never be null but still ;)
		if (projectWindow != null) {
			var e = new Event();
			e.keyCode = KeyCode.F2;
			e.type = EventType.KeyDown;
			projectWindow.SendEvent(e);
		}
	}

	private class X : ScriptableObject{}
}
