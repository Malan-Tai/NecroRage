using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTreeAI;

public class RandomPosition : ActionNode
{
    public float minDistance = 5f;
    public float maxDistance = 10f;
    public Vector2 max = Vector2.one * 10;
    public float centerProximity = 40;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        int willGoNearCenter = Random.Range(0,4);

        if (willGoNearCenter == 0)
        {
            blackboard.moveToPosition.x = Random.Range(-centerProximity, centerProximity);
            blackboard.moveToPosition.z = Random.Range(-centerProximity, centerProximity);
        }

        else
        {
            float posX = Random.Range(-maxDistance, maxDistance);
            while (posX < minDistance && posX > -minDistance)
            {
                posX = Random.Range(-maxDistance, maxDistance);
            }
            float posY = Random.Range(-maxDistance, maxDistance);
            while (posY < minDistance && posY > -minDistance)
            {
                posY = Random.Range(-maxDistance, maxDistance);
            }

            blackboard.moveToPosition.x = context.transform.position.x + posX;
            blackboard.moveToPosition.z = context.transform.position.z + posY;
        }
        
        return State.Success;
    }
}
