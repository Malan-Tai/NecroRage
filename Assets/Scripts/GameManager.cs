using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

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
    [SerializeField]
    private Image _hungerSliderBackground;

    [SerializeField]
    private Transform _bloodScreensParent;
    private Image[] _bloodScreens;
    [SerializeField]
    private float _bloodScreenFadeSpeed = 1f;

    [SerializeField]
    private Transform _redVisionParent;
    private Image[] _redVision;
    [SerializeField]
    private float _redVisionPulseTime = 2f;
    private float _currentPulse = 0f;

    [SerializeField]
    private Image _redHitScreen;
    private Color _hitScreenBaseColor;
    [SerializeField]
    private float _hitScreenFadeTime = 0.5f;

    public bool pulsing = false;
    private Coroutine _pulsingSliderCoroutine = null;

    private void Start()
    {
        _hungerSliderBaseWidth = _hungerSlider.GetComponent<RectTransform>().rect.width;
        _bloodScreens = _bloodScreensParent.GetComponentsInChildren<Image>();
        _redVision = _redVisionParent.GetComponentsInChildren<Image>();

        _hitScreenBaseColor = _redHitScreen.color;

        Color color;
        foreach (Image image in _redVision)
        {
            color = image.color;
            color.a = 0f;
            image.color = color;
        }

        color = _redHitScreen.color;
        color.a = 0f;
        _redHitScreen.color = color;

        SoundAssets.instance.PlayMusicWithFade(SoundAssets.instance.mainMusic,0.2f);
    }

    private void Update()
    {
        Color color;

        foreach (Image image in _bloodScreens)
        {
            color = image.color;
            color.a -= _bloodScreenFadeSpeed * Time.deltaTime;
            if (color.a < 0f) color.a = 0f;
            image.color = color;
        }

        foreach (Image image in _redVision)
        {
            color = image.color;
            color.a -= (1 / (_redVisionPulseTime * 0.6f)) * Time.deltaTime;
            if (color.a < 0f) color.a = 0f;
            image.color = color;
        }

        color = _redHitScreen.color;
        color.a -= (1 / (_hitScreenFadeTime)) * Time.deltaTime;
        if (color.a < 0f) color.a = 0f;
        _redHitScreen.color = color;

        if (pulsing)
        {
            if (_pulsingSliderCoroutine == null) _pulsingSliderCoroutine = StartCoroutine(SliderShakeCoroutine(60f, 5f));

            _currentPulse += Time.deltaTime;
            if (_currentPulse >= _redVisionPulseTime * 0.6f)
            {
                Color c = _redVision[1].color;
                c.a = 1;
                _redVision[1].color = c;
            }
            if (_currentPulse >= _redVisionPulseTime * 0.8f)
            {
                Color c = _redVision[1].color;
                c.a = 1;
                _redVision[0].color = c;
            }
            if (_currentPulse >= _redVisionPulseTime)
            {
                _currentPulse = 0f;
            }
        }
        else if (_pulsingSliderCoroutine != null)
        {
            StopCoroutine(_pulsingSliderCoroutine);
            _pulsingSliderCoroutine = null;
        }
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

            _hungerSlider.transform.parent.transform.eulerAngles = new Vector3(0, 0, theta);

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
        rect.anchoredPosition3D = new Vector3((rect.sizeDelta.x - _hungerSliderBaseWidth) / 2f, 0, 0);

        _hungerSliderBackground.rectTransform.sizeDelta = rect.sizeDelta;

        _hungerSlider.SetValueWithoutNotify(currentRatio);
    }

    public void GameOver()
    {
        SoundAssets.instance.StopMusicWithFade(0.5f);
        _hungerSlider.transform.parent.gameObject.SetActive(false);
        GetComponent<Defeat>().GameOver((int)(_timeScore + _eatenScore + _fullBellyScore));
    }

    public void TryVibration(float duration, float vibration)
    {
        PlayerIndex testPlayerIndex = 0;
        GamePadState testState = GamePad.GetState(testPlayerIndex);
        if (!testState.IsConnected) return;

        GamePad.SetVibration(testPlayerIndex, vibration, vibration);
        StartCoroutine(Vibrate(duration));
    }

    private IEnumerator Vibrate(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }

        GamePad.SetVibration(0, 0, 0);
    }

    public void SplashBloodOnScreen()
    {
        int n = _bloodScreens.Length;
        int i = Random.Range(0, n);

        while (_bloodScreens[i].color.a > 0f)
        {
            i++;
            if (i >= n - 1) i = 0;
            if (i < 0) i = n - 1;
        }

        _bloodScreens[i].color = Color.white;

        int scaleX = Random.Range(-2, 2);
        if (scaleX >= 0) scaleX = 1;
        else scaleX = -1;
        int scaleY = Random.Range(-2, 2);
        if (scaleY >= 0) scaleY = 1;
        else scaleY = -1;

        _bloodScreens[i].transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    public void HitScreen()
    {
        _redHitScreen.color = _hitScreenBaseColor;
    }
}
