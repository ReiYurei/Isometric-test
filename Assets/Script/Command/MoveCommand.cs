using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class MoveCommand : IBattleCommand
{
    public GameObject caster;
    public Tile startTile;
    public Tile targetTile;
    public List<Tile> path;
    public float moveRange;
    public List<TerrainTypes> walkable;
    public bool IsFinished { get; set; }
    public int Priority { get; set ; }

    public int MotionWeight { get; set; }
    public int TrueUnitSpeed { get; set; }


    public void OnClick()
    {       
    }
    public void Execute()
    {
        Move();
    }
    void Move()
    {
        if (path.Count > 0)
        {
            var worldPos = path[0].WorldSpacePos;
            var desiredPos = new Vector3(worldPos.x, worldPos.y + 0.25f, caster.transform.position.z);
            caster.transform.position = Vector2.MoveTowards(caster.transform.position, desiredPos, 5f*Time.deltaTime);
            if (Vector2.Distance(caster.transform.position, desiredPos) < 0.0001f)
            {
                path.RemoveAt(0);
            }
            if (path.Count <= 0)
            {
                IsFinished = true;
            }
        }

    }

}
