using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// PC character. Customizable, levelable, etc
/// </summary>
[System.Serializable]
public abstract class Character : ScriptableObject {
	// current stats
	public float curHP;
	public float resource;

	//
	public float maxHP {
		get { return charClass.CalculateHitpoints(this); }
	}

	// character name
	public string displayName;

	// stats
	public float strength;
	public float dexerity;
	public float agility;
	public float inteligence;
	public float wisdom;
	public float luck;

	// level
	public int level;

	[SerializeField]
	public CharacterClass charClass;

	// equipment
	public Weapon mainHandWeapon;
	public Weapon offHandWeapon;

	// armor, accessory

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
