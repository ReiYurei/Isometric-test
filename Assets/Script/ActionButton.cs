using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Wait = 0,
    Run = 1,
    Move = 2,
    Attack = 3,
    Talent = 4,
    Item = 5,
    Charge = 6,
    Skills = 7,
    Cancel = 8
}

public class ActionButton : MonoBehaviour
{
    [SerializeField] ActionType buttonType;

    void Start()
    {
        var button = gameObject.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(Action); 
        
    }
  
    void Action()
    {
        OnAction(buttonType);

    }
    private void Update()
    {
        
    }
    void OnAction(ActionType type)
    {
        switch ((int)type)
        {
            case 0:
                break;
            case 1:
                break;
            case 2: //Move
                if (GameManager.Instance.MapManager.Tiles[0].UnitObject != null)
                {
                    foreach (KeyValuePair<Vector2Int, Tile> tiles in GameManager.Instance.MapManager.map)
                    {
                        if (tiles.Value.Terrain == MapTerrain.Ground)
                        {
                            tiles.Value.ShowTile();
                        }

                    }
                }
                
                break;
            case 8: //Cancel
                foreach (KeyValuePair<Vector2Int, Tile> tiles in GameManager.Instance.MapManager.map)
                {
                    tiles.Value.HideTile();
                }
                break;
        }
    }
}
