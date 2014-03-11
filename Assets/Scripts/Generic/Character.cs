using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// PC character. Customizable, levelable, etc
/// </summary>
[System.Serializable]
public abstract class Character  {
	// current stats
	public float curHP;
	public float curResource;

	//
	public float maxHP {
		get { return charClass.CalculateHitpoints(this); }
	}

	// TODO maybe add this later
	public float maxResource {
		get { return 100f; }
	}

	//  override enable to set current HP to max HP
	void OnEnable() {
		curHP = maxHP;
	}

	// character name
	public string displayName;

	// stats
	public float strength;
	public float vitality;
	public float dexerity;
	public float agility;
	public float inteligence;
	public float wisdom;
	public float luck;

	// level and class
	public int level;


	public CharacterClassSO charClass;

	// equipment
	private WeaponConfig mWeaponConfig;
	private ArmorConfig mArmorConfig;

	public IWeapon [] equipedWeapons {
		get { 
			if(mWeaponConfig.equipedWeapons.Length == 0) 
				Debug.Log ("empty equipedWeapons"); 
			return mWeaponConfig.equipedWeapons; }
	}

	public Armor [] equipedArmor {
		get { return mArmorConfig.equipedArmor; }
	}
	// armor, accessory

	// empty constructor
	public Character() {}

	// used for cloning
	public static Character CreateFromConfig(CharacterSO config) {
		if(config is EnemyCharacterSO) {
			return new EnemyCharacter((EnemyCharacterSO)config);
		}
		else {
			return new PCCharacter((PCCharacterSO)config);
		}
	}

	protected Character(CharacterSO other) {
		// name
		displayName = other.displayName;

		// stats
		strength = other.strength;
		vitality = other.vitality;
		dexerity = other.dexerity;
		agility = other.agility;
		inteligence = other.inteligence;
		wisdom = other.wisdom;
		luck = other.luck;


		// level and class
		level = other.level;
		charClass = other.charClass;

		// create armor and weapon configs from class rules
		mWeaponConfig = charClass.CreateWeaponConfig();
		mArmorConfig = charClass.CreateArmorConfig ();


		for (int i=0; i < other.weapons.Length; i++) {
			mWeaponConfig.EquipWeapon(other.weapons[i], i);
		}
		/*
		for (int i=0; i < other.weapons.Length; i++) {
			mArmorConfig.EquipArmor(other.armors[i], other.armors[i].slot);
		}
*/

		curHP = maxHP;
	}
			
	// TODO
	public float magicAttack {
		get { return 0; }
	}

	public float accuracy {
		get {
			return dexerity;
		}
	}

	public float relfex {
		get {
			return agility;
		}
	}

	// TODO armor based on equipment + 10% vit
	// TODO physical resistance: product of armor and vit

	// TODO elemental resist

	public float critChance {
		get { return luck; }
	}

	public float critDefense {
		get { return luck + vitality / 2; }
	}

	/// <summary>
	/// Gets the stat value.
	/// </summary>
	/// <returns>The stat.</returns>
	/// <param name="stat">Stat.</param>
	public float GetStat(StatType stat) {
		switch(stat) {
		case StatType.STR:
			return strength;
		case StatType.VIT:
			return vitality;
		case StatType.AGI:
			return agility;
		case StatType.DEX:
			return dexerity;
		case StatType.INT:
			return inteligence;
		case StatType.WIS:
			return wisdom;
		case StatType.LUCK:
		default:
			return luck;
		}
	}


	public float GetResist(DamageType dmg) {
		float classResist = GetClassArmorResists(dmg);
		return classResist;
	}

	private float GetClassArmorResists(DamageType type) {
		switch(type) {
		case DamageType.CRUSH:
			// get crush armor stats
			return (vitality / 2f);
		case DamageType.PIERCE:
			return (agility / 2f);
		case DamageType.SLASH:
			return (dexerity / 2f);
		case DamageType.DARK:
			return (luck / 2f);
		case DamageType.LIGHT:
			return (wisdom / 2f);
		case DamageType.EARTH:
			return (strength / 2f);
		case DamageType.WIND:
			return (agility / 2f);
		case DamageType.FIRE:
			return (vitality / 2f);
		case DamageType.WATER:
			return (wisdom / 2f);
		default:
			return 1;
		}
	}

	public override string ToString ()
	{
		return string.Format ("[Character: displayName={0}]", displayName);
	}
	
}
