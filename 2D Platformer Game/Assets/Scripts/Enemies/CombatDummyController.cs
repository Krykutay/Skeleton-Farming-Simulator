using System;
using System.Collections;
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
    GameObject _aliveGo, _brokenTopGo, _brokenBotGo;
    Rigidbody2D _rbAlive, _rbBrokenTop, _rbBrokenBot;
    Vector3 _initialPosition;
    Quaternion _initialRotation;

    public Vector3 initialPosition { get { return _initialPosition; } }
    public Quaternion initialRotation { get { return _initialRotation; } }

    float _currentHealth;
    int _playerFacingDirection;

    bool _playerOnLeft;

    void Awake()
    {
        _aliveGo = transform.Find("Alive").gameObject;
        _brokenTopGo = transform.Find("Broken Top").gameObject;
        _brokenBotGo = transform.Find("Broken Bottom").gameObject;

        _aliveAnim = _aliveGo.GetComponent<Animator>();
        _rbAlive = _aliveGo.GetComponent<Rigidbody2D>();
        _rbBrokenTop = _brokenTopGo.GetComponent<Rigidbody2D>();
        _rbBrokenBot = _brokenBotGo.GetComponent<Rigidbody2D>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
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

    void Damage(float[] attackDetails)
    {
        HitParticlePool.Instance.Get(_aliveGo.transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));

        _currentHealth -= attackDetails[0];

        if (attackDetails[1] < _aliveGo.transform.position.x)
        {
            _playerOnLeft = true;
            _playerFacingDirection = 1;
        }
        else
        {
            _playerOnLeft = false;
            _playerFacingDirection = -1;
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
        _rbAlive.velocity = new Vector2(_knockbackSpeedX * _playerFacingDirection, _knockbackSpeedY);
        StartCoroutine(EndKnockback(_knockbackDuration));
    }

    IEnumerator EndKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        _rbAlive.velocity = new Vector2(0f, _knockbackSpeedY);
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
