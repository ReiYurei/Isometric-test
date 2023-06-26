using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{


    GameBaseState currentState;
    public GameBaseState previousState;
    public int turnCount;

    public delegate void OnStateChangeHandler(GameBaseState state);
    public event OnStateChangeHandler OnStateChange;

    public BattleBeginState BattleBeginState = new BattleBeginState();
    public PlayerTurnState PlayerTurnState = new PlayerTurnState();
    public EnemyTurnState EnemyTurnState = new EnemyTurnState();
    public PlayerActionState PlayerActionState = new PlayerActionState();
    public ActionUIState UIState = new ActionUIState();
    
    private void OnEnable()
    {
        turnCount = 1;
    }
    private void Start()
    {
        currentState = BattleBeginState;
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }


  
    public void SetState(GameBaseState state)
    {
        previousState = currentState;
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnChangeState()
    {
        OnStateChange?.Invoke(currentState);
    }

    public void Message(string message)
    {
        Debug.Log(message);

    }


    public void YieldExit(int time)
    {
        StartCoroutine(OnDelay(time));
    }
    IEnumerator OnDelay(int time)
    {
        
        yield return new WaitForSeconds(time);
        currentState.ExitState(this);
    }
}
