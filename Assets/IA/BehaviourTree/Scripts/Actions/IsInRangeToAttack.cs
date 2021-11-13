using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class IsInRangeToAttack : ActionNode
{
    public float range;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Vector3.Distance(context.transform.position, context.sensor.GetNearestDetected().transform.position) <= range) return State.Success;
        return State.Failure;
    }
}
