using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderCrusher : MonoBehaviour
{
    public delegate void EventCrush(Collider other);
    public event EventCrush OnCrush;

    private void OnTriggerEnter(Collider other)
    {
        if (OnCrush != null) OnCrush(other);
    }
}
