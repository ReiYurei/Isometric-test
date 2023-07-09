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
    
    public float HCost; //Cost to Goal
    public float GCost; //Cost from start
    public float FCost { get => HCost + GCost; }
    public Tile previousTile;


    public Color Color { get => color; }
    [SerializeField] Color color;
    public MapTerrain Terrain { get => terrain; }
    [SerializeField] MapTerrain terrain;
    public Vector3 WorldSpacePos { get => worldSpacePos; }
    [SerializeField] Vector3 worldSpacePos;
    public Vector2Int TileKey { get => tileKey; }
    [SerializeField] Vector2Int tileKey;
    public GameObject UnitObject { get => unitObject; }
    [SerializeField] GameObject unitObject;
    public bool IsOccuppied { get => isOccuppied; }
    [SerializeField] bool isOccuppied;

    public int MovementPenalty;
    public int TotalPenalty;

    void Awake()
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



    public void SetTileKey(Vector2Int tilekey)
    {
        this.tileKey = tilekey;
    }
    public void SetWorldPos(Vector3 worldSpacePos)
    {
        this.worldSpacePos = worldSpacePos;
    }
    public void SetTerrain(MapTerrain terrain)
    {
        this.terrain = terrain;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            
            this.unitObject = collision.gameObject;
            isOccuppied = true;
            ChangeColor(color.r, color.g-1, color.b-1, color.a);
        }


    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        this.unitObject = null;
        isOccuppied = false;
        ChangeColor(color.r, color.g+1, color.b+1, color.a);
    }
  
}
