using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _respawnPoint;

    [SerializeField] GameObject _player;

    [SerializeField] float _respawnTime;

    float _respawnTimeStart;

    bool _respawn;

    CinemachineVirtualCamera _cvc;

    void Awake()
    {
        _cvc = transform.Find("Cameras").Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    void OnEnable()
    {
        Player.PlayerDied += Player_PlayerDied;
    }

    void OnDisable()
    {
        Player.PlayerDied += Player_PlayerDied;
    }

    void Update()
    {
        CheckRespawn();
    }

    void Player_PlayerDied()
    {
        Respawn();
    }

    public void Respawn()
    {
        _respawnTimeStart = Time.time;
        _respawn = true;
    }

    void CheckRespawn()
    {
        if (_respawn && Time.time >= _respawnTimeStart + _respawnTime)
        {
            var tempPlayer = Instantiate(_player, _respawnPoint.position, Quaternion.Euler(0f, 0f ,0f));
            _cvc.m_Follow = tempPlayer.transform;
            _respawn = false;
        }
    }
}