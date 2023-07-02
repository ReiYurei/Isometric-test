using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IBattleCommand
{
    bool IsFinished { get; }
    int Priority { get; }
    int MotionWeight { get; }
    int  TrueUnitSpeed { get; }
    void Execute();

}

public interface IDamageable
{
    void OnDamage();
}