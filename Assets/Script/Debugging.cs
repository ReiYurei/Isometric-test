using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Debugging : MonoBehaviour
{

    public Vector3Int coordinate;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool checkTile = tilemaps[0].HasTile(coordinate);
            Debug.Log(checkTile);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
        }
    }
}
