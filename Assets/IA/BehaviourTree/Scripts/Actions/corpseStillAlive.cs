using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class corpseStillAlive : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.eatenCorpse.gameObject.activeInHierarchy == false)
        {
            blackboard.eatenCorpse.StopJoint();
            blackboard.eatenCorpse = null;
            context.necrovoreSensor.RefreshEntities();
            return State.Failure;
        }
        return State.Success;
    }
}
