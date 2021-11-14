using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public static BoulderSpawner instance;

    [SerializeField] private float _spawnradius = 15;

    [SerializeField] private int startSpawnThreshold = 1;
    private int minSpawnThreshold;
    [SerializeField] private int maxSpawnThreshold = 5;
    [SerializeField] private float timeToMaxThreshold = 60f;
    private float _currentThreshold;

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

        minSpawnThreshold = startSpawnThreshold;
        _currentThreshold = startSpawnThreshold;
        Boulder.OnLand += BoulderLand;
    }

    private void OnDisable()
    {
        Boulder.OnLand -= BoulderLand;
    }

    void Update()
    {
        _currentThreshold += ((maxSpawnThreshold - minSpawnThreshold) / timeToMaxThreshold) * Time.deltaTime;
        startSpawnThreshold = Mathf.FloorToInt(_currentThreshold);

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
