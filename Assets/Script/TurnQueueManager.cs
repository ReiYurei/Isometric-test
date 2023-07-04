using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnQueueManager : MonoBehaviour
{
    private List<IBattleCommand> commandsQueue = new List<IBattleCommand>();
    ActionHandler ActionCommand;
    StateManager state;

    void OnEnable()
    {

        if (ActionCommand == null && state == null)
        {
            try
            {
                ActionCommand = GameObject.FindGameObjectWithTag("ActionHandler").GetComponent<ActionHandler>();
                state = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>();
            }
            catch
            {
                Debug.LogWarning("ERROR : NO StateManager or ActionHandler FOUND!");
            }
            
        }
        ActionCommand.AddedCommand += OnAddCommand;
        
    }
    void OnDisable()
    {
        ActionCommand.AddedCommand -= OnAddCommand;
    }
    void OnAddCommand(IBattleCommand command)
    {
        commandsQueue.Add(command);
    }
    void Update()
    {
        SortQueue();
    }
  

    public void SortQueue()
    {
        var comparer = new TurnCommandComparer();
        commandsQueue.Sort(comparer);
    }
    public void RemoveCommand()
    {
        commandsQueue.RemoveAt(0);
    }

    public void _ExecuteTurn()
    {
        foreach (IBattleCommand command in commandsQueue)
        {
            Debug.Log(command);

        }
        if (commandsQueue.Count == 0)
        {
            state.SetState(state.BattleBeginState);
            return;
        }
        StopCoroutine("OnExecute");
        StartCoroutine("OnExecute");
    }
    IEnumerator OnExecute()
    {
      
        
        if (commandsQueue.Count == 0)
        {
            _ExecuteTurn();
            yield break;
        }
        if (commandsQueue[0].IsFinished == true)
        {
            RemoveCommand();
            _ExecuteTurn();
            yield break;

        }
        while (commandsQueue[0].IsFinished != true)
        {
            commandsQueue[0].Execute();
            yield return null;
        }
        _ExecuteTurn();
        yield break;




    }
}
