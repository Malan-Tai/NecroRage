using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreSpawner : MonoBehaviour
{
    public static NecrovoreSpawner instance;

    [SerializeField] private float spawnZoneWidth = 200;
    [SerializeField] private float spawnZoneHeight = 200;

    [SerializeField] private int startSpawnThreshold = 20;
    [SerializeField] private float spawnAwayFromPlayer = 20;
    [SerializeField] private float spawnBurstRadius = 10;
    [SerializeField] private int maxNumberSpawnedAtOnce = 5;

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
        int necrovoreCount = NecrovoreFactory.instance.necrovoreCounter;

        if (necrovoreCount <= startSpawnThreshold)
        {
            SpawnBurst();
        } 
    }

    void SpawnBurst()
    {

        Vector3 playerPosition = new Vector3(0f,0.5f,0f); // CHANGER AVEC LA VRAIE POSITION

        Vector3 spawnRegion = playerPosition;

        while (spawnRegion.x < playerPosition.x+spawnAwayFromPlayer  && spawnRegion.x > playerPosition.x-spawnAwayFromPlayer && spawnRegion.z < playerPosition.z+spawnAwayFromPlayer  && spawnRegion.z > playerPosition.z-spawnAwayFromPlayer)
        {
            spawnRegion = new Vector3(Random.Range(-spawnZoneWidth/2, spawnZoneWidth/2), 0.5f,  Random.Range(-spawnZoneHeight/2, spawnZoneHeight/2));
        }

        int necrovoreToSpawn = Random.Range(1,maxNumberSpawnedAtOnce+1);

        for (int i = 0; i <  necrovoreToSpawn; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(spawnRegion.x + Random.Range(-spawnBurstRadius, spawnBurstRadius), 0.5f, spawnRegion.z + Random.Range(-spawnBurstRadius, spawnBurstRadius));
            NecrovoreFactory.instance.GetNecrovore(randomSpawnPosition);  
        }

    }
}
