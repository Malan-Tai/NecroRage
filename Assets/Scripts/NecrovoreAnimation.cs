using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreAnimation : MonoBehaviour
{
    NecrovorePlayer player;

    private void Start()
    {
        player = transform.parent.GetComponent<NecrovorePlayer>();
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("Walking", player.Walking);
        GetComponent<Animator>().SetBool("Dashing", player.Dashing);
        GetComponent<Animator>().SetBool("Eating", player.Eating);
    }
}
