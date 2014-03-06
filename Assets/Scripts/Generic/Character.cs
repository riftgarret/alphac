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


	public CharacterClassData charClass;

	// equipment
	private WeaponConfig mWeaponConfig;
	private ArmorConfig mArmorConfig;

	public Weapon [] equipedWeapons {
		get { return mWeaponConfig.equipedWeapons; }
	}

	public Armor [] equipedArmor {
		get { return mArmorConfig.equipedArmor; }
	}
	// armor, accessory
	
	// empty constructor
	public Character() {}

	// used for cloning
	public static Character CreateFromConfig(CharacterConfig config) {
		if(config is EnemyCharacterConfig) {
			return new EnemyCharacter((EnemyCharacterConfig)config);
		}
		else {
			return new PCCharacter((PCCharacterConfig)config);
		}
	}

	protected Character(CharacterConfig other) {
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

		for (int i=0; i < other.weapons.Length; i++) {
			mArmorConfig.EquipArmor(other.armors[i], other.armors[i].slot);
		}


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
	

	private float GetArmorResist(DamageType type, Armor armor) {
		if(armor == null) {
			return 0f;
		}
		switch(type) {
		case DamageType.CRUSH:
			return armor.config.resists.crush;
		case DamageType.PIERCE:
			return armor.config.resists.pierce;
		case DamageType.SLASH:
			return armor.config.resists.slash;
		case DamageType.DARK:
			return armor.config.resists.dark;
		case DamageType.LIGHT:
			return armor.config.resists.light;
		case DamageType.WIND:
			return armor.config.resists.wind;
		case DamageType.EARTH:
			return armor.config.resists.earth;
		case DamageType.FIRE:
			return armor.config.resists.fire;
		case DamageType.WATER:
			return armor.config.resists.water;
		}
		return 0f;
	}

	public override string ToString ()
	{
		return string.Format ("[Character: displayName={0}]", displayName);
	}
	
}
