using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class COOL : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Debug.Log(blackboard.isAttacking);
        return State.Success;
    }
}
