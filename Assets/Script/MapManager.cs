using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public Tile OverlayTilePrefab;
    public GameObject OverlayContainer;
    public List<Tilemap> Tilemaps;
    public List<Tile> Tiles;

    public Dictionary<Vector2Int, Tile> map;
    void Awake()
    {
       
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        map = new Dictionary<Vector2Int, Tile>();

        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            Tilemaps.Add(child.GetComponent<Tilemap>());
        }
        int tilemapsCount = Tilemaps.Count;

          //tilemaps[1] = Terrain
        for (int i = 0; i < tilemapsCount; i++)
        {
            BoundsInt bounds = Tilemaps[i].cellBounds;
            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.max.y; y >= bounds.min.y; y--)
                {
                    for (int x = bounds.max.x; x >= bounds.min.x; x--)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (Tilemaps[i].HasTile(tileLocation) && !map.ContainsKey(tileKey))
                        {
                            var tile = Instantiate(OverlayTilePrefab, OverlayContainer.transform);
                            var cellWorldPosition = Tilemaps[i].GetCellCenterWorld(tileLocation);
                            tile.WorldSpacePos = cellWorldPosition;
                            tile.TileKey = tileKey;
                            tile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);


                            if (Tilemaps[i] == Tilemaps[(int)MapTerrain.Water])
                            {
                                tile.ChangeColor(1, 0, 0, 0);
                                tile.SetTerrain(MapTerrain.Water);
                            }
                            if (Tilemaps[i] == Tilemaps[(int)MapTerrain.Ground])
                            {
                                tile.ChangeColor(1, 1, 1, 0);
                                tile.SetTerrain(MapTerrain.Ground);
                            }
                            map.Add(tileKey, tile);
                        }
                    }
                }
            }
        }
        
        
    }
}
