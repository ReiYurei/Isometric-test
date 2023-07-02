using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AttackCommand : IBattleCommand
{
    public bool IsFinished { get; set; }
    public int Priority { get; set; }
    public int MotionWeight { get; set; }
    public int TrueUnitSpeed { get; set; }

  
    public void Execute()
    {

    }

    public void OnUpdate()
    {
    }
}
