using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public OnHoverSelectionInfo SelectedInfo;
    public OnHoverSelectionInfo HoverInfo;
    public void OnSelectInfo(Tile tile, GameObject selectedObject, Vector2Int tileKey, Vector3 worldSpacePos)
    {
        SelectedInfo.Tile = tile;
        SelectedInfo.StandingObject = selectedObject;
        SelectedInfo.TileKey = tileKey;
        SelectedInfo.WorldSpacePos = worldSpacePos;
        SelectedInfo.Terrain = SelectedInfo.Tile.Terrain;
        OnSelectUnitInfo();
    }
    public void OnHoverInfo(Tile tile, GameObject selectedObject)
    {
        HoverInfo.Tile = tile;
        HoverInfo.StandingObject = selectedObject;
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
        }
        
        else
        {
            SelectedInfo.UnitStatus = null;
        }

    }

}
