using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public static BoulderSpawner instance;

    [SerializeField] private float _spawnradius = 15;

    [SerializeField] private int startSpawnThreshold = 1;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] private GameObject _boulderPrefab;

    private int _airBoulderCount = 0;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        Boulder.OnLand += BoulderLand;
    }

    private void OnDisable()
    {
        Boulder.OnLand -= BoulderLand;
    }

    void Update()
    {
        if (_airBoulderCount < startSpawnThreshold)
        {
            Vector3 unit = _playerTransform.position + Random.insideUnitSphere * _spawnradius;
            unit.y = 0;

            GameObject newBoulder = Instantiate(_boulderPrefab, this.transform);
            newBoulder.transform.position = unit;

            _airBoulderCount++;
        }
    }

    private void BoulderLand()
    {
        _airBoulderCount--;
    }
}
