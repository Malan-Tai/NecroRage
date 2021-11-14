using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorpseSprites
{
    blueWarrior,
    redWarrior,
    necrovore
}

public class Corpse : MonoBehaviour
{
    public static int corpseCounter { get => CorpseFactory.instance.corpseCounter; }

    [SerializeField]
    private float _maxHP;
    private float _hp;

    public delegate void EventDie();
    public event EventDie OnDeath;

    private Joint _joint;
    private SpriteRenderer _renderer;

    [SerializeField]
    private Sprite[] _blueWarriorSprites;
    [SerializeField]
    private Sprite[] _redWarriorSprites;
    [SerializeField]
    private Sprite[] _necrovoreSprites;

    private Sprite[] _usedSprites;

    private void Start()
    {
        _joint = GetComponent<Joint>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _hp = _maxHP;

        if (_usedSprites == null)
        {
            _usedSprites = _blueWarriorSprites;
        }
    }

    public void SetUsedSprites(CorpseSprites sprite)
    {
        _joint = GetComponent<Joint>();
        _renderer = GetComponentInChildren<SpriteRenderer>();

        switch (sprite)
        {
            case CorpseSprites.necrovore:
                _usedSprites = _necrovoreSprites;
                break;
            case CorpseSprites.blueWarrior:
                _usedSprites = _blueWarriorSprites;
                break;
            default:
                _usedSprites = _redWarriorSprites;
                break;
        }
        _renderer.sprite = _usedSprites[0];

        _hp = _maxHP;
    }

    public float TakeDamage(float damage)
    {
        float actuallyTaken = Mathf.Min(_hp, damage);
        _hp -= damage;
        if (_hp <= 0)
        {
            if (OnDeath != null) OnDeath();
            _usedSprites = null;
            CorpseFactory.instance.CorpseDied(gameObject);
        }
        else
        {
            int n = _usedSprites.Length;
            for (int i = 0; i < n; i++)
            {
                if ((n - 1 - i) * _maxHP / n < _hp && _hp <= (n - i) * _maxHP / n)
                {
                    _renderer.sprite = _usedSprites[i];
                    break;
                }
            }
        }

        return actuallyTaken;
    }

    public void StartJoint(Rigidbody body)
    {
        this.transform.position = new Vector3(body.transform.position.x, 0, body.transform.position.z);
        _joint.connectedBody = body;
    }

    public void StopJoint()
    {
        _joint.connectedBody = null;
    }

    
}
