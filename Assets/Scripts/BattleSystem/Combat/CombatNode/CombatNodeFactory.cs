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
using System.Collections.Generic;

public class CombatNodeFactory
{
	private BattleEntity mEntity;

	private Dictionary<WeaponConfig, WeaponConfigCombatNode> mCachedWeaponConfigMap;
	private CharacterCombatNode mCachedCharacterNode;

	public CombatNodeFactory (BattleEntity entity)
	{
		mEntity = entity;
		mCachedWeaponConfigMap = new Dictionary<WeaponConfig, WeaponConfigCombatNode> ();
	}

	public ICombatNode CreateWeaponConfigNode(int weaponIndex) {
		Weapon weapon = mEntity.GetWeapon (weaponIndex);
		WeaponConfig config = weapon.weaponConfig;
		if (!mCachedWeaponConfigMap.ContainsKey (config)) {
			mCachedWeaponConfigMap[config] = new WeaponConfigCombatNode(config);
		}
		return mCachedWeaponConfigMap [config];
	}

	public ICombatNode CreateCharacterNode() {
		if (mCachedCharacterNode == null) {
			mCachedCharacterNode = new CharacterCombatNode(mEntity.character);
		}
		return mCachedCharacterNode;
	}
}
