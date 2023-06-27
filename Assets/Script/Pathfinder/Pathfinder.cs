using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PathFinder
{
    public List<Tile> FindPath(Tile start, Tile target)
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
            var neighbourTiles = GetNeighbourTiles(currentTile);

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

  //  int GetDistance(Tile start, Tile neighbour)
  //  {
  //      return Mathf.Abs(start.TileKey.x - neighbour.TileKey.x) + Mathf.Abs(start.TileKey.y - neighbour.TileKey.y);
  //  }
    float GetDistance(Tile start, Tile neighbour)
    {
        return Mathf.Abs(start.WorldSpacePos.x - neighbour.WorldSpacePos.x) + Mathf.Abs(start.WorldSpacePos.y - neighbour.WorldSpacePos.y);
    }

    public List<Tile> GetNeighbourTiles(Tile tile)
    {
        var map = GameManager.Instance.MapManager.map;

        List<Tile> neighbours = new List<Tile>();


        //TOP
        Vector2Int tilekey = new Vector2Int(tile.TileKey.x, tile.TileKey.y +1);

        if (map.ContainsKey(tilekey))
        {
            neighbours.Add(map[tilekey]);
        }

        //Bottom
        tilekey = new Vector2Int(tile.TileKey.x, tile.TileKey.y - 1);

        if (map.ContainsKey(tilekey))
        {
            neighbours.Add(map[tilekey]);
        }

        //Left
        tilekey = new Vector2Int(tile.TileKey.x-1, tile.TileKey.y);

        if (map.ContainsKey(tilekey))
        {
            neighbours.Add(map[tilekey]);
        }


        //Right
        tilekey = new Vector2Int(tile.TileKey.x+1, tile.TileKey.y);

        if (map.ContainsKey(tilekey))
        {
            neighbours.Add(map[tilekey]);
        }
        return neighbours;
    }
}
