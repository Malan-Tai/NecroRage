using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class RunAwayFromEntity : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector3 pushAway = context.transform.position - blackboard.closestEntity.transform.position;
        blackboard.moveToPosition.x = context.transform.position.x + pushAway.normalized.x * 5f;
        blackboard.moveToPosition.z = context.transform.position.z + pushAway.normalized.z * 5f;
        return State.Success;
    }
}
