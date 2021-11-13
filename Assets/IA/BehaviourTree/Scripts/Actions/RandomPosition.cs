using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class RandomPosition : ActionNode
{
    public Vector2 min = Vector2.one * -10;
    public Vector2 max = Vector2.one * 10;
    public float centerProximity = 40;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        int willGoNearCenter = Random.Range(0,4);

        if (willGoNearCenter == 0)
        {
            blackboard.moveToPosition.x = Random.Range(-centerProximity, centerProximity);
            blackboard.moveToPosition.z = Random.Range(-centerProximity, centerProximity);
        }

        else
        {
            blackboard.moveToPosition.x = context.transform.position.x + Random.Range(min.x, max.x);
            blackboard.moveToPosition.z = context.transform.position.z + Random.Range(min.y, max.y);
        }
        
        return State.Success;
    }
}
