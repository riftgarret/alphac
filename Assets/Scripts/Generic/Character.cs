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
	public WeaponConfig mainHandWeapon;
	public WeaponConfig offHandWeapon;

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

		mainHandWeapon = (WeaponConfig)mainHandWeapon;
		offHandWeapon = (WeaponConfig)offHandWeapon;

		curHP = maxHP;
	}

	// TODO tune for balance for dual weild
	public float physicalAttack {
	 get {
			float atk = 1;
			if(mainHandWeapon != null) {
				atk += mainHandWeapon.CalculateAttack(this);
			}
			if(offHandWeapon != null) {
				atk += offHandWeapon.CalculateAttack(this);
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
}
