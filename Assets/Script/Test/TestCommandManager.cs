using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommandManager : MonoBehaviour
{
    [SerializeField] List<ICommand> commands = new List<ICommand>();

    
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int mw = Random.Range(1, 10);
            var objA = new TestCommandInterface();
            objA.MotionWeight = mw;
            objA.UnitSpeed = mw;
            commands.Add(objA);
            Debug.Log($"Added {objA}");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            int mw = Random.Range(11, 20);
            var objB = new TestCommandInterface2();
            objB.MotionWeight = mw;
            objB.UnitSpeed = mw;
            commands.Add(objB);
            Debug.Log($"Added {objB}");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (ICommand command in commands)
            {
                command.Execute();
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (ICommand command in commands)
            {
                Debug.Log(command.MotionWeight);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var comparer = new TestCommandComparer();

            commands.Sort(comparer);
        }
    }
}
