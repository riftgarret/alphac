using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public abstract class BattleService : MonoBehaviour, IBattleService{

    public float unitOfTime = 1f;

    protected GameState mGameState;
    private Queue<IBattleEvent> mBattleEventQueue;
    private Queue<BattleEntity> mCharacterDecisionQueue;
    private CombatOperationExecutor mCombatExecutor;
    protected BattleEntityManagerComponent mEntityManager;

    // a way to we can only be in a certain state when the game is active
    protected enum GameState {
        INTRO,
        ACTIVE,
        VICTORY,
        LOSS
    }

    // UNITY LIFE CYCLE

    void Awake() {        
        mBattleEventQueue = new Queue<IBattleEvent>();
        mCharacterDecisionQueue = new Queue<BattleEntity>();
        mGameState = GameState.INTRO;
        mCombatExecutor = new CombatOperationExecutor();
        mEntityManager = GetComponent<BattleEntityManagerComponent>();

        // first create our battle system for other components to initialize
        BattleSystem.OnServiceStart(this);
    }

    void OnDestroy() {
        BattleSystem.OnServiceDestroy();
    }

    void Start() {
        OnInitialize(); 
		mGameState = GameState.ACTIVE;
    }

    void Update() {
        // Life cycle order            
        // event queue
        while (mBattleEventQueue.Count > 0) {
            IBattleEvent battleEvent = mBattleEventQueue.Dequeue();
        }

        // tic game 
        if (activeGameTime) {
            OnTimeDelta(unitOfTime * Time.deltaTime);
        }
    }

    // INTERFACE CALLS TO DIGEST EVENTS
    
    public void PostBattleEvent(IBattleEvent e) {
        Debug.Log("On event: " + e);
        mBattleEventQueue.Enqueue(e);
    }

    public void PostActionRequired(BattleEntity entity) {
        Debug.Log("entity decision required: " + entity);
        if (!mCharacterDecisionQueue.Contains(entity)) {
            mCharacterDecisionQueue.Enqueue(entity);
        }
    }

    public void PostActionSelected(BattleEntity entity, IBattleAction action) {
        Debug.Log("entity decision selected: " + entity + ", " + action);
        if (mCharacterDecisionQueue.Peek() != entity) {
            Debug.LogError("Entity decision did not match peek'd entity");
        }

        mCharacterDecisionQueue.Dequeue();
        OnActionSelected(entity, action);
    }

    public void ExecuteCombat(ICombatOperation combatOperation) {
        mCombatExecutor.Execute(combatOperation);
    }

    protected abstract void OnInitialize();

    protected abstract void OnTimeDelta(float delta);

    protected abstract void OnActionRequired(BattleEntity entity);

    protected abstract void OnActionSelected(BattleEntity entity, IBattleAction action);

    protected abstract void OnBattleEvent(IBattleEvent e);    

    /// <summary>
    /// in game clock thats handled on update when the user interface isnt stalled for current active character	/// </summary>
    /// <value><c>true</c> if active game time; otherwise, <c>false</c>.</value>
    private bool activeGameTime {
        get {
            bool isActive = mBattleEventQueue.Count() == 0;
            isActive &= mGameState == GameState.ACTIVE;
            return isActive;
        }
    }

}
    
