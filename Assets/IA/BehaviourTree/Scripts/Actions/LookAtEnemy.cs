using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class LookAtEnemy : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        Vector3 targetDirection = (blackboard.closestEntity.transform.position - context.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //Debug.Log(targetRotation);
        
        Quaternion nextRotation = Quaternion.Lerp(context.transform.rotation, targetRotation, 0.1f);
        context.transform.rotation = nextRotation;
        
        //context.transform.LookAt(blackboard.closestEntity.transform.position);
        return State.Success;
    }
}
