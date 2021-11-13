using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 _velocity;

    void Update()
    {
        this.transform.position += _velocity * Time.deltaTime;
    }

    public void Die()
    {
        ArrowFactory.instance.arrowDied(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Component comp;
        if (other.TryGetComponent(typeof(WarriorDeath), out comp))
        {
            WarriorDeath warrior = comp as WarriorDeath;
            warrior.Die();
            Die();
        }
        else if (other.TryGetComponent(typeof(NecrovoreDeath), out comp))
        {
            NecrovoreDeath necrovore = comp as NecrovoreDeath;
            necrovore.Die();
            Die();
        }
        else if (other.TryGetComponent(typeof(NecrovorePlayer), out comp))
        {
            NecrovorePlayer necrovore = comp as NecrovorePlayer;
            necrovore.GetHit();
            Die();
        }
        else if (!other.CompareTag("Ground"))
        {
            Die();
        }
    }
}
