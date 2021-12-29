using System.Collections;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] _bodySr;
    [SerializeField] float _activeTime = 0.25f;
    [SerializeField] float _alphaSet = 0.9f;
    [SerializeField] float _alphaDecay = 6f;

    SpriteRenderer[] _playerBodySr;

    Color _color;

    float _alpha;
    float _timeActivated;

    void Awake()
    {
        Transform playerBodyParts = Player.Instance.transform.Find("BodyParts");
        Transform playerBody = playerBodyParts.Find("Body");


        _playerBodySr = new SpriteRenderer[_bodySr.Length];
        _playerBodySr[0] = playerBody.Find("Chest").GetComponent<SpriteRenderer>();
        _playerBodySr[1] = playerBody.Find("Head").GetComponent<SpriteRenderer>();
        _playerBodySr[2] = playerBody.Find("RightArm").GetComponent<SpriteRenderer>();
        _playerBodySr[3] = playerBody.Find("LeftArm").GetComponent<SpriteRenderer>();
        _playerBodySr[4] = playerBodyParts.Find("LeftLeg").GetComponent<SpriteRenderer>();
        _playerBodySr[5] = playerBodyParts.Find("RightLeg").GetComponent<SpriteRenderer>();
        _playerBodySr[6] = playerBody.Find("RightArm").Find("player_sword").GetComponent<SpriteRenderer>();
        _playerBodySr[7] = playerBody.Find("LeftArm").Find("player_sword").GetComponent<SpriteRenderer>();

        for (int i = 0; i < _bodySr.Length; i++)
        {
            _bodySr[i].sprite = _playerBodySr[i].sprite;
        }
    }

    void OnEnable()
    {
        _alpha = _alphaSet;
        _bodySr[0].sprite = _playerBodySr[0].sprite;
        _bodySr[1].sprite = _playerBodySr[1].sprite;
        _bodySr[6].sprite = _playerBodySr[6].sprite;
        _bodySr[7].sprite = _playerBodySr[7].sprite;

        transform.position = Player.Instance.transform.position;
        transform.rotation = Player.Instance.transform.rotation;

        _timeActivated = Time.unscaledTime;
    }

    void Update()
    {
        _alpha -= _alphaDecay * Time.unscaledDeltaTime;
        _color = new Color(1f, 1f, 1f, _alpha);
        foreach (SpriteRenderer sr in _bodySr)
        {
            sr.color = _color;
        }

        if (Time.unscaledTime >= (_timeActivated + _activeTime))
        {
            PlayerAfterImagePool.Instance.ReturnToPool(this);
        }
    }
}
