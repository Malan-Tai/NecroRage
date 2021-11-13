using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreDeath : MonoBehaviour
{
    public void Die()
    {
        NecrovoreFactory.instance.necrovoreDied(this.gameObject);

        transform.parent.GetComponentInChildren<NecrovoreRangeSensor>().ClearEntities();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("RedAttack") || col.CompareTag("BlueAttack"))
        {
            Die();
        }
    }

}
