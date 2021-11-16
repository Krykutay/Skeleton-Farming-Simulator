using System.Collections;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField] float _activeTime = 0.2f;
    [SerializeField] float _alphaSet = 0.8f;

    Transform _player;
    SpriteRenderer _sr;
    SpriteRenderer _playerSr;

    Color _color;

    float _alpha;
    float _alphaMultiplier = 0.96f;

    ObjectPoolingManager _objectPoolingManagerInstance;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _sr = GetComponent<SpriteRenderer>();
        _playerSr = _player.GetComponent<SpriteRenderer>();
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
    }

    void OnEnable()
    {
        _alpha = _alphaSet;
        _sr.sprite = _playerSr.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        StartCoroutine(Despawn(_activeTime));
    }

    IEnumerator Despawn(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        _objectPoolingManagerInstance.ReturnToPool("afterImage", this.gameObject);
    }

    void Update()
    {
        _alpha *= _alphaMultiplier;
        _color = new Color(1f, 1f, 1f, _alpha);
        _sr.color = _color;
    }
}
