using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class CanSeeWarrior : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.necrovoreSensor.CanSeeEntity())
        {
            blackboard.closestEntity = context.necrovoreSensor.GetNearestDetected();
            if (blackboard.closestEntity.CompareTag("RedSoldier") || blackboard.closestEntity.CompareTag("BlueSoldier"))
            {
                return State.Success;
            }
        } 
        return State.Failure;
    }
}
