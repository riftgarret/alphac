//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

public class ConfigurableCombatNode : ICombatNode
{
	protected float [] mPropertyAdd;
	protected float [] mPropertyMultiply;

	public ConfigurableCombatNode ()
	{
		mPropertyAdd = new float[(int)CombatNodeProperty.COUNT];
		mPropertyMultiply = new float[(int)CombatNodeProperty.COUNT];

		for(int i=0; i < (int)CombatNodeProperty.COUNT; i++) {
			mPropertyMultiply[i] = 1f;
		}
	}

	public float GetPropertyAdd (CombatNodeProperty property)
	{
		return mPropertyAdd[(int)property];
	}

	public float GetPropertyMultiply (CombatNodeProperty property)
	{
		return mPropertyMultiply[(int)property];
	}

	/// <summary>
	/// Read and load the general modifiers.
	/// </summary>
	/// <param name="modifiers">Modifiers.</param>
	public void ReadGeneralModifiers(GeneralOffensiveModifier [] modifiers) {
		// parse out general
		if(modifiers != null) {
			foreach(GeneralOffensiveModifier mod in modifiers) {
				switch(mod.type) {
				case GeneralOffensiveModifierType.TOTAL_DMG_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.TOTAL_DAMAGE] = mod.modValue;
					break;
				case GeneralOffensiveModifierType.TOTAL_DMG_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.TOTAL_DAMAGE] = mod.modValue;
					break;	
				case GeneralOffensiveModifierType.CRIT_CHANCE_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.CRIT_CHANCE] = mod.modValue;
					break;
				case GeneralOffensiveModifierType.CRIT_CHANCE_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.CRIT_CHANCE] = mod.modValue;
					break;
				}
			}
		}
	}

	public void ReadStatModifiers(StatModifier [] modifiers) {
		// parse out general
		if(modifiers != null) {
			foreach(StatModifier mod in modifiers) {
				switch(mod.stat) {
				case StatType.STR:
					this.mPropertyMultiply[(int)CombatNodeProperty.STR] = mod.mod;
					break;				
				case StatType.VIT:
					this.mPropertyMultiply[(int)CombatNodeProperty.VIT] = mod.mod;
					break;
				case StatType.DEX:
					this.mPropertyMultiply[(int)CombatNodeProperty.DEX] = mod.mod;
					break;
				case StatType.AGI:
					this.mPropertyMultiply[(int)CombatNodeProperty.AGI] = mod.mod;
					break;
				case StatType.INT:
					this.mPropertyMultiply[(int)CombatNodeProperty.INT] = mod.mod;
					break;
				case StatType.WIS:
					this.mPropertyMultiply[(int)CombatNodeProperty.WIS] = mod.mod;
					break;
				case StatType.LUCK:
					this.mPropertyMultiply[(int)CombatNodeProperty.LUCK] = mod.mod;
					break;
				}
			}
		}
	}

	public void ReadPhysicalModifiers(PhysicalOffensiveModifier [] modifiers) {
		// parse out physical
		if(modifiers != null) {
			foreach(PhysicalOffensiveModifier mod in modifiers) {
				switch(mod.type) {
				case PhysicalOffensiveModifierType.POWER_PHYSICAL_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.POWER_PHYSICAL] = mod.modValue;
					break;
				case PhysicalOffensiveModifierType.POWER_PHYSICAL_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.POWER_PHYSICAL] = mod.modValue;
					break;
				case PhysicalOffensiveModifierType.DODGE_IGNORE_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.REFLEX_IGNORE] = mod.modValue;
					break;
				case PhysicalOffensiveModifierType.DODGE_IGNORE_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.REFLEX_IGNORE] = mod.modValue;
					break;					
				case PhysicalOffensiveModifierType.ARMOR_IGNORE_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.RESIST_IGNORE] = mod.modValue;
					break;
				case PhysicalOffensiveModifierType.ARMOR_IGNORE_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.RESIST_IGNORE] = mod.modValue;
					break;					
				}
			}
		}		
	}

	public void ReadMagicalModifiers(MagicalOffensiveModifier [] modifiers) {
		// parse out physical
		if(modifiers != null) {
			foreach(MagicalOffensiveModifier mod in modifiers) {
				switch(mod.type) {
				case MagicalOffensiveModifierType.POWER_MAGICAL_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.POWER_MAGIC] = mod.modValue;
					break;
				case MagicalOffensiveModifierType.POWER_MAGICAL_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.POWER_MAGIC] = mod.modValue;
					break;
				case MagicalOffensiveModifierType.RESIST_IGNORE_ADD:
					this.mPropertyAdd[(int)CombatNodeProperty.RESIST_IGNORE] = mod.modValue;
					break;
				case MagicalOffensiveModifierType.RESIST_IGNORE_MULTIPLY:
					this.mPropertyMultiply[(int)CombatNodeProperty.RESIST_IGNORE] = mod.modValue;
					break;
				}
			}
		}		
	}
}

