using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Debugging : MonoBehaviour
{

    public Vector3 coordinate;

    public MouseController mouse;
    public List<Tilemap> tilemaps;
    

    private void Start()
    {
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = gameObject.transform.GetChild(i);
            tilemaps.Add(child.GetComponent<Tilemap>());
        }
    }
    void Update()
    {
        if (GameManager.Instance.MapManager.Tiles.Count > 0)
        {
            coordinate = GameManager.Instance.MapManager.Tiles[0].transform.position;
        }
        else if (GameManager.Instance.MapManager.Tiles.Count >= 1)
        {
            coordinate = GameManager.Instance.MapManager.Tiles[1].transform.position;
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
