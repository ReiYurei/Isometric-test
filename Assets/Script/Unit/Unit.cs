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
        status.HealthPoint = status.MaxHP;
        Debug.Log($"{gameObject.name}'s Speed = {status.UnitSpeed}");
    }
    
    public void Interact()
    {
        
    }
    public void OnDamage()
    {

    }
}
