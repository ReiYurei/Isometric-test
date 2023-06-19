using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : GameBaseState
{
    public override void EnterState(StateManager state)
    {
        state.OnChangeState();
        state.Message($"Player Turn!");
        
    }

    public override void ExitState(StateManager state)
    {
    }

    public override void UpdateState(StateManager state)
    {
    }
}
