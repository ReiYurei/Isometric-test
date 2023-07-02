using System.Collections.Generic;
using UnityEngine;
public class ActionUIState : GameBaseState
{
    public override void EnterState(StateManager state)
    {
        state.OnChangeState();
        var tiles = GameManager.Instance.MapManager.map;
        foreach (KeyValuePair<Vector2Int, Tile> tile in tiles)
        {
            tile.Value.HideTile();
        }
    }

    public override void ExitState(StateManager state)
    {
    }

    public override void UpdateState(StateManager state)
    {
    }
}