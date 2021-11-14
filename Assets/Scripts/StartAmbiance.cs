using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAmbiance : MonoBehaviour
{
    void Start()
    {
        SoundAssets.instance.PlayAmbiantWithFade(0.5f);
    }

}
