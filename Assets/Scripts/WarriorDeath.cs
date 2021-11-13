using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDeath : MonoBehaviour
{
    public void Die()
    {
        transform.parent.gameObject.SetActive(false); // OUAIS OUAIS TOUT SA
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("RedAttack") && gameObject.CompareTag("BlueSoldier"))
        {
            Die();
        }
        if (col.CompareTag("BlueAttack") && gameObject.CompareTag("RedSoldier"))
        {
            Die();
        }
    }

}
