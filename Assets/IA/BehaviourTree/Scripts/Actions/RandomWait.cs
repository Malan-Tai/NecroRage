using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class RandomWait : ActionNode
{
    public float minDuration = 0.2f;
    public float maxDuration = 3f;
    public float duration;
    float startTime;

    protected override void OnStart() {
        duration = Random.Range(minDuration,maxDuration);
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > duration) {
            return State.Success;
        }
        return State.Running;
    }
}
