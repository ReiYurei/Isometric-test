using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public SelectionInfo SelectedInfo { get => selectedInfo; private set => hoverInfo = value; }
    [SerializeField] SelectionInfo selectedInfo;
    public SelectionInfo HoverInfo { get => hoverInfo; private set => hoverInfo = value; }
    [SerializeField] SelectionInfo hoverInfo;
    public void OnSelectInfo(Tile tile, GameObject selectedObject, Vector2Int tileKey, Vector3 worldSpacePos)
    {
        SelectedInfo.Tile = tile;
        SelectedInfo.StandingObject = selectedObject;
        SelectedInfo.TileKey = tileKey;
        SelectedInfo.WorldSpacePos = worldSpacePos;
        SelectedInfo.Terrain = SelectedInfo.Tile.Terrain;
        SelectedInfo.IsOccupied = SelectedInfo.Tile.IsOccuppied;
        SelectedInfo.TotalPenalty = SelectedInfo.Tile.TotalPenalty;
        OnSelectUnitInfo();

    }
    public void OnHoverInfo(Tile tile)
    {
        HoverInfo.Tile = tile;       
        HoverInfo.Terrain = HoverInfo.Tile.Terrain;
        OnHoverUnitInfo();


    }



    void OnHoverUnitInfo()
    {
        if (HoverInfo.StandingObject != null)
        {
            HoverInfo.UnitStatus = HoverInfo.StandingObject.GetComponent<Unit>().status;
        }
        else
        {    
            HoverInfo.UnitStatus = null;
        }
    }
    void OnSelectUnitInfo()
    {
        if (SelectedInfo.StandingObject != null)
        {
            SelectedInfo.UnitStatus = SelectedInfo.StandingObject.GetComponent<Unit>().status;
            

            if (!SelectedInfo.UnitStatus.IsFriendly)
            {
                SelectedInfo.UnitStatus = null;
                return;
            }
            var state = GameManager.Instance.StateManager;
            state.SetState(state.UIState);

        }
    }
}
