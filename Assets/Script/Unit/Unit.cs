using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Unit : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] public UnitStatus status;
    void Start()
    {
        status.healthPoint = status.MaxHP;
        Debug.Log($"{gameObject.name}'s Speed = {status.unitSpeed}");
    }
    
    public void Interact()
    {
        
    }
    public void OnDamage()
    {

    }
}
