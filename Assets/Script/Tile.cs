using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum MapTerrain
{
    Water = 0,
    Ground = 1,
}

public class Tile : MonoBehaviour
{
    Color color;
    public MapTerrain Terrain;
    public Vector3 WorldSpacePos;
    public Vector2Int TileKey;
    public GameObject UnitObject;
    public List<Tile> NeighborTile;
    public bool isOccuppied;

    void Awake()
    {
        

    }

    private void Update()
    {

       
    }
    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r,color.g,color.b, 1);
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
    }
    public void ChangeColor(float r, float g, float b, float a)
    {
        color = new Color(r, g, b, a);
    }
    public void SetTerrain(MapTerrain terrain)
    {
        this.Terrain = terrain;
    }
    public List<Tile> GetNeighborTiles(Tile currentTiles)
    {
        var center = this.transform;

        //top neighbour
        return null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            
            this.UnitObject = collision.gameObject;
            isOccuppied = true;
        }


    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        this.UnitObject = null;
        isOccuppied = false;
    }
  
}
