using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[System.Serializable]
public class UnitStatus 
{
    public float MaxHP;
    public float healthPoint;
    public float Speed;

    public int unitSpeed
    {
        get { return Mathf.RoundToInt(10000f / Speed); }
    }

    public int TurnChances;
    public int MoveRange;
    public int AttackPower;
    [SerializeField] private bool isFriendly;
}
