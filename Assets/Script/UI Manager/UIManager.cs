using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager _instance;
    [Header("State Related UI Object")]
    [SerializeField]StateUI stateUI = new StateUI();
    [System.Serializable]
    class StateUI
    {
       public GameObject begin;
       public GameObject enemyTurn;
       public GameObject playerTurn;
    }

    [Header("Action Related UI Object")]
    [SerializeField] ActionUI actionUI = new ActionUI();
    
    [System.Serializable]
    class ActionUI
    {
        public GameObject actionCommand;
        public List<GameObject> listOfAction;
        public GameObject endTurnButtonObj;
    }

    Camera cam;
    Vector3 pos;

    StateManager stateManager;
  

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        
    }
    public void Update()
    {
        
    }
    public void Start()
    {
        cam = Camera.main;
        stateManager = GameManager.Instance.StateManager;
        stateManager.OnStateChange += OnStateChangeHandler;

    }

    IEnumerator RightToLeftSmoothStep(GameObject gameObject)
    {
        float waitTime = 12f;
        float counter = 0f;

        var obj = gameObject.GetComponent<RectTransform>();
        float minimum = 5000.0f;
        float smoothTime = 10f;
        float xVelocity = 100.0f;
        float maximum = 0.0f;
;
        float newPosition = Mathf.SmoothDamp(minimum, maximum, ref xVelocity, smoothTime);
        
        while (counter < waitTime)
        {
            obj.anchoredPosition = new Vector2(newPosition, 0f);
            counter += Time.deltaTime;
            yield return null; //Don't freeze Unity
        }
    }
    void OnStateChangeHandler(GameBaseState state)
    {
        switch (state)
        {
            case BattleBeginState:
                
                foreach (GameObject action in actionUI.listOfAction)
                {
                        action.SetActive(false);
                }
                //StartCoroutine(RightToLeftSmoothStep(stateUI.begin));
                               
                stateUI.begin.SetActive(true);
                stateUI.enemyTurn.SetActive(false);
                stateUI.playerTurn.SetActive(false);
                
                actionUI.endTurnButtonObj.SetActive(false);

                break;
            case EnemyTurnState:
                foreach (GameObject action in actionUI.listOfAction)
                    {
                        action.SetActive(false);
                    }
                stateUI.begin.SetActive(false);
                stateUI.enemyTurn.SetActive(true);
                stateUI.playerTurn.SetActive(false);

                actionUI.endTurnButtonObj.SetActive(false);

                break;

            case PlayerTurnState:
                foreach (GameObject action in actionUI.listOfAction)
                    {
                        action.SetActive(false);
                    }
                stateUI.begin.SetActive(false);
                stateUI.enemyTurn.SetActive(false);
                stateUI.playerTurn.SetActive(true);

                actionUI.endTurnButtonObj.SetActive(false);

                break;
            case PlayerActionState:
                foreach (GameObject ui in actionUI.listOfAction)
                {
                    ui.SetActive(false);
                }
                stateUI.playerTurn.SetActive(false);
                actionUI.endTurnButtonObj.SetActive(true);
                actionUI.actionCommand.SetActive(false);


                break;

            case SelectingTargetState:
                foreach (GameObject action in actionUI.listOfAction)
                {
                    action.SetActive(false);
                }
                actionUI.endTurnButtonObj.SetActive(false);
                actionUI.actionCommand.SetActive(false);
                break;
            case ActionUIState:
                var selectedInfo = GameManager.Instance.SelectionManager.SelectedInfo;
                var unitStatus = selectedInfo.UnitStatus;
                var standingObj = selectedInfo.StandingObject;
                var turnChances = unitStatus.TurnChances;


                for (int i = 1; i < turnChances + 1; i++)
                {

                    if (i == 1)
                    {
                        pos = cam.WorldToScreenPoint(standingObj.transform.position + new Vector3(-1.5f, 0, 0));
                    }
                    else if (i == 2)
                    {
                        pos = cam.WorldToScreenPoint(standingObj.transform.position + new Vector3(1.5f, 0, 0));
                    }
                    else if (i == 3)
                    {
                        pos = cam.WorldToScreenPoint(standingObj.transform.position + new Vector3(0, 1f, 0));
                    }
                    else if (i == 4)
                    {
                        pos = cam.WorldToScreenPoint(standingObj.transform.position + new Vector3(0, -1f, 0));
                    }
                    actionUI.listOfAction[i - 1].transform.position = pos;
                    actionUI.listOfAction[i - 1].SetActive(true);

                }
                stateUI.begin.SetActive(false);
                stateUI.enemyTurn.SetActive(false);

                break;
        }
    }
    public void OnClickAction(GameObject button)
    {
        var buttonPos = button.transform.position;
        actionUI.actionCommand.transform.position = new Vector3 (buttonPos.x, buttonPos.y -135f,buttonPos.z);
        actionUI.actionCommand.SetActive(true);
    }
    private void OnDisable()
    {
        stateManager.OnStateChange -= OnStateChangeHandler;
    }
}
