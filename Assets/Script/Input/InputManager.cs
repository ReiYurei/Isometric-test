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
        Actions.FindAction("Back").performed -= OnCancel;
        var state = GameManager.Instance.StateManager;
        state.SetState(state.PlayerActionState);
        
       

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
                Actions.FindActionMap("Player Turn").Enable();
                Actions.FindActionMap("Action UI").Enable();
                break;
            case ActionUIState:
                Actions.FindActionMap("Player Turn").Disable();
                Actions.FindActionMap("Action UI").Enable();
                Actions.FindAction("Back").performed += OnCancel;
                break;
            case SelectingTargetState:
                Actions.FindActionMap("Player Turn").FindAction("Click").Disable();
                Actions.FindActionMap("Player Turn").FindAction("MousePos").Enable();
                Actions.FindActionMap("Action UI").Enable();
                Actions.FindAction("Back").performed += OnCancel;
                break;
            case EndTurnState:
                Actions.FindActionMap("Player Turn").Disable();
                Actions.FindActionMap("Action UI").Disable();
                break;
        }
    }

    private void OnDisable()
    {
        stateManager.OnStateChange -= OnStateChangeHandler;
    }
}
