using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 _velocity;
    private const float LIFETIME = 10;
    private float _currentLife = 0f;

    void Update()
    {
        this.transform.position += _velocity * Time.deltaTime;

        _currentLife += Time.deltaTime;
        if (_currentLife >= LIFETIME)
        {
            Die();
        }
    }

    public void Die()
    {
        ArrowFactory.instance.arrowDied(this.gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _currentLife = 0f;
        _velocity = velocity;
        transform.rotation = Quaternion.LookRotation(Vector3.up, _velocity);
        //print("life:" + _currentLife + "," + LIFETIME + " ; _velocity:" + _velocity);
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
