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
    public MapTerrain terrain;
    public Vector3 gridLocation;
    public Vector2Int tileKey;
    public GameObject unit;
    public List<Tile> neighborTile;

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
        this.terrain = terrain;
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
            
            this.unit = collision.gameObject;
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        this.unit = null;
    }
  
}
