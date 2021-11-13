using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrovorePlayer : MonoBehaviour
{
    private CustomCamera _camera;

    [SerializeField]
    private float _maxHunger;
    private float _hunger;
    [SerializeField]
    private float _hungerDrainRate;
    [SerializeField]
    private float _eatingDamage;
    [SerializeField]
    private float _eatingSlowness;

    private Corpse _eatenCorpse = null;

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
    private float _cameraShakeEatingInterval;
    [SerializeField]
    private float _cameraShakeEatingMagnitude;
    [SerializeField]
    private float _cameraShakeEatingDuration;
    private float _currentCamShakeEatingTime = 0f;

    private Vector3 _velocity;

    private List<Corpse> _corpses;

    private void Start()
    {
        _camera = Camera.main.GetComponentInParent<CustomCamera>();

        _corpses = new List<Corpse>();

        _hunger = _maxHunger;
    }

    void Update()
    {
        this.transform.position += _velocity * Time.deltaTime;

        _camera.SetTargetPosition(this.transform.position + _velocity.normalized * 2f + new Vector3(0, 10, 0));

        _currentDashTime += Time.deltaTime;
        if (_dashing && _currentDashTime >= _dashDuration)
        {
            _dashing = false;
        }

        _hunger -= _hungerDrainRate * Time.deltaTime;
        float dmg = 0f;
        if (_eatenCorpse != null)
        {
            dmg = _eatenCorpse.TakeDamage(_eatingDamage * Time.deltaTime);
            _hunger += dmg;

            if (_currentCamShakeEatingTime <= 0f)
            {
                StartCoroutine(_camera.Shake(_cameraShakeEatingDuration, _cameraShakeEatingMagnitude));
                _currentCamShakeEatingTime = _cameraShakeEatingInterval;
            }
            _currentCamShakeEatingTime -= Time.deltaTime;
        }
        float fullBelly = _hunger - _maxHunger;
        _hunger = Mathf.Min(_maxHunger, _hunger);

        GameManager.Instance.SetSlider(_hunger / _maxHunger);

        if (_hunger <= 0)
        {
            GameManager.Instance.StartCoroutine(_camera.Shake(0.3f, 0.4f));
            GameManager.Instance.PrintScores();
            this.gameObject.SetActive(false);
        }
        else GameManager.Instance.UpdateScore(Time.deltaTime, dmg, fullBelly);
    }

    public void Dash()
    {
        if (_dashing || _eatenCorpse != null) return;

        _dashing = true;
        _currentDashTime = 0f;

        _velocity *= _dashSpeedBonus;

        _hunger -= _dashHungerDrain;
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

    public void FinishEating()
    {
        if (_eatenCorpse == null) return;

        _corpses.Remove(_eatenCorpse);
        StopEating();
    }

    public void StopEating()
    {
        if (_eatenCorpse == null) return;

        _eatenCorpse.OnDeath -= FinishEating;
        _eatenCorpse.StopJoint();
        _eatenCorpse = null;

        _camera.ZoomOut();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Corpse"))
        {
            _corpses.Add(other.GetComponent<Corpse>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Corpse"))
        {
            _corpses.Remove(other.GetComponent<Corpse>());
        }
    }
}
