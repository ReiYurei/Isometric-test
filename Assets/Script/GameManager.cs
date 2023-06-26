using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public StateManager StateManager;
    public InputManager InputManager;
    public MapManager MapManager;
    public SelectionManager SelectionManager;
    public UIManager UIManager;

    void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(StateManager);
            DontDestroyOnLoad(InputManager);
            DontDestroyOnLoad(MapManager);
            DontDestroyOnLoad(SelectionManager);
            DontDestroyOnLoad(UIManager);

        }
    }
}
