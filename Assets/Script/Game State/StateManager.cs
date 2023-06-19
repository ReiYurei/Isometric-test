using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    GameBaseState currentState;
    GameBaseState previousState;
    public int turnCount;

    public delegate void OnStateChangeHandler(GameBaseState state);
    public event OnStateChangeHandler OnStateChange;

    public BattleBeginState BattleBeginState = new BattleBeginState();
    public PlayerTurnState PlayerTurnState = new PlayerTurnState();
    public EnemyTurnState EnemyTurnState = new EnemyTurnState();

    private void Start()
    {
        turnCount = 1;
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
        StartCoroutine(OnDelay(message));
    
    }

    IEnumerator OnDelay(string message)
    {
        Debug.Log(message);
        yield return new WaitForSeconds(1f);
        currentState.ExitState(this);
    }
}
