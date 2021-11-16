using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] bool _combatEnabled;
    [SerializeField] float _inputTimer;
    [SerializeField] float _attack1Radius;
    [SerializeField] float _attack1Damage;

    [SerializeField] Transform _attack1HitBoxPos;

    [SerializeField] LayerMask _isDamageable;

    float _lastInputTime = Mathf.NegativeInfinity;

    bool _gotInput;
    bool _isAttacking;
    bool _isFirstAttack;

    Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _anim.SetBool("canAttack", _combatEnabled);
    }

    void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_combatEnabled)
            {
                // attempt combat
                _gotInput = true;
                _lastInputTime = Time.time;
            }
        }
    }

    void CheckAttacks()
    {
        if (_gotInput)
        {
            //perform attack 1
            if (!_isAttacking)
            {
                _gotInput = false;
                _isAttacking = true;
                _isFirstAttack = !_isFirstAttack;
                _anim.SetBool("attack1", true);
                _anim.SetBool("firstAttack", _isFirstAttack);
                _anim.SetBool("isAttacking", _isAttacking);
            }
        }

        if (Time.time >= _lastInputTime + _inputTimer)
        {
            // wait for new input
            _gotInput = false;
        }
    }

    void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_attack1HitBoxPos.position, _attack1Radius, _isDamageable);

        foreach (Collider2D collider in detectedObjects)
        {
            // may as well try events
            collider.transform.parent.SendMessage("Damage", _attack1Damage);
            // instantiate hit particle

        }
    }

    void FinishAttack1()
    {
        _isAttacking = false;
        _anim.SetBool("isAttacking", _isAttacking);
        _anim.SetBool("attack1", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attack1HitBoxPos.position, _attack1Radius);
    }

}
