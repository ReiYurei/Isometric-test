using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(StateManager state);
    public abstract void UpdateState(StateManager state);
    public abstract void ExitState(StateManager state);

}
