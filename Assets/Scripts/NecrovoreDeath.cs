using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreDeath : MonoBehaviour
{
    [SerializeField] private float distanceToDie = 50f;
    void Update()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance._player.transform.position) >= distanceToDie)
        {
            Die();
        }
    }
    public void Die()
    {
        SoundManager.PlaySound(SoundManager.Sound.Death_necrovore, transform.position,1f);
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
