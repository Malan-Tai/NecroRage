using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class Attack : ActionNode
{   
    public float duration = 0.1f;
    private float startTime = 0f;
    protected override void OnStart() {
        startTime = Time.time;
        context.attackCollider.gameObject.SetActive(true);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime < duration)
        {
            return State.Running;
        }
        else
        {
            context.attackCollider.gameObject.SetActive(false);
            return State.Success;
        }
    }
}
