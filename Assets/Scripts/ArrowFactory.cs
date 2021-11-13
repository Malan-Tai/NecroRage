using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour
{
    public static ArrowFactory instance;

    public int arrowCounter;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowContainer;

    private Queue<GameObject> deadArrow;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        deadArrow = new Queue<GameObject>();
    }

    public GameObject GetArrow(Vector3 position)
    {
        GameObject newArrow = null;

        if (deadArrow.Count > 0)
        {
            newArrow = deadArrow.Dequeue();
            newArrow.SetActive(true);
            newArrow.transform.position = position;
        }
        else
        {
            newArrow = Instantiate(arrowPrefab, position, Quaternion.identity);
            newArrow.transform.SetParent(arrowContainer.transform);
        }
        arrowCounter++;
        return newArrow;
    }

    public void arrowDied(GameObject arrow)
    {
        arrow.gameObject.SetActive(false);
        deadArrow.Enqueue(arrow.gameObject);
        arrowCounter--;
    }
}
