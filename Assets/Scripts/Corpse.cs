using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    [SerializeField]
    private float _hp;

    public delegate void EventDie();
    public event EventDie OnDeath;

    private Joint _joint;

    private void Start()
    {
        _joint = GetComponent<Joint>();
    }

    public float TakeDamage(float damage)
    {
        float actuallyTaken = Mathf.Min(_hp, damage);
        _hp -= damage;
        if (_hp <= 0)
        {
            if (OnDeath != null) OnDeath();

            //Destroy(gameObject);
            this.gameObject.SetActive(false);
        }
        return actuallyTaken;
    }

    public void StartJoint(Rigidbody body)
    {
        _joint.connectedBody = body;
    }

    public void StopJoint()
    {
        _joint.connectedBody = null;
    }
}
