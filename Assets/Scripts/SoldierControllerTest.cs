using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierControllerTest : MonoBehaviour
{
    NavMeshAgent agent;

    public RangeSensor sensor;
 
    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {

        if ( Input.GetMouseButtonDown (0)){ 
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if ( Physics.Raycast (ray,out hit,100.0f)) {
                agent.SetDestination(hit.point);
                //Debug.Log(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(sensor.GetNearestDetected());
        }
    }

    

}
