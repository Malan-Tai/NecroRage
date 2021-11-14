using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class AnimatorSetBool : ActionNode
{
    public string boolName = "";
    public bool boolValue = true;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.animator.SetBool(boolName,boolValue);
        return State.Success;
    }
}
