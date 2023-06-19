using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public InputActionAsset actions; 
    StateManager stateManager;
    private void Start()
    {
        stateManager = GameManager.Instance.StateManager;
        stateManager.OnStateChange += OnStateChangeHandler;
    }

    void OnStateChangeHandler(GameBaseState state)
    {
        switch (state)
        {
            case BattleBeginState:
                actions.FindActionMap("Player Turn Input").Disable();
                actions.FindActionMap("UI").Enable();
                
                break;
            case EnemyTurnState:
                actions.FindActionMap("Player Turn Input").Disable();
                actions.FindActionMap("UI").Enable();
                
                break;

            case PlayerTurnState:
                actions.FindActionMap("Player Turn Input").Enable();
                actions.FindActionMap("UI").Enable();
                
                break;
        }
    }

    private void OnDisable()
    {
        stateManager.OnStateChange -= OnStateChangeHandler;
    }
}
