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
        Corpse newCorpse = Instantiate(corpsePrefab,transform.position,Quaternion.Euler(90f,0f,0f));
        newCorpse.transform.SetParent(corpseContainer);

        if (gameObject.CompareTag("RedSoldier"))
        {
            SoundManager.PlaySound(SoundManager.Sound.Death_soldier1,transform.position);
            WarriorFactory.instance.redWarriorDied(this.gameObject);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Death_soldier2,transform.position);
            WarriorFactory.instance.blueWarriorDied(this.gameObject);
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
