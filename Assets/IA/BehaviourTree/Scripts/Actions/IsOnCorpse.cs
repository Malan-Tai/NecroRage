using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class IsOnCorpse : ActionNode
{
    public float range;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, blackboard.closestEntity.transform.position) <= range)
        {
            return State.Success;
        } 
        else
        {
            //Debug.Log("NOTINRANGE");
            return State.Failure;
        }
        
    }
}
