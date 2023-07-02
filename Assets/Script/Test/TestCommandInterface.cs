using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommandInterface //: IBattleCommand
{
   
    string command = "Hihihihi";
    public int Priority { get; set; }
    public bool IsFinished { get; set; }
    public int MotionWeight { get; set; }
    public int TrueUnitSpeed { get; set; }
    public void Execute()
    {
        Debug.Log(command);
    }
}
public class TestCommandInterface2 //: IBattleCommand
{
    public int Priority { get; set; }
    public bool IsFinished { get; set; }

    public int MotionWeight { get; set; }
    public int TrueUnitSpeed { get; set; }
    public GameObject obj;
    string command = "Ayy lmao";
    public void Execute()
    {
        Debug.Log(command);
    }
}

public class TurnCommandComparer : IComparer<IBattleCommand>
{
    public int Compare(IBattleCommand x, IBattleCommand y)
    {
        if (x.Priority.CompareTo(y.Priority) == 0)
        {
            return Compare2(x, y);

        }
        return x.Priority.CompareTo(y.Priority);
    }

    public int Compare2(IBattleCommand x, IBattleCommand y)
    {
        return x.TrueUnitSpeed.CompareTo(y.TrueUnitSpeed);
    }
}