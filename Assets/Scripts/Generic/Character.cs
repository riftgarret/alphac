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

	[SerializeField]
	public CharacterClassConfig charClass;

	// equipment
	public Weapon mainHandWeapon;
	public ArmorSlot armorHelmet;
	public ArmorSlot armorTorso;
	public ArmorSlot armorLegs;
	public ArmorSlot armorArms;


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

		mainHandWeapon = other.mainHandWeapon;

		curHP = maxHP;
	}



	// TODO tune for balance for dual weild
	public float physicalAttack {
	 get {
			float atk = 1;
			if(mainHandWeapon != null) {
				atk += mainHandWeapon.CalculateAttack(this);
			}

			return strength + atk;
		}
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
		float armorResist = GetTotalArmorResists(dmg);
		float classResist = GetClassArmorResists(dmg);
		return armorResist + classResist;
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

	/// <summary>
	/// Gets the total armor resists. for the armor set
	/// </summary>
	/// <returns>The total armor resists.</returns>
	/// <param name="type">Type.</param>
	private float GetTotalArmorResists(DamageType type) {
		float total = 0f;
		total += GetArmorResist(type, armorArms);
		total += GetArmorResist(type, armorHelmet);
		total += GetArmorResist(type, armorTorso);
		total += GetArmorResist(type, armorLegs);
		return total;
	}

	private float GetArmorResist(DamageType type, ArmorSlot armor) {
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
}
