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

public class CombatResolver
{
	private BattleEntity mEntity;
	private ICombatNode mRootNode;

	public CombatResolver (BattleEntity entity) : this(entity, entity.CreateDefaultCombatNode()) {
	}

	public CombatResolver (BattleEntity entity, ICombatNode rootNode)
	{
		mRootNode = rootNode;
		mEntity = entity;
	}

	public BattleEntity entity {
		get { return mEntity; }
	}

	public float GetProperty(CombatPropertyType property) {
		float ret = mRootNode.GetPropertyAdd (property);
		ret *= mRootNode.GetPropertyMultiply (property);
		return ret;
	}

	public float GetAccuracy() {
		return GetProperty(CombatPropertyType.ACCURACY);
	}

	public float GetPhysicalDamage() {
		float totalDamage = GetProperty (CombatPropertyType.TOTAL_DAMAGE);

		float strScale = GetProperty (CombatPropertyType.MAG_SCALE_STR);
		if (strScale >= 0) {

		}
		return totalDamage;
	}

	public float GetMagicalDamage() {
		float totalDamage = GetProperty (CombatPropertyType.TOTAL_DAMAGE);
		return totalDamage;
	}

	public float GetPhysicalPower() {
		return GetProperty(CombatPropertyType.POWER_PHYSICAL);
	}

	public float GetMagicalPower() {
		return GetProperty(CombatPropertyType.POWER_MAGIC);
	}

	/// <summary>
	/// Gets the crit chance.
	/// </summary>
	/// <returns>The crit chance.</returns>
	public float GetCritChance() {
		return GetProperty(CombatPropertyType.CRIT_CHANCE);
	}

	/// <summary>
	/// Gets the reflex.
	/// </summary>
	/// <returns>The reflex.</returns>
	public float GetReflex() {
		return GetProperty(CombatPropertyType.REFLEX);
	}
	
	/// <summary>
	/// Gets the crit defense.
	/// </summary>
	/// <returns>The crit defense.</returns>
	public float GetCritDefense() {
		return GetProperty(CombatPropertyType.CRIT_DEFENSE);
	}
	
	/// <summary>
	/// Gets the resist.
	/// </summary>
	/// <returns>The resist.</returns>
	/// <param name="dmgType">Dmg type.</param>
	public float GetResist(DamageType dmgType) {
		CombatPropertyType prop;
		switch (dmgType) {
		case DamageType.SLASH:
			prop = CombatPropertyType.RESIST_SLASH;
			break;
		case DamageType.CRUSH:
			prop = CombatPropertyType.RESIST_CRUSH;
			break;
		case DamageType.PIERCE:
			prop = CombatPropertyType.RESIST_PIERCE;
			break;
		case DamageType.DARK:
			prop = CombatPropertyType.RESIST_DARK;
			break;
		case DamageType.LIGHT:
			prop = CombatPropertyType.RESIST_LIGHT;
			break;
		case DamageType.WIND:
			prop = CombatPropertyType.RESIST_WIND;
			break;
		case DamageType.EARTH:
			prop = CombatPropertyType.RESIST_EARTH;
			break;
		case DamageType.WATER:
			prop = CombatPropertyType.RESIST_WATER;
			break;
		case DamageType.FIRE:
		default:
			prop = CombatPropertyType.RESIST_FIRE;
			break;
		}
		
		return GetProperty (prop);
	}
}