using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : GameBaseState
{
    public override void EnterState(StateManager state)
    {
        state.OnChangeState();
        state.YieldExit(1);
        

    }

    public override void ExitState(StateManager state)
    {
        state.SetState(state.PlayerTurnState);
    }

    public override void UpdateState(StateManager state)
    {
    }
}
