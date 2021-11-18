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
        PlayerStats.PlayerDied += PlayerStats_PlayerDied;
    }

    void OnDisable()
    {
        PlayerStats.PlayerDied += PlayerStats_PlayerDied;
    }

    void Update()
    {
        CheckRespawn();
    }

    void PlayerStats_PlayerDied()
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
            var tempPlayer = Instantiate(_player, _respawnPoint);
            _cvc.m_Follow = tempPlayer.transform;
            _respawn = false;
        }
    }
}