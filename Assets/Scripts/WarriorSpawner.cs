using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSpawner : MonoBehaviour
{
    public static WarriorSpawner instance;

    [SerializeField] private float spawnZoneWidth = 200;
    [SerializeField] private float spawnZoneHeight = 200;

    [SerializeField] private int startSpawnThreshold = 15;
    [SerializeField] private float spawnAwayFromPlayer = 15;
    [SerializeField] private float spawnBurstRadius = 10;

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
        int blueCount = WarriorFactory.instance.blueCounter;
        int redCount = WarriorFactory.instance.redCounter;

        //Debug.Log(blueCount);

        if (blueCount <= startSpawnThreshold) SpawnBurst(true);
        if (redCount <= startSpawnThreshold) SpawnBurst(false);
    }

    void SpawnBurst(bool blueSpawn = true)
    {

        Vector3 playerPosition = GameManager.Instance._player.transform.position; // CHANGER AVEC LA VRAIE POSITION

        Vector3 spawnRegion = playerPosition;

        while (spawnRegion.x < playerPosition.x+spawnAwayFromPlayer  && spawnRegion.x > playerPosition.x-spawnAwayFromPlayer && spawnRegion.z < playerPosition.z+spawnAwayFromPlayer  && spawnRegion.z > playerPosition.z-spawnAwayFromPlayer)
        {
            spawnRegion = new Vector3(Random.Range(-spawnZoneWidth/2, spawnZoneWidth/2), 0.5f,  Random.Range(-spawnZoneHeight/2, spawnZoneHeight/2));
        }


        int blueToSpawn = blueSpawn ? 1 : 0;
        int redToSpawn = blueSpawn ? 0 : 1;

        for (int i = 0; i < blueToSpawn ; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(spawnRegion.x + Random.Range(-spawnBurstRadius, spawnBurstRadius), 0.5f, spawnRegion.z + Random.Range(-spawnBurstRadius, spawnBurstRadius));
            WarriorFactory.instance.GetBlueWarrior(randomSpawnPosition);  
        }
        
        for (int i = 0; i < redToSpawn ; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(spawnRegion.x + Random.Range(-spawnBurstRadius, spawnBurstRadius), 0.5f, spawnRegion.z + Random.Range(-spawnBurstRadius, spawnBurstRadius));
            WarriorFactory.instance.GetRedWarrior(randomSpawnPosition);  
        }

    }
}
