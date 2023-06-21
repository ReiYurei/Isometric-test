using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IBattleCommand
{
    int MotionWeight { get; }
    int  UnitSpeed { get; }
    void Execute(); 
}

public interface IDamageable
{
    void OnDamage();
}