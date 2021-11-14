using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseFactory : MonoBehaviour
{
    public static CorpseFactory instance;

    public int corpseCounter = 3;

    [SerializeField] private GameObject corpsePrefab;
    [SerializeField] private GameObject corpseContainer;

    private Queue<GameObject> deadCorpse;

    void Start()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        deadCorpse = new Queue<GameObject>();
    }

    public GameObject GetCorpse(Vector3 position, CorpseSprites sprites)
    {
        GameObject newCorpse = null;

        if (deadCorpse.Count > 0)
        {
            newCorpse = deadCorpse.Dequeue();
            newCorpse.transform.position = position;
        }
        else
        {
            newCorpse = Instantiate(corpsePrefab, position, Quaternion.Euler(90f, 0, 0));
            newCorpse.transform.SetParent(corpseContainer.transform);
        }
        corpseCounter++;

        newCorpse.SetActive(true);
        newCorpse.GetComponent<Corpse>().SetUsedSprites(sprites);
        return newCorpse;
    }

    public void CorpseDied(GameObject corpse)
    {
        corpse.SetActive(false);
        deadCorpse.Enqueue(corpse);
        corpseCounter--;
    }
}
