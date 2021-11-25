using System.Collections;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] float _activeTime = 0.25f;
    [SerializeField] float _alphaSet = 0.9f;
    [SerializeField] float _alphaDecay = 6f;

    Transform _player;
    SpriteRenderer _sr;
    SpriteRenderer _playerSr;

    Color _color;

    float _alpha;
    float _timeActivated;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _sr = GetComponent<SpriteRenderer>();
        _playerSr = _player.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerSr = _player.GetComponent<SpriteRenderer>();
        }

        _alpha = _alphaSet;
        _sr.sprite = _playerSr.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        _timeActivated = Time.unscaledTime;
    }

    void Update()
    {
        _alpha -= _alphaDecay * Time.unscaledDeltaTime;
        _color = new Color(1f, 1f, 1f, _alpha);
        _sr.color = _color;

        if (Time.unscaledTime >= (_timeActivated + _activeTime))
        {
            PlayerAfterImagePool.Instance.ReturnToPool(this);
        }
    }
}
