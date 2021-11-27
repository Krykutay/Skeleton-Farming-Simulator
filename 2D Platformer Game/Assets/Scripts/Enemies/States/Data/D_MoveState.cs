using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMoveStateData", menuName = "Data/State Data/Move State")]
public class D_MoveState : ScriptableObject
{
    [SerializeField] float _moveSpeed = 3f;

    public float movementSpeed { get { return _moveSpeed; } }
}
