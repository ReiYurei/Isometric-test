using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommandInterface : ICommand 
{
   
    string command = "Hihihihi";

    public int MotionWeight { get; set; }
    public int UnitSpeed { get; set; }
    public void Execute()
    {
        Debug.Log(command);
    }
}
public class TestCommandInterface2 : ICommand
{
    public int MotionWeight { get; set; }
    public int UnitSpeed { get; set; }
    public GameObject obj;
    string command = "Ayy lmao";
    public void Execute()
    {
        Debug.Log(command);
    }
}

public class TestCommandComparer : IComparer<ICommand>
{
    public int Compare(ICommand x, ICommand y)
    {
        if (x.MotionWeight.CompareTo(y.MotionWeight) == 0)
        {
            return Compare2(x, y);

        }
        return x.MotionWeight.CompareTo(y.MotionWeight);
    }

    public int Compare2(ICommand x, ICommand y)
    {
        return x.UnitSpeed.CompareTo(y.UnitSpeed);
    }
}