using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionState : GameBaseState
{
    public override void EnterState(StateManager state)
    {
        state.OnChangeState();

    }

    public override void ExitState(StateManager state)
    {
    }

    public override void UpdateState(StateManager state)
    {
    }
}

