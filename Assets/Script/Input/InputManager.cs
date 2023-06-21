using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public InputActionAsset Actions; 
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
                Actions.FindActionMap("Player Turn Input").Disable();
                Actions.FindActionMap("Menu UI").Enable();
                
                break;
            case EnemyTurnState:
                Actions.FindActionMap("Player Turn Input").Disable();
                Actions.FindActionMap("Menu UI").Enable();
                
                break;

            case PlayerTurnState:
                Actions.FindActionMap("Player Turn Input").Enable();
                Actions.FindActionMap("Menu UI").Enable();
                
                break;
        }
    }

    private void OnDisable()
    {
        stateManager.OnStateChange -= OnStateChangeHandler;
    }
}
