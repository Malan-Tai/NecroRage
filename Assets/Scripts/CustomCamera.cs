using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3 _targetPosition;

    private void Update()
    {
        this.transform.position += (_targetPosition - this.transform.position) * _speed;
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }
}
