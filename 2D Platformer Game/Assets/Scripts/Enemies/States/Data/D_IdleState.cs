using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleStateData", menuName = "Data/State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    [SerializeField] float _minIdleTime = 1f;
    [SerializeField] float _maxIdleTime = 2f;

    public float minIdleTime { get { return _minIdleTime; } }
    public float maxIdleTime { get { return _maxIdleTime; } }
}
