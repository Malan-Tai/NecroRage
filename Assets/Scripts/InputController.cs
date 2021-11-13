using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private NecrovorePlayer _player;

    void Start()
    {
        _player = this.GetComponent<NecrovorePlayer>();
    }

    void Update()
    {
        _player.SetVelocity(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump")) _player.Dash();
    }
}
