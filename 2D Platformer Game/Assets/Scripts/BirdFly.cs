using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    float _heightVariance;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _turnTime = 3f;

    float _initialTurnTime;

    void Start()
    {
        _heightVariance = Random.Range(0.6f, 1.4f);
        _initialTurnTime = _turnTime;
    }

    void Update()
    {
        _turnTime -= Time.deltaTime;
        if (_turnTime < 0)
        {
            _turnTime = _initialTurnTime;
            _moveSpeed = -_moveSpeed;
            transform.Rotate(0f, 180f, 0f);
        }

        transform.position += new Vector3(_moveSpeed * Time.deltaTime, Mathf.Sin(Time.time) * Time.deltaTime * _heightVariance);
            
    }
}
