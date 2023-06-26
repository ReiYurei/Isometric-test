using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextShown : MonoBehaviour
{

    TextMeshProUGUI textComponent;
    void Start()
    {
        var turnCount = GameManager.Instance.StateManager.turnCount;

        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = $"TURN {turnCount} START";
    }

    
}
