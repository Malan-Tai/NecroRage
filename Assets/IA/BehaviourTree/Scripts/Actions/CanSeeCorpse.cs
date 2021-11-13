using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class CanSeeCorpse : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.necrovoreSensor.CanSeeEntity())
        {
            blackboard.closestEntity = context.necrovoreSensor.GetNearestDetected();
            if (blackboard.closestEntity.CompareTag("Corpse"))
            {
                return State.Success;
            }
        } 
        return State.Failure;
    }
}
