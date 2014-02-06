using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class CustomAssetEditor {

	private static System.Type ProjectWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ObjectBrowser");
	private static EditorWindow projectWindow = null;

	[MenuItem("Assets/Create/Character Class")]	
	public static void CreateClassAsset() {		
		CharacterClass asset = CharacterClass.CreateInstance<CharacterClass>();
		CompleteAssetCreation(asset, "CharacterClass");
	}

	[MenuItem("Assets/Create/Equipment/Weapon")]	
	public static void CreateWeaponAsset() {		
		Weapon asset = CharacterClass.CreateInstance<Weapon>();
		CompleteAssetCreation(asset, "Weapon");
	}

	[MenuItem("Assets/Create/NPC/Enemy Character")]	
	public static void CreateNPCAsset() {
		NPCCharacter asset = NPCCharacter.CreateInstance<NPCCharacter>();
		CompleteAssetCreation(asset, "NPC");
	}	

	[MenuItem("Assets/Create/NPC/Enemy Party")]	
	public static void CreateEnemyPartyAsset() {		
		PCParty asset = CharacterClass.CreateInstance<PCParty>();
		CompleteAssetCreation(asset, "Enemy Party");
	}

	[MenuItem("Assets/Create/Test/PC Character")]	
	public static void CreateTestPCAsset() {
		PCCharacter asset = NPCCharacter.CreateInstance<PCCharacter>();
		CompleteAssetCreation(asset, "NPC");
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
}
