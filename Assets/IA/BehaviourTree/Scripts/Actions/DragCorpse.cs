using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class DragCorpse : ActionNode
{
    public float eatingDamages = 5f;

    public float speed = 5;
    public float stoppingDistance = 0.5f;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    protected override void OnStart() {
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.moveToPosition;
        context.agent.updateRotation = false;
        context.agent.acceleration = acceleration;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.eatenCorpse == null)
        {
            return State.Failure;
        }

        blackboard.eatenCorpse.TakeDamage(eatingDamages * Time.deltaTime);
        //context.transform.Find("necrovore sprite").LookAt(blackboard.eatenCorpse.transform.position);
        context.transform.rotation = Quaternion.LookRotation(-context.agent.velocity, Vector3.up);

        if (blackboard.eatenCorpse.gameObject.activeInHierarchy == false)
        {
            blackboard.eatenCorpse.StopJoint();
            blackboard.eatenCorpse = null;
            context.necrovoreSensor.RefreshEntities();
            return State.Failure;
        }

        if (context.agent.pathPending) {
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance) {
            return State.Success;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
            return State.Failure;
        }
        return State.Running;
    }
}
