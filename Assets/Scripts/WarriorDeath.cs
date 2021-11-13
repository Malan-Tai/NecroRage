using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDeath : MonoBehaviour
{
    public void Die()
    {
        if (gameObject.CompareTag("RedSoldier"))
        {
            WarriorFactory.instance.redWarriorDied(this.gameObject);
        }
        else
        {
            WarriorFactory.instance.blueWarriorDied(this.gameObject);
        }

        transform.parent.GetComponentInChildren<RangeSensor>().ClearEntities();
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
