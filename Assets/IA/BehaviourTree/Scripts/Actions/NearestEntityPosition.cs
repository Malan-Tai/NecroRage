using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class NearestEntityPosition : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.moveToPosition.x = blackboard.closestEntity.transform.position.x;
        blackboard.moveToPosition.z = blackboard.closestEntity.transform.position.z;
        return State.Success;
    }
}
