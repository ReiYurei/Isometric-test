using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButtonScript : MonoBehaviour
{
    private UIManager target;
    public Button button;

    

    private void Start()
    {
        target = GameManager.Instance.UIManager;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        target.OnClickAction(this.gameObject);
    }

}
