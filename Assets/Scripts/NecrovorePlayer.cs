using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovorePlayer : MonoBehaviour
{
    private CustomCamera _camera;

    [SerializeField]
    private float _maxHunger;
    private float _baseMaxHunger;
    private float _hunger;
    [SerializeField]
    private float _hungerDrainRate;
    [SerializeField]
    private float _eatingDamage;
    [SerializeField]
    private float _eatingSlowness;

    private Corpse _eatenCorpse = null;
    private float _eatenHunger = 0f;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _dashSpeedBonus;
    [SerializeField]
    private float _dashDuration;
    private float _currentDashTime = 0f;
    private bool _dashing = false;
    [SerializeField]
    private float _dashHungerDrain;
    [SerializeField]
    private float _cameraShakeDashMagnitude;
    [SerializeField]
    private float _cameraShakeDashDuration;

    [SerializeField]
    private float _cameraShakeEatingInterval;
    [SerializeField]
    private float _cameraShakeEatingMagnitude;
    [SerializeField]
    private float _cameraShakeEatingDuration;
    private float _currentCamShakeEatingTime = 0f;

    [SerializeField]
    private float _hungerLostOnHit;

    private Vector3 _velocity;

    private List<Corpse> _corpses;

    public bool Dashing { get => _dashing; }
    public bool Eating { get => _eatenCorpse != null; }
    public bool Walking { get => !Dashing && !Eating && _velocity != Vector3.zero; }
    public bool EatWalking { get => !Dashing && _velocity != Vector3.zero; }

    private void Start()
    {
        _camera = Camera.main.GetComponentInParent<CustomCamera>();

        _corpses = new List<Corpse>();

        _hunger = _maxHunger;
        _baseMaxHunger = _maxHunger;
    }

    void Update()
    {
        this.transform.position += _velocity * Time.deltaTime;
        //this.transform.Translate(_velocity * Time.deltaTime);

        _camera.SetTargetPosition(this.transform.position + _velocity.normalized * 4f + new Vector3(0, 10, 0));

        _currentDashTime += Time.deltaTime;
        if (_dashing && _currentDashTime >= _dashDuration)
        {
            _dashing = false;
            if (_eatenCorpse != null && !Input.GetButton("Eat")) StopEating();
        }

        float dmg = 0f;
        if (_eatenCorpse != null)
        {
            dmg = _eatenCorpse.TakeDamage(_eatingDamage * Time.deltaTime);
            _eatenHunger += dmg - _hungerDrainRate * Time.deltaTime;

            if (_currentCamShakeEatingTime <= 0f)
            {
                StartCoroutine(_camera.Shake(_cameraShakeEatingDuration, _cameraShakeEatingMagnitude));
                _currentCamShakeEatingTime = _cameraShakeEatingInterval;
                _hunger += _eatenHunger;
                _eatenHunger = 0f;
                GameManager.Instance.SliderShake(_cameraShakeEatingDuration, _cameraShakeEatingMagnitude * 4f);
                GameManager.Instance.TryVibration(_cameraShakeEatingDuration * 1.2f, 0.7f);
                GameManager.Instance.SplashBloodOnScreen();

                Vector3 bloodDirection = (_eatenCorpse.transform.position - this.transform.position);
                bloodDirection.y = 0;
                BloodParticleSystemHandler.Instance.SpawnBlood(this.transform.position, bloodDirection.normalized);
                SoundAssets.instance.playMunchSound(transform.position);
            }
            _currentCamShakeEatingTime -= Time.deltaTime;
        }
        else
        {
            _hunger -= _hungerDrainRate * Time.deltaTime;
        }
        float fullBelly = _hunger - _maxHunger;
        _hunger = Mathf.Min(_maxHunger, _hunger);

        GameManager.Instance.SetSlider(_hunger / _maxHunger);

        if (_hunger <= 0)
        {
            GameManager.Instance.StartCoroutine(_camera.Shake(0.3f, 0.4f));
            GameManager.Instance.PrintScores();
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
        else
        {
            GameManager.Instance.UpdateScore(Time.deltaTime, dmg, fullBelly);

            GameManager.Instance.pulsing = _hunger <= _baseMaxHunger * 0.25f;
        }

        if (Walking)
        {
            SoundAssets.instance.PlayFootstep(false);
        }
        else if (EatWalking)
        {
            SoundAssets.instance.PlayFootstep(true);
        }
    }

    public void Dash()
    {
        if (_dashing || _eatenCorpse != null || _velocity == Vector3.zero) return;

        _dashing = true;
        _currentDashTime = 0f;

        _velocity *= _dashSpeedBonus;

        _hunger -= _dashHungerDrain;

        SoundManager.PlaySound(SoundManager.Sound.Dash);

        StartCoroutine(_camera.Shake(_cameraShakeDashDuration, _cameraShakeDashMagnitude));
    }

    public void SetVelocity(float x, float y)
    {
        if (_dashing) return;

        _velocity = new Vector3(x, 0, y).normalized * _speed;

        if (_eatenCorpse != null)
        {
            _velocity *= _eatingSlowness;

            if (_velocity.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.up, -_velocity);
            }
        }
        else if (_velocity.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up, _velocity);
        }
    }

    public void StartEating()
    {
        if (_corpses.Count <= 0 || _eatenCorpse != null) return;

        _eatenCorpse = _corpses[0];
        _eatenCorpse.StartJoint(GetComponent<Rigidbody>());

        _currentCamShakeEatingTime = 0f;

        _eatenCorpse.OnDeath += FinishEating;

        _camera.ZoomIn();
    }

    public void StartEating(Corpse corpse)
    {
        if (_corpses.Count <= 0 || _eatenCorpse != null) return;

        _eatenCorpse = corpse;
        _eatenCorpse.StartJoint(GetComponent<Rigidbody>());

        _currentCamShakeEatingTime = 0f;

        _eatenCorpse.OnDeath += FinishEating;

        _camera.ZoomIn();
    }

    public void FinishEating()
    {
        if (_eatenCorpse == null) return;
        _corpses.Remove(_eatenCorpse);
        StopEating();
    }

    public IEnumerator BurpAfterSeconds()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
        if (Random.Range(0,3) == 0) SoundManager.PlaySound(SoundManager.Sound.Blurp, transform.position);
    }

    public void StopEating()
    {
        if (_eatenCorpse == null) return;

        _eatenCorpse.OnDeath -= FinishEating;
        _eatenCorpse.StopJoint();
        _eatenCorpse = null;

        _hunger += _eatenHunger;
        _eatenHunger = 0;
        GameManager.Instance.SetSlider(_hunger / _maxHunger);

        _camera.ZoomOut();
        
        StartCoroutine(BurpAfterSeconds());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Corpse"))
        {
            Corpse corpse = other.GetComponent<Corpse>();
            _corpses.Add(corpse);
            if (_dashing && _eatenCorpse == null)
            {
                StartEating(corpse);
            }
        }
        else if (other.CompareTag("BlueAttack") || other.CompareTag("RedAttack"))
        {
            GetHit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Corpse"))
        {
            _corpses.Remove(other.GetComponent<Corpse>());
        }
    }

    public void GetHit()
    {
        _maxHunger -= _hungerLostOnHit;
        _hunger = Mathf.Min(_maxHunger, _hunger);
        GameManager.Instance.ShortenSlider(_maxHunger / _baseMaxHunger, _hunger / _maxHunger);
        print("oof");
        SoundManager.PlaySound(SoundManager.Sound.Damage_necrovore);
        GameManager.Instance.HitScreen();
    }
}
