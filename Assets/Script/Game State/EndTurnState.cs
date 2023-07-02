public class EndTurnState : GameBaseState
{
    public override void EnterState(StateManager state)
    {
        state.OnChangeState();
        state.YieldExit(1);

    }

    public override void ExitState(StateManager state)
    {

    }

    public override void UpdateState(StateManager state)
    {

    }
}