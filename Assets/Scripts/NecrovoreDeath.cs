using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovoreDeath : MonoBehaviour
{
    [SerializeField] private float distanceToDie = 50f;
    void Update()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance._player.transform.position) >= distanceToDie)
        {
            NecrovoreFactory.instance.necrovoreDied(gameObject);
        }
    }
    public void Die()
    {
        SoundAssets.instance.playNecrovoreDeathSound(transform.position,0.5f);
        NecrovoreFactory.instance.necrovoreDied(this.gameObject);

        transform.parent.GetComponentInChildren<NecrovoreRangeSensor>().ClearEntities();

        CorpseFactory.instance.GetCorpse(transform.position, CorpseSprites.necrovore);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("RedAttack") || col.CompareTag("BlueAttack"))
        {
            Die();
        }
    }

}
