using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreFactory : MonoBehaviour
{
    public static NecrovoreFactory instance;

    public int necrovoreCounter;
    
    [SerializeField] private GameObject necrovorePrefab;
    [SerializeField] private GameObject necrovoreContainer;

    private Queue<GameObject> deadNecrovore;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        deadNecrovore = new Queue<GameObject>();
    }

    public GameObject GetNecrovore(Vector3 position)
    {
        GameObject newNecrovore = null;

        if (deadNecrovore.Count>0)
        {
            newNecrovore = deadNecrovore.Dequeue();
            newNecrovore.SetActive(true);
            newNecrovore.transform.position = position;
        }
        else
        {
            newNecrovore = Instantiate(necrovorePrefab, position, Quaternion.identity); 
            newNecrovore.transform.SetParent(necrovoreContainer.transform);
            
        }
        necrovoreCounter++;
        return newNecrovore;
    }

    public void necrovoreDied(GameObject necrovore)
    {
        necrovore.transform.parent.gameObject.SetActive(false);
        deadNecrovore.Enqueue(necrovore.transform.parent.gameObject);
        necrovoreCounter--;
    }
}
