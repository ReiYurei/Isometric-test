using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface ICommand
{
    int MotionWeight { get; }
    int UnitSpeed { get; }
    void Execute(); 
}