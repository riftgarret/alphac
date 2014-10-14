using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CombatUtil {
    
    /// <summary>
    /// Get chance to hit
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public static float ChanceToHit(CombatResolver src, CombatResolver dest) {
        // TODO first check for flags to hit or miss
        float accuracy = src.CombatStats.accuracy;
        float evasion = dest.CombatStats.evasion;
        float chanceToHit = accuracy / (accuracy + evasion);
        return chanceToHit;
    }

    /// <summary>
    /// Using a random 0-1 on chance to hit for success
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public static bool HitSuccess(CombatResolver src, CombatResolver dest) {
        return UnityEngine.Random.Range(0f, 1f) > ChanceToHit(src, dest);        
    }

    /// <summary>
    /// Chance to crit
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public static float ChanceToCrit(CombatResolver src, CombatResolver dest) {
        float srcCritChance = src.CombatStats.critAccuracy;
		float critDefense = dest.CombatStats.critEvasion;
		float critChance = srcCritChance / (srcCritChance + critDefense); 
        return critChance;
    }

    /// <summary>
    /// based on ChanceToCrit if was sucessful
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public static bool CritSuccess(CombatResolver src, CombatResolver dest) {
        return UnityEngine.Random.Range(0f, 1f) > ChanceToCrit(src, dest);        
    }

    /// <summary>
    /// Calculate damage
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <param name="isCrit"></param>
    /// <returns></returns>
    public static DamageEvent CalculateDamage(CombatResolver src, CombatResolver dest, bool isCrit) {        
        // pull out min and max damage and calculated 'rolled damage'
        ElementVector min = src.DamageMin;        
        ElementVector max = src.DamageMax;
        float rand = UnityEngine.Random.Range(0f, 1f);
        ElementVector diff = max - min;
        ElementVector randomDmg = min + (diff * rand);

        // scale damage by vector, this could have been done earlier, same results
        AttributeVector attributes = src.Attributes;
        AttributeVector damageAttributeScalar = src.DamageAttributeScalar;
        AttributeVector resultDamageExtra = attributes * damageAttributeScalar;
        float scaleDamage = resultDamageExtra.Sum;

        // scale damage should be < 1 normally, so we want to ensure this is positive on scaling
        ElementVector resultDamage = randomDmg * (1 + scaleDamage); 

        // scale on dmg bonus
        resultDamage += src.ElementAttackRaw;
        resultDamage *= src.ElementAttackScalar;

        // if is critical 
        ElementVector critDamage = new ElementVector();
        if (isCrit) {
            float critScale = CalculateCritDamageScale(src, dest);
            critDamage = (resultDamage * critScale);
        }

        // total damage                 
        return new DamageEvent(src.entity, dest.entity, resultDamage, critDamage, dest.ElementDefense);
    }

    /// <summary>
    /// Crit damage, can scale from 0 to 1x damage depending on crit defense
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public static float CalculateCritDamageScale(CombatResolver src, CombatResolver dest) {        
        float critPower = src.CombatStats.critPower;
        float critDefense = dest.CombatStats.critDefense;
        float value = (critPower - critDefense) / critPower;
        return Math.Max(0, value);
    }
} 

