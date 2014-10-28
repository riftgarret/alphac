using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CombatOperationFactory {

    public static ICombatOperation createOperation(BattleEntity src, BattleEntity dest, CombatRound combatRound) {
        CombatResolver srcRes = CombatResolverFactory.CreateSource(src, combatRound);
        CombatResolver destRes = CombatResolverFactory.CreateDestination(dest);

		CombatOperation.Builder builder = new CombatOperation.Builder ();

		builder.AddCondition (new HitChanceConditionLogic ());
		builder.AddLogic (new DamageLogic ());
        
        return builder.Build(srcRes, destRes);
    }

}
