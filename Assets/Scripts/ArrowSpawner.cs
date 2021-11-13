using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public static ArrowSpawner instance;

    [SerializeField] private float _spawnradius = 15;

    [SerializeField] private int startSpawnThreshold = 3;

    [SerializeField] private float _arrowSpeed = 10;

    [SerializeField] private Transform _playerTransform;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    void Update()
    {
        int arrowCount = ArrowFactory.instance.arrowCounter;

        if (arrowCount <= startSpawnThreshold)
        {
            Vector3 unit = _playerTransform.position + Random.onUnitSphere * _spawnradius;
            unit.y = 0;

            GameObject arrow = ArrowFactory.instance.GetArrow(unit);
            arrow.GetComponent<Arrow>().SetVelocity((_playerTransform.position - unit) * _arrowSpeed);
            arrow.transform.eulerAngles = new Vector3(90f, 0f, 0f);
        }
    }
}
