using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;


public class Debugging : MonoBehaviour
{

    public Vector3 coordinate;

    public MouseController mouse;
    public List<Tilemap> tilemaps;

    public Tile startTile;
    public Tile targetTile;


    List<Tile> tilePath;
    public List<Vector2Int> tileKey;
    public List<Vector3> worldPos;


    PathFinder pathfinder;
    public InputActionAsset Actions;
    private void Start()
    {
        pathfinder = new PathFinder();
        Actions.FindActionMap("Debug").FindAction("Click").performed += OnClickDebugging;
        Actions.FindActionMap("Debug").Enable();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            var map = GameManager.Instance.MapManager.Tiles;
            foreach (Tile tile in tilePath)
            {
                tile.HideTile();
            }
        }
    }
    public void OnClickDebugging(InputAction.CallbackContext context)
    {
        var selection = GameManager.Instance.SelectionManager.SelectedInfo;
        var target = GameManager.Instance.SelectionManager.SelectedInfo;
        if (selection.StandingObject != null)
        {
            startTile = selection.Tile;
        }
        else
        {
            targetTile = selection.Tile;
        }
        if (startTile != null && targetTile != null)
        {
            tilePath = pathfinder.FindPath(startTile ?? startTile, targetTile ?? targetTile);
            foreach (Tile tile in tilePath)
            {
                tileKey.Add(tile.TileKey);
                worldPos.Add(tile.WorldSpacePos);
                tile.ShowTile();
            }
        }

    }




    public int numPoints;
    public float RadiusX;
    public float radiusX
    {
        get { return RadiusX * 0.75f; }
    }
    public float RadiusY
    {
        get { return radiusX * 0.5f; }
    }
    float radiusY;
    private void OnDrawGizmos()
    {
        radiusY = RadiusY;

        Vector3 center = new Vector3(coordinate.x, coordinate.y, coordinate.z);


            
            Gizmos.color = Color.red;
            float angleIncrement = 2f * Mathf.PI / numPoints;

            Vector3 prevPoint = center + new Vector3(radiusX, 0f, 0f);
            for (int i = 1; i <= numPoints; i++)
            {
                float angle = i * angleIncrement;
                float x = center.x + radiusX * Mathf.Cos(angle);
                float y = center.y + radiusY * Mathf.Sin(angle);
                Vector3 nextPoint = new Vector3(x, y, center.z);
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
       
        
    }
}
