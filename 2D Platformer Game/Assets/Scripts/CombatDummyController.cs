using System;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    public static Action<CombatDummyController> Died;

    [SerializeField] float _maxHealth;
    [SerializeField] bool _applyKnockback;
    [SerializeField] float _knockbackSpeedX;
    [SerializeField] float _knockbackSpeedY;
    [SerializeField] float _knockbackDuration;
    [SerializeField] float _knockbackDeathSpeedX;
    [SerializeField] float _knockbackDeathSpeedY;
    [SerializeField] float _deathTorque;

    Animator _aliveAnim;
    PlayerController _pc;
    GameObject _aliveGo, _brokenTopGo, _brokenBotGo;
    Rigidbody2D _rbAlive, _rbBrokenTop, _rbBrokenBot;
    Vector3 _initialPosition, _initialRotation;

    public Vector3 initialPosition { get { return _initialPosition; } }
    public Vector3 initialRotation { get { return _initialRotation; } }

    float _currentHealth;
    int _playerFacingDirection;
    float _knockbackStart;

    bool _playerOnLeft;
    bool _knockback;

    void Awake()
    {
        _pc = GameObject.Find("Player").GetComponent<PlayerController>();

        _aliveGo = transform.Find("Alive").gameObject;
        _brokenTopGo = transform.Find("Broken Top").gameObject;
        _brokenBotGo = transform.Find("Broken Bottom").gameObject;

        _aliveAnim = _aliveGo.GetComponent<Animator>();
        _rbAlive = _aliveGo.GetComponent<Rigidbody2D>();
        _rbBrokenTop = _brokenTopGo.GetComponent<Rigidbody2D>();
        _rbBrokenBot = _brokenBotGo.GetComponent<Rigidbody2D>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation.eulerAngles;
    }

    void OnEnable()
    {
        _currentHealth = _maxHealth;
        
        _aliveGo.transform.localPosition = Vector3.zero;
        _brokenTopGo.transform.localPosition = Vector3.zero;
        _brokenBotGo.transform.localPosition = Vector3.zero; 
        _aliveGo.SetActive(true);
        _brokenTopGo.SetActive(false);
        _brokenBotGo.SetActive(false);
    }

    void Update()
    {
        CheckKnockback();
    }

    void Damage(float amount)
    {
        HitParticlePool.Instance.Get(_aliveGo.transform.position, new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360)));

        _currentHealth -= amount;
        _playerFacingDirection = _pc.GetFacingDirection();

        if (_playerFacingDirection == 1)
        {
            _playerOnLeft = true;
        }
        else
        {
            _playerOnLeft = false;
        }

        _aliveAnim.SetBool("playerOnLeft", _playerOnLeft);
        _aliveAnim.SetTrigger("damage");

        if (_applyKnockback && _currentHealth > 0)
        {
            Knockback();
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Knockback()
    {
        _knockback = true;
        _knockbackStart = Time.time;
        _rbAlive.velocity = new Vector2(_knockbackSpeedX * _playerFacingDirection, _knockbackSpeedY);
    }

    void CheckKnockback()
    {
        if (Time.time >= _knockbackStart + _knockbackDuration && _knockback)
        {
            _knockback = false;
            _rbAlive.velocity = new Vector2(0f, _knockbackSpeedY);
        }
    }

    void Die()
    {
        Died?.Invoke(this);

        _aliveGo.SetActive(false);
        _brokenTopGo.SetActive(true);
        _brokenBotGo.SetActive(true);

        _brokenTopGo.transform.position = _aliveGo.transform.position;
        _brokenBotGo.transform.position = _aliveGo.transform.position;

        _rbBrokenTop.velocity = new Vector2(_knockbackDeathSpeedX * _playerFacingDirection, _knockbackDeathSpeedY);
        _rbBrokenBot.velocity = new Vector2(_knockbackSpeedX * _playerFacingDirection, _knockbackSpeedY);

        _rbBrokenTop.AddTorque(_deathTorque * -_playerFacingDirection, ForceMode2D.Impulse);
    }


}
