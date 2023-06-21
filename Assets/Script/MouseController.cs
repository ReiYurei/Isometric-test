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

    private InputActionAsset actions;

    private void Awake()
    {
        actions = GameManager.Instance.InputManager.Actions;
        actions.FindAction("Click").performed += OnClick;

    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(actions.FindAction("MousePos").ReadValue<Vector2>());
        mousePos2d = new Vector2(mousePos.x, mousePos.y);
    }
    void LateUpdate()
    {
        OnMouseHover();
    }
    void OnMouseHover()
    {
        var hoveredTile = GetPositionOnTile();
        if (hoveredTile.HasValue)
        {
            var tile = hoveredTile.Value.collider.gameObject.GetComponent<Tile>();
            transform.position = tile.WorldSpacePos;
            GameManager.Instance.SelectionManager.OnHoverInfo(tile, tile.UnitObject);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        var selectedTile = GetPositionOnTile();
        if (selectedTile.HasValue)
        {
            var tile = selectedTile.Value.collider.gameObject.GetComponent<Tile>();
            if (GameManager.Instance.MapManager.Tiles.Count <= 0)
            {
                GameManager.Instance.MapManager.Tiles.Add(tile);
                GameManager.Instance.MapManager.Tiles[0].ShowTile();
                GameManager.Instance.SelectionManager.OnSelectInfo(tile, tile.UnitObject, tile.TileKey, tile.WorldSpacePos);


            }        
            else
            {
                GameManager.Instance.MapManager.Tiles[0].HideTile();
                GameManager.Instance.MapManager.Tiles.RemoveAt(0);
                GameManager.Instance.MapManager.Tiles.Add(tile);
                GameManager.Instance.MapManager.Tiles[0].ShowTile();
                GameManager.Instance.SelectionManager.OnSelectInfo(tile, tile.UnitObject, tile.TileKey, tile.WorldSpacePos);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            GetGameObject(collision.gameObject);
        }
;
    }

    GameObject GetGameObject(GameObject standingObject)
    {
        return standingObject;
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
