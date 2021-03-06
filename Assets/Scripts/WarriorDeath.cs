using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDeath : MonoBehaviour
{
    [SerializeField] private Corpse corpsePrefab;
    [SerializeField] private Transform corpseContainer;

    [SerializeField] private float distanceToDie = 50f;


    public void Start()
    {
        corpseContainer = GameObject.Find("CorpseContainer").transform;
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance._player.transform.position) >= distanceToDie)
        {
            if (gameObject.CompareTag("RedSoldier"))
            {
                WarriorFactory.instance.redWarriorDied(this.gameObject);
            }
            else
            {
                WarriorFactory.instance.blueWarriorDied(this.gameObject);
            }
        }
    }

    public void Die()
    {
    //    Corpse newCorpse = Instantiate(corpsePrefab,transform.position,Quaternion.Euler(90f,0f,0f));
    //    newCorpse.transform.SetParent(corpseContainer);

        SoundAssets.instance.playWarriorDeathSound(transform.position,0.8f);
        if (gameObject.CompareTag("RedSoldier"))
        {
            WarriorFactory.instance.redWarriorDied(this.gameObject);

            CorpseFactory.instance.GetCorpse(transform.position, CorpseSprites.redWarrior);
        }
        else
        {
            WarriorFactory.instance.blueWarriorDied(this.gameObject);

            CorpseFactory.instance.GetCorpse(transform.position, CorpseSprites.blueWarrior);
        }


        transform.parent.GetComponentInChildren<RangeSensor>().ClearEntities();
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("RedAttack") && gameObject.CompareTag("BlueSoldier"))
        {
            Die();
        }
        if (col.CompareTag("BlueAttack") && gameObject.CompareTag("RedSoldier"))
        {
            Die();
        }
    }

}
