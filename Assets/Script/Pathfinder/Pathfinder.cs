using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PathFinder
{

    public List<Tile> FindPath(Tile start, Tile target, Dictionary<Vector2Int, Tile> bound, List<MapTerrain> walkable)
    {
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();
        openList.Add(start);
        while (openList.Count > 0)
        {
            Tile currentTile = openList.OrderBy(x => x.FCost).First();

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (currentTile == target)
            {
                return GetFinishedPath(start, target);
            }
            
            var neighbourTiles = GetNeighbourTiles(bound,currentTile, start, target, walkable);

            foreach (Tile neighbour in neighbourTiles)
            {
                if (closedList.Contains(neighbour))
                {
                    continue;
                }
                neighbour.GCost = GetDistance(start, neighbour);
                neighbour.HCost = GetDistance(target, neighbour);

                neighbour.previousTile = currentTile;

                if (!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<Tile>();
    }

    public List<Tile> GetFinishedPath(Tile start, Tile target)
    {
        List<Tile> finishedPath = new List<Tile>();
        Tile currentTile = target;
        while (currentTile != start)
        {
            finishedPath.Add(currentTile);
            currentTile = currentTile.previousTile;
        }
        finishedPath.Reverse();
        return finishedPath;
    }

    int GetDistance(Tile start, Tile neighbour)
    {
        return Mathf.Abs(start.TileKey.x - neighbour.TileKey.x) + Mathf.Abs(start.TileKey.y - neighbour.TileKey.y);
    }
   // float GetDistance(Tile start, Tile neighbour)
   // {
   //     return Mathf.Abs(start.WorldSpacePos.x - neighbour.WorldSpacePos.x) + Mathf.Abs(start.WorldSpacePos.y - neighbour.WorldSpacePos.y);
   // }

    List<Tile> GetNeighbourTiles(Dictionary<Vector2Int, Tile> bound, Tile tile, Tile start, Tile target, List<MapTerrain> walkable)
    {
        var map = bound;
        var x = Mathf.Abs(start.TileKey.x - target.TileKey.x);
        var y = Mathf.Abs(start.TileKey.y - target.TileKey.y);
        
        List<Tile> neighbours = new List<Tile>();
        //if x < y && Walkable 
        if (x < y)
        {
            GetLeft();
            GetRight();
            GetTop();
            GetBottom();
            
        }
        else if (x > y)
        {
            GetTop();
            GetBottom();
            GetLeft();
            GetRight();
        }
        else
        {
            GetLeft();
            GetRight();
            GetTop();
            GetBottom();
        }

        void GetTop()
        {
            Vector2Int tilekey = new Vector2Int(tile.TileKey.x, tile.TileKey.y + 1);
            if (map.ContainsKey(tilekey) && Walkable(tile, walkable) == true)
            {
                neighbours.Add(map[tilekey]);
            }
        }



        void GetBottom()
        {
          
            Vector2Int tilekey = new Vector2Int(tile.TileKey.x, tile.TileKey.y - 1);

            if ((map.ContainsKey(tilekey) && Walkable(tile, walkable) == true))
            {
                neighbours.Add(map[tilekey]);
            }

        }

        void GetLeft()
        {
            //Left
            Vector2Int tilekey = new Vector2Int(tile.TileKey.x - 1, tile.TileKey.y);

            if ((map.ContainsKey(tilekey) && Walkable(tile, walkable) == true))
            {
                neighbours.Add(map[tilekey]);
            }
        }
        

        void GetRight()
        {
            //Right
            Vector2Int tilekey = new Vector2Int(tile.TileKey.x + 1, tile.TileKey.y);

            if ((map.ContainsKey(tilekey) && Walkable(tile, walkable) == true))
            {
                neighbours.Add(map[tilekey]);
            }
            
        }
        return neighbours;
    }
    public Dictionary<Vector2Int,Tile> GetInRangeCircle(float range, Tile start)
    {

        var map = GameManager.Instance.MapManager.map;
        Dictionary <Vector2Int, Tile> inRangeTiles = new Dictionary<Vector2Int, Tile>();

        //int numPoints = 8;

        //float radiusX = range * 0.75f;       
        //float radiusY = radiusX * 0.5f;

        Vector2Int center = new Vector2Int(start.TileKey.x, start.TileKey.y);
        int topBound = (int)center.y + (int)range;
        int bottomBound = (int)center.y - (int)range;
        int leftBound = (int)center.x - (int)range;
        int rightBound = (int)center.x + (int)range;


        for (int y = bottomBound; y <= topBound; y++)
        {
            for (int x = leftBound; x <= rightBound; x++)
            {
                var tile = new Vector2Int(x, y);
                if (inRangeCircle(center, tile, range) && map.ContainsKey(tile))
                {
                    inRangeTiles.Add(tile, map[tile]);
                }

            }
        }
        bool inRangeCircle(Vector2Int center, Vector2Int tile, float range)
        {
            float dx = center.x - tile.x;
            float dy = center.y - tile.y;
            float distance = dx * dx + dy * dy;

            return distance <= range * range;
        }
        return inRangeTiles;
    }
    public Dictionary<Vector2Int, Tile> GetInRangeDiamond(float range, Tile start)
    {

        var map = GameManager.Instance.MapManager.map;

        Dictionary<Vector2Int, Tile> inRangeTiles = new Dictionary<Vector2Int, Tile>();

        Vector2Int center = new Vector2Int(start.TileKey.x, start.TileKey.y);
        int topBound = (int)center.y + (int)range;
        int bottomBound = (int)center.y - (int)range;
        int leftBound = (int)center.x - (int)range;
        int rightBound = (int)center.x + (int)range;


        for (int y = bottomBound; y <= topBound; y++)
        {
            for (int x = leftBound; x <= rightBound; x++)
            {
                var tile = new Vector2Int(x, y);
                if (inRangeDiamond(center, tile, range) && map.ContainsKey(tile))
                {
                    inRangeTiles.Add(tile,map[tile]);
                }

            }
        }
        bool inRangeDiamond(Vector2Int center, Vector2Int tile, float range)
        {
            float dx = Mathf.Abs(center.x - tile.x);
            float dy = Mathf.Abs(center.y - tile.y);
            float distance = dx + dy;

            return distance <= range;
        }      
        return inRangeTiles;
    }

    public bool Walkable(Tile tile, List<MapTerrain> walkable)
    {
        foreach (MapTerrain terrain in walkable)
        {
            if (tile.Terrain == terrain)
            {
                return true;
            }
        }
        return false;
    }
}
