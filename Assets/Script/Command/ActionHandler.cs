using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionHandler : MonoBehaviour
{
    public delegate void AddCommandHandler(IBattleCommand command);
    public event AddCommandHandler AddedCommand;
    public void _MoveCommand()
    {
        InputActionAsset actions = GameManager.Instance.InputManager.Actions;
        var state = GameManager.Instance.StateManager;
        var selectedUnit = GameManager.Instance.SelectionManager.SelectedInfo;
        PathFinder pathfinder = new PathFinder();
        MoveCommand moveCommand = new MoveCommand();

        var startTile = moveCommand.startTile = selectedUnit.Tile;
        var moveRange = moveCommand.moveRange = selectedUnit.UnitStatus.MoveRange;

        moveCommand.IsFinished = false;
        moveCommand.caster = selectedUnit.StandingObject;
        moveCommand.TrueUnitSpeed = selectedUnit.UnitStatus.TrueUnitSpeed;
        moveCommand.MotionWeight = 5;
        moveCommand.Priority = moveCommand.TrueUnitSpeed + moveCommand.MotionWeight;

        state.SetState(state.SelectingTargetState);
        actions.FindActionMap("Action UI").FindAction("Click").performed += OnMoveClick;

        var bound = pathfinder.GetInRangeDiamond(moveRange, startTile);
        foreach (KeyValuePair<Vector2Int, Tile> tile in bound)
        {
            tile.Value.ShowTile();
        }


        void OnMoveClick(InputAction.CallbackContext context)
        {
            var mouse =  GameManager.Instance.MouseController;
            var selectedTile = mouse.GetPositionOnTile();
            var tile = selectedTile.Value.collider.gameObject.GetComponent<Tile>();
            if (selectedTile.HasValue && tile.IsOccuppied == false)
            {
                var bound = pathfinder.GetInRangeDiamond(moveRange, startTile);
                moveCommand.targetTile = tile;
                List<Tile> tiles = pathfinder.FindPath(startTile, moveCommand.targetTile, bound);
                moveCommand.path = tiles;
                foreach (Tile _tile in tiles)
                {
                    Debug.Log(_tile.TileKey);
                }
                actions.FindActionMap("Action UI").FindAction("Click").performed -= OnMoveClick;
                _AddCommand(moveCommand);
                //EXPERIMENTAL

                selectedUnit.UnitStatus.TurnChances--;
                state.SetState(state.PlayerActionState);
            }


        }
    }
    public void _AttackCommand()
    {
        AttackCommand attackCommand = new AttackCommand();
        Debug.Log(attackCommand);
    }

    public void _DefendCommand()
    {

    }

    void _AddCommand(IBattleCommand command)
    {
        AddedCommand?.Invoke(command);
    }
}
