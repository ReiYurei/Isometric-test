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


    void OnSubmit(InputAction.CallbackContext context)
    {

    }
    void OnCancel(InputAction.CallbackContext context)
    {
        var state = GameManager.Instance.StateManager;
        state.SetState(state.previousState);
    }
    void OnStateChangeHandler(GameBaseState state)
    {
        switch (state)
        {
            case BattleBeginState:
                Actions.FindActionMap("Player Turn").Disable();
                Actions.FindActionMap("Action UI").Disable();


                break;
            case EnemyTurnState:
                Actions.FindActionMap("Player Turn").Disable();
                Actions.FindActionMap("Action UI").Disable();
                
                break;

            case PlayerTurnState:
                Actions.FindActionMap("Player Turn").Disable();
                Actions.FindActionMap("Action UI").Disable();

                break;
            case PlayerActionState:
                Actions.FindAction("Back").performed -= OnCancel;
                Actions.FindActionMap("Player Turn").Enable();
                Actions.FindActionMap("Action UI").Enable();
                break;
            case ActionUIState:
                Actions.FindActionMap("Player Turn").Disable();
                Actions.FindActionMap("Action UI").Enable();
                Actions.FindAction("Back").performed += OnCancel;
                break;
        }
    }

    private void OnDisable()
    {
        stateManager.OnStateChange -= OnStateChangeHandler;
    }
}
