using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    private Sprite _landedSprite;
    [SerializeField]
    private Sprite _notLandedSprite;

    [SerializeField]
    private float _baseScaleValue = 1f;
    private float _shadowScale; // = 0f;
    private float _bodyScale; // = 10f;

    [SerializeField]
    private float _fallTime = 5;
    private float _currentFallTime;

    private float _shadowScaleSpeed;
    private float _bodyScaleSpeed;

    [SerializeField]
    private float _rotateSpeed = 10f;
    private float _currentAngle;

    //private bool _startFalling = false;
    private bool _landed;

    private Transform _shadow;
    private Transform _body;
    private Collider _bodyCollider;
    private BoulderCrusher _crusher;
    private SpriteRenderer _bodySprite;

    private int _activeCrusherFrames;
    private bool _listeningToCrush;

    public delegate void EventLand();
    public static event EventLand OnLand;

    private void Start()
    {
        _shadow = transform.Find("Shadow");
        _body = transform.Find("Body");
        _bodyCollider = _body.GetComponent<Collider>();
        _crusher = _body.GetComponent<BoulderCrusher>();
        _bodySprite = _body.GetComponent<SpriteRenderer>();

        Init();
    }

    public void Init()
    {
        _landed = false;
        _currentAngle = Random.Range(-180f, 179f);
        _baseScaleValue += Random.Range(-1f, 1f) * _baseScaleValue * 0.2f;
        float relativeFallTimeChange = Random.Range(-1f, 1f) * _fallTime * 0.1f;
        _fallTime += relativeFallTimeChange * _fallTime;
        _rotateSpeed += relativeFallTimeChange * _rotateSpeed;
        _shadowScale = 0f;
        _bodyScale = 3f;
        _activeCrusherFrames = 0;
        _listeningToCrush = true;

        _shadow.gameObject.SetActive(true);
        _shadow.transform.localScale = Vector3.one * _shadowScale;
        _body.transform.localScale = Vector3.one * _bodyScale;
        _crusher.OnCrush += HandleCrush;
        _bodyCollider.isTrigger = true;
        _bodyCollider.enabled = false;
        _bodySprite.sprite = _notLandedSprite;
        _body.gameObject.SetActive(false);

        _shadowScaleSpeed = (_baseScaleValue - _shadowScale) / _fallTime;
        _bodyScaleSpeed = 2f * (_baseScaleValue - _bodyScale) / _fallTime;
    }

    void Update()
    {
        if (!_landed)
        {
            _currentAngle += _rotateSpeed * Time.deltaTime;
            this.transform.rotation = Quaternion.Euler(90f, _currentAngle, 0f);

            _shadowScale += _shadowScaleSpeed * Time.deltaTime;
            //if (_shadowScale > _baseScaleValue) _shadowScale = _baseScaleValue;
            _shadow.localScale = Vector3.one * _shadowScale;

            _currentFallTime += Time.deltaTime;
            if (_currentFallTime >= _fallTime / 2f)
            {
                if (!_body.gameObject.activeInHierarchy) _body.gameObject.SetActive(true);

                _bodyScale += _bodyScaleSpeed * Time.deltaTime;
                _body.localScale = Vector3.one * _bodyScale;
            }

            if (_bodyScale <= _baseScaleValue)
            {
                _landed = true;
                _bodyCollider.enabled = true;
                _bodySprite.sprite = _landedSprite;
                _bodySprite.sortingOrder--;
                _shadow.gameObject.SetActive(false);
                SoundManager.PlaySound(SoundManager.Sound.Rock_explode1);

                if (OnLand != null) OnLand();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_landed && _listeningToCrush)
        {
            _activeCrusherFrames++;

            if (_activeCrusherFrames >= 2)
            {
                _listeningToCrush = false;
                _crusher.OnCrush -= HandleCrush;
                _bodyCollider.isTrigger = false;
            }
        }
    }

    private void HandleCrush(Collider other)
    {
        if (!_listeningToCrush) return;

        if (other.CompareTag("BlueSoldier") || other.CompareTag("RedSoldier"))
        {
            WarriorDeath warrior = other.GetComponentInChildren<WarriorDeath>();
            warrior.Die();
        }
        else if (other.CompareTag("Necrophage"))
        {
            NecrovoreDeath necrovore = other.GetComponentInChildren<NecrovoreDeath>();
            if (necrovore == null) necrovore = other.GetComponent<NecrovoreDeath>();
            if (necrovore != null)
            {
                necrovore.Die();
            }
            else
            {
                NecrovorePlayer player = other.GetComponent<NecrovorePlayer>();
                print("player");
                player.GetHit();
            }
        }
    }
}
