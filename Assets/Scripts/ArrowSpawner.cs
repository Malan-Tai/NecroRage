using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public static ArrowSpawner instance;

    [SerializeField] private float _spawnradius = 15;

    [SerializeField] private int startSpawnThreshold = 3;
    private int minSpawnThreshold;
    [SerializeField] private int maxSpawnThreshold = 7;
    [SerializeField] private float timeToMaxThreshold = 60f;
    private float _currentThreshold;

    [SerializeField] private float _arrowSpeed = 2;

    [SerializeField] private Transform _playerTransform;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        minSpawnThreshold = startSpawnThreshold;
        _currentThreshold = startSpawnThreshold;
    }

    void Update()
    {
        int arrowCount = ArrowFactory.instance.arrowCounter;

        _currentThreshold += ((maxSpawnThreshold - minSpawnThreshold) / timeToMaxThreshold) * Time.deltaTime;
        startSpawnThreshold = Mathf.FloorToInt(_currentThreshold);
        //print(startSpawnThreshold);

        if (arrowCount <= startSpawnThreshold)
        {
            Vector3 unit = _playerTransform.position + Random.onUnitSphere * _spawnradius;
            unit.y = 0;

            GameObject arrow = ArrowFactory.instance.GetArrow(unit);
            arrow.transform.eulerAngles = new Vector3(90f, 0f, 0f);
            arrow.GetComponent<Arrow>().SetVelocity((_playerTransform.position - unit).normalized * _arrowSpeed);
        }
    }
}
