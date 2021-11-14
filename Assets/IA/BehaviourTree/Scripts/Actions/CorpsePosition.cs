using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class CorpsePosition : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.moveToPosition.x = context.necrovoreSensor.GetNearestCorpse().transform.position.x;
        blackboard.moveToPosition.z = context.necrovoreSensor.GetNearestCorpse().transform.position.z;
        return State.Success;
    }
}
