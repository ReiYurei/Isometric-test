using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseController : MonoBehaviour
{
    Vector3 mousePos;
    Vector2 mousePos2d;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2d = new Vector2(mousePos.x, mousePos.y);
    }
    void LateUpdate()
    {
        var selectedTile = GetPositionOnTile();
        

        
        if (selectedTile.HasValue)
        {
            GameObject overlayTile = selectedTile.Value.collider.gameObject;
            var tile = overlayTile.GetComponent<Tile>();
            transform.position = overlayTile.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                var overlayTilePosition = tile.gridLocation;
                tile.ShowTile();
                Debug.Log(tile.terrain);
                Debug.Log(overlayTilePosition);
            }
        }
    }
    public RaycastHit2D? GetPositionOnTile()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);
        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }
}
