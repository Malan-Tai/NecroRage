using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class isEatingCorpse : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.eatenCorpse != null)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
