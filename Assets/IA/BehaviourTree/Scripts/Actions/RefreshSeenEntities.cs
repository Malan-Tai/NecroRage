using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class RefreshSeenEntities : ActionNode
{
    protected override void OnStart() {
        context.sensor.RefreshEntities();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
