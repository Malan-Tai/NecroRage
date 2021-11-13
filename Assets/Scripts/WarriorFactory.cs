using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorFactory : MonoBehaviour
{
    public static WarriorFactory instance;

    public int redCounter;
    public int blueCounter;
    
    [SerializeField] private GameObject redWarriorPrefab;
    [SerializeField] private GameObject blueWarriorPrefab;
    [SerializeField] private GameObject warriorContainer;

    private Queue<GameObject> deadRedWarriors;
    private Queue<GameObject> deadBlueWarriors;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        deadBlueWarriors = new Queue<GameObject>();
        deadRedWarriors = new Queue<GameObject>();
    }

    public GameObject GetRedWarrior(Vector3 position)
    {
        GameObject newWarrior = null;

        if (deadRedWarriors.Count>0)
        {
            newWarrior = deadRedWarriors.Dequeue();
            newWarrior.SetActive(true);
            newWarrior.transform.position = position;
        }
        else
        {
            newWarrior = Instantiate(redWarriorPrefab, position, Quaternion.identity); 
            newWarrior.transform.SetParent(warriorContainer.transform);
            
        }
        redCounter++;
        return newWarrior;
    }

    public void redWarriorDied(GameObject warrior)
    {
        warrior.transform.parent.gameObject.SetActive(false);
        deadRedWarriors.Enqueue(warrior.transform.parent.gameObject);
        redCounter--;
    }

    public GameObject GetBlueWarrior(Vector3 position)
    {
        GameObject newWarrior = null;

        if (deadBlueWarriors.Count>0)
        {
            newWarrior = deadBlueWarriors.Dequeue();
            newWarrior.SetActive(true);
            newWarrior.transform.position = position;
        }
        else
        {
            newWarrior = Instantiate(blueWarriorPrefab, position, Quaternion.identity); 
            newWarrior.transform.SetParent(warriorContainer.transform);
            
        }
        blueCounter++;
        return newWarrior;
    }

    public void blueWarriorDied(GameObject warrior)
    {
        warrior.transform.parent.gameObject.SetActive(false);
        deadBlueWarriors.Enqueue(warrior.transform.parent.gameObject);
        blueCounter--;
    }
}
