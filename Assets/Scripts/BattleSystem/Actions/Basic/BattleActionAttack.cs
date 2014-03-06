using UnityEngine;
using System.Collections;

public class BattleActionAttack : BattleActionPhysical {
	int mAttackCount = 0;

	public BattleActionAttack(CombatSkill skill, BattleEntity source, ITargetResolver targetResolver) : base(skill, source, targetResolver) {

	}

	public override void OnExecuteAction (float actionClock)
	{	
		if(actionClock >= timeAction && mAttackCount == 0) {

			// temp set all as default
			SkillCombatNode skillNode = new SkillCombatNode(this.combatSkill);
			int weaponCount = sourceEntity.equipedWeapons.Length;

			// TODO not sure if this should always be all, or just known to be single or all
			foreach(BattleEntity entity in targetResolver.GetTargets(combatSkill)) {

				// for each weapon
				for(int weaponIndex = 0; weaponIndex < weaponCount; weaponIndex++) {
					// TODO if more than 1 weapon, we should dull down multiple hits dmg
					CombatResolver resolver = sourceEntity.CreateCombatNodeBuilder()
						.SetSkillCombatNode(skillNode)
						.SetWeaponIndex(weaponIndex)
						.BuildResolver();

					BattleSystem.combatExecutor.ExecutePhysicalAttack(sourceEntity, entity, this, CombatStatusEffectList.EMPTY, GetWeaponDamageType(weaponIndex), resolver);
				}

			}

			mAttackCount++;
		}	
	}
}
