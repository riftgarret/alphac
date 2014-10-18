using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleController : BattleService, IBattleController {

    // test data
    private EnemyPartySO enemyParty;
    private PCPartySO pcParty;

    
    private BattleTimeQueue mBattleTimeQueue;            
    private PCTurnManager mTurnManager;

    

    // BattleService required methods
    protected override void OnBattleEvent(IBattleEvent e) {        
        // TODO forward to combat log

        // evaluate if the game is over, or we have won
        switch (e.EventType) {
            case BattleEventType.DEATH:
                CheckForVictoryOrAnnilate(!e.SrcEntity.isPC); // 
                break;
        }

    }    

    protected override void OnInitialize() {
        // override parameters        
        mBattleTimeQueue = new BattleTimeQueue();
        mTurnManager = new PCTurnManager(this);

        // initialize entities for other methods in start
        EnemyCharacter[] npcChars = enemyParty.CreateUniqueCharacters();
        PCCharacter[] pcChars = pcParty.CreateUniqueCharacters();

        mEntityManager = new BattleEntityManager(pcChars, npcChars);

        mBattleTimeQueue.InitEntities(mEntityManager.allEntities);
    }    

    protected override void OnTimeDelta(float delta) {
        mBattleTimeQueue.IncrementTimeDelta(delta);
    }

    protected override void OnActionRequired(BattleEntity entity) {
        if (entity is PCBattleEntity) {
            mTurnManager.QueuePC((PCBattleEntity)entity);
        }
        else if(entity is EnemyBattleEntity) {
            EnemyBattleEntity npc = (EnemyBattleEntity)entity;
            npc.enemyCharacter.skillResolver.ResolveAction(this, npc);
        }
    }

    protected override void OnActionSelected(BattleEntity entity, IBattleAction action) {
        mBattleTimeQueue.SetAction(entity, action);
    }
    


    private void CheckForVictoryOrAnnilate(bool isEnemies) {
        BattleEntity[] entities = isEnemies ? (BattleEntity[])entityManager.enemyEntities : (BattleEntity[])entityManager.pcEntities;


        foreach (BattleEntity entity in entities) {
            if (entity.character.curHP > 0) {
                return; // we found an alive player, no way to achieve either state
            }
        }

        // if we got here, it means everyone is dead
        this.mGameState = isEnemies ? GameState.VICTORY : GameState.LOSS;

        // not sure if this is the best place to put this, perhaps in its own script
        if (isEnemies) {
            Debug.Log("Victory");
        }
        else {
            Debug.Log("Defeat");
        }
    }

    // implemented IBattleController to expose components
    public BattleEntityManager entityManager {
        get { return mEntityManager; }
    }


    public PCTurnManager turnManager {
        get { return mTurnManager; }
    }
}
