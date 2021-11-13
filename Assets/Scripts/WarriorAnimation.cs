using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimation : MonoBehaviour
{
    public bool walking;
    public bool attacking;

    private void Update()
    {
        GetComponent<Animator>().SetBool("Walking", walking);
        GetComponent<Animator>().SetBool("Attacking", attacking);
    }
}
