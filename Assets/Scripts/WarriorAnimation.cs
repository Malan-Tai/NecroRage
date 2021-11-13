using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnimation : MonoBehaviour
{
    public bool walking;

    private void Update()
    {
        GetComponent<Animator>().SetBool("Walking", walking);
    }
}
