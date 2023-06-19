using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    Vector3 mousePos;
    Vector2 mousePos2d;

    private void Awake()
    {
       
        
    }
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
        }

    }

    public void OnClick(InputAction.CallbackContext context)
    {
        var selectedTile = GetPositionOnTile();
        if (selectedTile.HasValue)
        {
            var tile = selectedTile.Value.collider.gameObject.GetComponent<Tile>();
            var tilePosition = tile.gridLocation;
            if (GameManager.Instance.MapManager.tiles.Count <= 0)
            {
                GameManager.Instance.MapManager.tiles.Add(tile);
                GameManager.Instance.MapManager.tiles[0].ShowTile();

            }        
            else
            {
                GameManager.Instance.MapManager.tiles[0].HideTile();
                GameManager.Instance.MapManager.tiles.RemoveAt(0);
                GameManager.Instance.MapManager.tiles.Add(tile);
                GameManager.Instance.MapManager.tiles[0].ShowTile();
                
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


   // void OnEnable()
   // {
   //     actions.FindActionMap("Player Turn Input").Enable();
   // }
   // void OnDisable()
   // {
   //     actions.FindActionMap("Player Turn Input").Disable();
   // }
}
