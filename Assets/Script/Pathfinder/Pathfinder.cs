using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PathFinder
{

    public List<Tile> FindPath(Tile start, Tile target, Dictionary<Vector2Int, Tile> bound)
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
            
            var neighbourTiles = GetNeighbourTiles(bound,currentTile, start, target);

            foreach (Tile neighbour in neighbourTiles)
            {
                if (closedList.Contains(neighbour))
                {
                    continue;
                }
                
                neighbour.GCost = GetDistance(start, neighbour) + neighbour.MovementPenalty;
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
 

    List<Tile> GetNeighbourTiles(Dictionary<Vector2Int, Tile> bound, Tile tile, Tile start, Tile target)
    {
        var map = bound;
        var x = Mathf.Abs(start.TileKey.x - target.TileKey.x);
        var y = Mathf.Abs(start.TileKey.y - target.TileKey.y);

        List<Tile> neighbours = new List<Tile>();
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
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x, tile.TileKey.y + 1);
            if (!map.ContainsKey(tileKey))
            {
                return;
            }
            neighbours.Add(map[tileKey]);

        }



        void GetBottom()
        {
          
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x, tile.TileKey.y - 1);

            if (!map.ContainsKey(tileKey))
            {
                return;
            }
            neighbours.Add(map[tileKey]);

        }

        void GetLeft()
        {
            //Left
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x - 1, tile.TileKey.y);

            if (!map.ContainsKey(tileKey))
            {
                return;
            }
            neighbours.Add(map[tileKey]);
        }
        

        void GetRight()
        {
            //Right
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x + 1, tile.TileKey.y);

            if (!map.ContainsKey(tileKey))
            {
                return;
            }
            neighbours.Add(map[tileKey]);

        }
        

        return neighbours.Distinct().ToList();
    }
    List<Tile> GetSingleTileNeighbour(Dictionary<Vector2Int, Tile> bound , Tile tile)
    {
        var map = bound;

        List<Tile> neighbours = new List<Tile>();

      
        GetLeft();
        GetRight();
        GetTop();
        GetBottom();

        
        void GetTop()
        {
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x, tile.TileKey.y + 1);
            if (!map.ContainsKey(tileKey))
            {
                return;
            }
            neighbours.Add(map[tileKey]);

        }
        void GetBottom()
        {
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x, tile.TileKey.y - 1);

            if (!map.ContainsKey(tileKey))
            {
                return;
            }

            neighbours.Add(map[tileKey]);
        }
        void GetLeft()
        {
            //Left
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x - 1, tile.TileKey.y);

            if (!map.ContainsKey(tileKey))
            {
                return;
            }

            neighbours.Add(map[tileKey]);
        }


        void GetRight()
        {
            //Right
            Vector2Int tileKey = new Vector2Int(tile.TileKey.x + 1, tile.TileKey.y);

            if (!map.ContainsKey(tileKey))
            {
                return;
            }

            neighbours.Add(map[tileKey]);
        }

        return neighbours.Distinct().ToList(); ;

    }
    public Dictionary<Vector2Int,Tile> GetInRangeCircle(float range, Tile start, List<TerrainTypes> walkable)
    {

        var map = GameManager.Instance.MapManager.map;
        Dictionary <Vector2Int, Tile> inRangeTiles = new Dictionary<Vector2Int, Tile>();

        
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
                if (!map.ContainsKey(tile))
                {
                    continue;
                }
                if (!inRangeCircle(center, tile, range))
                {
                    continue;
                }
                if (map[tile].IsOccuppied || !Walkable(map[tile], walkable))
                {
                    continue;
                }
                inRangeTiles.Add(tile, map[tile]);

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
    public Dictionary<Vector2Int, Tile> GetInRangeDiamond(float range, Tile start, List<TerrainTypes> walkable)
    {
        /*
        up = y + 1
        down = y - 1
        left = x - 1
        right = x + 1
        upper left = x - y
        upper right x + y
        down right = -x + y
        down left = -x -x 
        */
        var map = GameManager.Instance.MapManager.map;

        Dictionary<Vector2Int, Tile> inRangeTiles = new Dictionary<Vector2Int, Tile>();

        Vector2Int center = new Vector2Int(start.TileKey.x, start.TileKey.y);
        #region Bound Variable
        int topBound = (int)center.y + (int)range;
        int bottomBound = (int)center.y - (int)range;
        int leftBound = (int)center.x - (int)range;
        int rightBound = (int)center.x + (int)range;
        #endregion
        

        for (int y = bottomBound; y <= topBound; y++)
        {
            for (int x = leftBound; x <= rightBound; x++)
            {
                var tile = new Vector2Int(x, y);
                if (!map.ContainsKey(tile))
                {
                    continue;
                }
                if (!inRangeDiamond(center, tile, range))
                {
                    continue;
                }
                if (map[tile].IsOccuppied || !Walkable(map[tile], walkable))
                {
                    continue;
                }              
                inRangeTiles.Add(tile, map[tile]);
            }
        }


        Dictionary<Vector2Int, Tile> tempMap = new Dictionary<Vector2Int, Tile>();
        foreach (KeyValuePair<Vector2Int, Tile> mapTile in inRangeTiles)
        {
            var tileKey = new Vector2Int(mapTile.Value.TileKey.x, mapTile.Value.TileKey.y);
            tempMap.Add(tileKey, mapTile.Value);
        
        }
        foreach (KeyValuePair<Vector2Int, Tile> mapTile in tempMap) //Dictionary
        {
            int dx = Mathf.Abs(start.TileKey.x - mapTile.Value.TileKey.x);
            int dy = Mathf.Abs(start.TileKey.y - mapTile.Value.TileKey.y);
            int distance = (int)range - (dx + dy);
            var tileKey = new Vector2Int(mapTile.Value.TileKey.x, mapTile.Value.TileKey.y);
            if (distance < range)
            {
                mapTile.Value.TotalPenalty = mapTile.Value.MovementPenalty + ((int)range - distance);
            }
            var neigbours =GetSingleTileNeighbour(tempMap, mapTile.Value);

            if (range < mapTile.Value.TotalPenalty )
            {
                inRangeTiles.Remove(tileKey);
            }
            
        }
        //List<Tile> tempMap = new List<Tile>();   //USING LIST
        //foreach (KeyValuePair<Vector2Int, Tile> mapTile in inRangeTiles)
        //{
        //    tempMap.Add(mapTile.Value);
        //}
        //foreach (Tile mapTile in tempMap) 
        //{
        //    GetNeighbourTiles(inRangeTiles, mapTile, start, mapTile);
        //    int dx = Mathf.Abs(mapTile.TileKey.x - start.TileKey.x);
        //    int dy = Mathf.Abs(mapTile.TileKey.y - start.TileKey.y);
        //    int distance = (int)range - (dx+dy);
        //    
        //    var tileKey = new Vector2Int(mapTile.TileKey.x, mapTile.TileKey.y);
        //    if (distance < range)
        //    {
        //        mapTile.TotalPenalty = mapTile.MovementPenalty + ((int)range - distance);
        //    }
        //    if (range < mapTile.TotalPenalty )
        //    {
        //        inRangeTiles.Remove(tileKey);
        //    }
        //    
        //}

        bool inRangeDiamond(Vector2Int center, Vector2Int tile, float range)
        {
            float dx = Mathf.Abs(center.x - tile.x);
            float dy = Mathf.Abs(center.y - tile.y);
            float distance = dx + dy;

            return distance <= range;
        }      
        return inRangeTiles;
    }


    public bool Walkable(Tile tile, List<TerrainTypes> walkable)
    {
        foreach (TerrainTypes terrain in walkable)
        {
            if (tile.Terrain == terrain.walkableTerrain)
            {
                tile.MovementPenalty = terrain.movePenalty;
                return true;
            }
        }
        return false;
    }
}
