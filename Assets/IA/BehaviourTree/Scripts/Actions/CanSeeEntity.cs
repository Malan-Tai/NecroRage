using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class CanSeeEntity : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.sensor.CanSeeEntity())
        {
            blackboard.closestEntity = context.sensor.GetNearestDetected();
            return State.Success;
        }
        return State.Failure;
    }
}
