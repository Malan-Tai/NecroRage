using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private float _timeScore;
    private float _eatenScore;
    private float _fullBellyScore;
    [SerializeField]
    private float _timeBonus;
    [SerializeField]
    private float _eatenBonus;
    [SerializeField]
    private float _fullBellyBonus;

    public void UpdateScore(float deltaTime, float eatenDamage, float bonusDamage)
    {
        _timeScore += deltaTime * _timeBonus;
        _eatenScore += eatenDamage * _eatenBonus;
        if (bonusDamage > 0) _fullBellyScore += eatenDamage * _fullBellyBonus;
    }

    public void PrintScores()
    {
        print("SCORES : time=" + _timeScore + " | eaten=" + _eatenScore + " | full belly=" + _fullBellyScore);
    }
}
