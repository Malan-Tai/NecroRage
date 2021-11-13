using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreRangeSensor : MonoBehaviour
{
    public float range = 2;
    public List<GameObject> detectedCharacters;

    void Start()
    {
        detectedCharacters = new List<GameObject>();
    }

    void OnTriggerEnter(Collider col)
    {
        bool mustHad = false;
        if (col.CompareTag("BlueSoldier") || col.CompareTag("RedSoldier"))
        {
            mustHad = true;
        }

        if (col.CompareTag("Corpse"))
        {
            mustHad = true;
        }

        if (mustHad)
        {
            detectedCharacters.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("BlueSoldier") || col.CompareTag("RedSoldier"))
        {
            detectedCharacters.Remove(col.gameObject);
        }

        if (col.CompareTag("Corpse")) // enlever cette ligne pour qu'il se rappelle de la position
        {
            detectedCharacters.Remove(col.gameObject);
        }
    }


    public GameObject GetNearestDetected()
    {
        float distance = Mathf.Infinity;
        GameObject result = null;
        foreach (GameObject character in detectedCharacters)
        {
            if (Vector3.Distance(transform.position,character.transform.position) < distance)
            {
                result = character;
                distance = Vector3.Distance(transform.position,character.transform.position);
            }
        }
        return result;
    }

    public GameObject GetNearestCorpse()
    {
        float distance = Mathf.Infinity;
        GameObject result = null;
        foreach (GameObject character in detectedCharacters)
        {
            if (Vector3.Distance(transform.position,character.transform.position) < distance && character.CompareTag("Corpse"))
            {
                result = character;
                distance = Vector3.Distance(transform.position,character.transform.position);
            }
        }
        return result;
    }


    public bool CanSeeEntity()
    {
        return (detectedCharacters.Count > 0);
    }

    public void RefreshEntities()
    {
        for (int i = detectedCharacters.Count -1 ; i >=0 ; i--)
        {
            if (detectedCharacters[i].activeInHierarchy == false)
            {
                detectedCharacters.RemoveAt(i);
            }
        }
    }

    public void ClearEntities()
    {
        detectedCharacters.Clear();
    }

    /*
    public void OnDrawGizmosSelected()
        {

            if (!isActiveAndEnabled) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, range);
        }
        */
}
