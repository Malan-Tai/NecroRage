using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class GrabCorpse : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        GameObject tmp = context.necrovoreSensor.GetNearestCorpse();
        if (blackboard.eatenCorpse == null) return State.Failure;
        blackboard.eatenCorpse = tmp.GetComponent<Corpse>();
        blackboard.eatenCorpse.StartJoint(context.physics);
    
        return State.Success;
    }
}


