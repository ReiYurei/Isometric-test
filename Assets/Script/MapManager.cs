using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public Tile overlayTilePrefab;
    public GameObject overlayContainer;
    public List<Tilemap> tilemaps;
    
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
            tilemaps.Add(child.GetComponent<Tilemap>());
        }
        int tilemapsCount = tilemaps.Count;

          //tilemaps[1] = Terrain
        for (int i = 0; i < tilemapsCount; i++)
        {
            BoundsInt bounds = tilemaps[i].cellBounds;
            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.max.y; y >= bounds.min.y; y--)
                {
                    for (int x = bounds.max.x; x >= bounds.min.x; x--)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tilemaps[i].HasTile(tileLocation) && !map.ContainsKey(tileKey))
                        {
                            var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                            var cellWorldPosition = tilemaps[i].GetCellCenterWorld(tileLocation);
                            overlayTile.gridLocation = tileLocation;
                            overlayTile.tileKey = tileKey;
                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);


                            if (tilemaps[i] == tilemaps[(int)MapTerrain.Water])
                            {
                                overlayTile.ChangeColor(1, 0, 0, 0);
                                overlayTile.SetTerrain(MapTerrain.Water);
                            }
                            if (tilemaps[i] == tilemaps[(int)MapTerrain.Ground])
                            {
                                overlayTile.ChangeColor(1, 1, 1, 0);
                                overlayTile.SetTerrain(MapTerrain.Ground);
                            }
                            map.Add(tileKey, overlayTile);
                        }
                    }
                }
            }
        }
        
        
    }
}
