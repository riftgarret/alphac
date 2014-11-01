using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CombatOperationFactory {

    public static ICombatOperation createOperation(BattleEntity src, BattleEntity dest, CombatRound combatRound) {
        CombatResolver srcRes = CombatResolverFactory.CreateSource(src, combatRound);
        CombatResolver destRes = CombatResolverFactory.CreateDestination(dest);



		HitChanceLogic hitChanceLogic = new HitChanceLogic ();
		DamageLogic damageLogic = new DamageLogic ();

		return new CombatOperation.Builder ()
			.AddLogic(damageLogic)
			.Require(delegate(ICombatLogic [] conditions) {
					HitChanceLogic hitChance = (HitChanceLogic) conditions[0];
					return hitChance.Hits;
				}, hitChanceLogic)			
			.Build(srcRes, destRes);
    }

}
