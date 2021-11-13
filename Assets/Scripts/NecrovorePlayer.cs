using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovorePlayer : MonoBehaviour
{
    private CustomCamera _camera;

    [SerializeField]
    private float _maxHunger;
    private float _hunger;
    [SerializeField]
    private float _hungerDrainRate;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _dashSpeedBonus;
    [SerializeField]
    private float _dashDuration;
    private float _currentDashTime = 0f;
    private bool _dashing = false;
    [SerializeField]
    private float _dashHungerDrain;

    private Vector3 _velocity;

    private void Start()
    {
        _camera = Camera.main.GetComponent<CustomCamera>();
    }

    void Update()
    {
        this.transform.position += _velocity * Time.deltaTime;

        _camera.SetTargetPosition(this.transform.position + _velocity.normalized * 2f + new Vector3(0, 10, 0));

        _currentDashTime += Time.deltaTime;
        if (_dashing && _currentDashTime >= _dashDuration)
        {
            _dashing = false;
        }

        _hunger -= _hungerDrainRate * Time.deltaTime;
    }

    public void Dash()
    {
        if (_dashing) return;

        _dashing = true;
        _currentDashTime = 0f;

        _velocity *= _dashSpeedBonus;
    }

    public void SetVelocity(float x, float y)
    {
        if (_dashing) return;

        _velocity = new Vector3(x, 0, y).normalized * _speed;
        _hunger -= _dashHungerDrain;

        if (_velocity.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up, _velocity);
        }
    }
}
