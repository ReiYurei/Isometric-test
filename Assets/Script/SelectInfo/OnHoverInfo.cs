using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class SelectionInfo
{
    public Tile Tile;
    public MapTerrain Terrain;
    public Vector2Int TileKey;
    public Vector3 WorldSpacePos;

    public GameObject StandingObject;

    public UnitStatus UnitStatus;
}
