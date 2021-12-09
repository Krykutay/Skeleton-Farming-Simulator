using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    [SerializeField] int _maxHealth = 50;
    [SerializeField] float _damageHopSpeed = 6f;

    [SerializeField] float _groundCheckRadius = 0.3f;
    [SerializeField] float _wallCheckDistance = 0.2f;
    [SerializeField] float _ledgeCheckDistance = 0.5f;
    [SerializeField] float _ledgeBehindCheckDistance = 3f;

    [SerializeField] float _minAgroDistance = 8f;
    [SerializeField] float _maxAgroDistance = 10f;

    [SerializeField] float _stunResistance = 3f;
    [SerializeField] float _stunRecoveryTime = 3f;

    [SerializeField] float _meleeRangeActionDistance = 1f;

    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _player;

    public int maxHealth { get { return _maxHealth; } }
    public float damageHopSpeed { get { return _damageHopSpeed; } }

    public float groundCheckRadius { get { return _groundCheckRadius; } }
    public float wallCheckDistance { get { return _wallCheckDistance; } }
    public float ledgeCheckDistance { get { return _ledgeCheckDistance; } }
    public float ledgeBehindCheckDistance { get { return _ledgeBehindCheckDistance; } }

    public float minAgroDistance { get { return _minAgroDistance; } }
    public float maxAgroDistance { get { return _maxAgroDistance; } }

    public float stunResistance { get { return _stunResistance; } }
    public float stunRecoveryTime { get { return _stunRecoveryTime; } }

    public float meleeRangeActionDistance { get { return _meleeRangeActionDistance; } }

    public LayerMask ground { get { return _ground; } }
    public LayerMask player { get { return _player; } }
}
