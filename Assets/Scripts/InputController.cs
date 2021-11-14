using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private NecrovorePlayer _player;

    [SerializeField]
    private float _eatCooldown = 1f;
    private float _curCooldown;

    void Start()
    {
        _player = this.GetComponent<NecrovorePlayer>();
        _curCooldown = _eatCooldown;
    }

    void Update()
    {
        _curCooldown += Time.deltaTime;
        _player.SetVelocity(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump")) _player.Dash();

        if (Input.GetButtonDown("Eat") && _curCooldown >= _eatCooldown) _player.StartEating();
        else if (Input.GetButtonUp("Eat"))
        {
            if (_player.StopEating()) _curCooldown = 0f;
        }
    }
}
