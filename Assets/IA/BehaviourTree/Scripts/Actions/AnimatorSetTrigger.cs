using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class AnimatorSetTrigger : ActionNode
{
    public string triggerName = "";

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.animator.SetTrigger(triggerName);
        return State.Success;
    }
}
