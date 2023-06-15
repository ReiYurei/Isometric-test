using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapTerrain
{
    Water = 0,
    Ground = 1,
}

public class Tile : MonoBehaviour
{
    Color color;
    public MapTerrain terrain;
    public Vector3Int gridLocation;
    public GameObject character;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideTile();
        }
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
}
