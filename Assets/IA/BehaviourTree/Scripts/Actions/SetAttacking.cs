using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class SetAttacking : ActionNode
{
    public bool valueToSet;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.isAttacking = valueToSet;
        return State.Success;
    }
}
