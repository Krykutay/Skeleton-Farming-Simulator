using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveStateData", menuName = "Data/State Data/Move State")]
public class D_MoveState : ScriptableObject
{
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] float _moveStateDelay = 0.1f;

    public float movementSpeed { get { return _moveSpeed; } }
    public float moveStateDelay { get { return _moveStateDelay; } }
}
