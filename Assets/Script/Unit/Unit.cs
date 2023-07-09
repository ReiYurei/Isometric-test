using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Unit : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] public UnitStatus status;
    void OnEnable()
    {
        var uid = status.UID;
        UnitManager.UnitInScene.Add(uid, this.gameObject);
        //Debug.Log(UnitManager.UnitInScene[uid]);
    }
    void OnDisable()
    {
        var uid = status.UID;
        if (UnitManager.UnitInScene.ContainsKey(uid))
        {
            UnitManager.UnitInScene.Remove(uid);
        }
    }
    void Start()
    {
        status.HealthPoint = status.MaxHP;
        //Debug.Log($"{gameObject.name}'s Speed = {status.TrueUnitSpeed}");
    }
    
    public void Interact()
    {
        
    }
    public void OnDamage()
    {

    }
}
