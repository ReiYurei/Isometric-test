using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;



[System.Serializable]
public class UnitStatus
{
    public string UID { get => name; }
    [SerializeField, Required] string uid;
    public string Name { get => name; }
    [SerializeField] string name;
    public float MaxHP { get => maxHP; }
    [SerializeField] float maxHP;
    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
    [SerializeField] float healthPoint;
    public float Speed { get => speed;}
    [SerializeField] float speed;

    public List<MapTerrain> Walkable { get => walkable; }
    [SerializeField] List<MapTerrain> walkable = new List<MapTerrain>();

    public int TrueUnitSpeed
    {
        get { return Mathf.RoundToInt(10000f / Speed); }
    }

    public int TurnChances { get => turnChances; set => turnChances = value; }
    [SerializeField] int turnChances;
    public int MoveRange { get => moveRange; }
    [SerializeField] int moveRange;
    public int AttackPower { get => attackPower; }
    [SerializeField] int attackPower;
    public bool IsFriendly { get => isFriendly; }
    [SerializeField] bool isFriendly;
}
