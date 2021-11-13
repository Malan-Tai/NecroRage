using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    public NecrovorePlayer _player;

    [SerializeField]
    private Slider _hungerSlider;
    private float _hungerSliderBaseWidth;

    private void Start()
    {
        _hungerSliderBaseWidth = _hungerSlider.GetComponent<RectTransform>().rect.width;
    }

    public void UpdateScore(float deltaTime, float eatenDamage, float fullBelly)
    {
        _timeScore += deltaTime * _timeBonus;
        _eatenScore += eatenDamage * _eatenBonus;
        if (fullBelly > 0) _fullBellyScore += fullBelly * _fullBellyBonus;
    }

    public void PrintScores()
    {
        print("SCORES : time=" + _timeScore + " | eaten=" + _eatenScore + " | full belly=" + _fullBellyScore);
    }

    public void SetSlider(float hungerRatio)
    {
        _hungerSlider.SetValueWithoutNotify(hungerRatio);
    }

    private IEnumerator SliderShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float theta = Random.Range(-1f, 1f) * magnitude;

            _hungerSlider.transform.eulerAngles = new Vector3(0, 0, theta);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _hungerSlider.transform.eulerAngles = Vector3.zero;
    }

    public void SliderShake(float duration, float magnitude)
    {
        StartCoroutine(SliderShakeCoroutine(duration, magnitude));
    }

    public void ShortenSlider(float newMaxRatio, float currentRatio)
    {
        RectTransform rect = _hungerSlider.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(newMaxRatio * _hungerSliderBaseWidth, rect.sizeDelta.y);
        //rect.position = new Vector3((rect.sizeDelta.x - _hungerSliderBaseWidth) / 2f, 0, 0);
        rect.anchoredPosition3D = new Vector3((rect.sizeDelta.x - _hungerSliderBaseWidth) / 2f, 0, 0);
    }

    public void GameOver()
    {
        _hungerSlider.gameObject.SetActive(false);
        GetComponent<Defeat>().GameOver((int)(_timeScore + _eatenScore + _fullBellyScore));
    }
}
