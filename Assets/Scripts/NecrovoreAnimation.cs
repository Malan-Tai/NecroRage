using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreAnimation : MonoBehaviour
{
    Rigidbody rb;
    NecrovorePlayer player;

    private void Start()
    {
        player = transform.parent.GetComponent<NecrovorePlayer>();
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("Walking", player.Walking);
    }
}
