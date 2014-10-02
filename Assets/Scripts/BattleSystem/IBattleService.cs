using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IBattleService {
    void PostBattleEvent(IBattleEvent e);
    void PostActionRequired(BattleEntity entity);
    void PostActionSelected(BattleEntity entity, IBattleAction action);
    void ExecuteCombat(BattleEntity src, BattleEntity dest, ICombatSkill combatSkill, CombatRound combatRound); // TODO put in logic to pass damage event
}
