using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBeginState : GameBaseState
{
    public override void EnterState(StateManager state)
    {
        state.OnChangeState();
        state.Message($"Battle Begin! Turn {state.turnCount}");
        
    }

    public override void ExitState(StateManager state)
    {
        state.SetState(state.EnemyTurnState);
    }

    public override void UpdateState(StateManager state)
    {
    }
}
