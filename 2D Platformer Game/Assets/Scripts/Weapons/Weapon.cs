using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnim;
    protected Animator weaponAnim;

    protected PlayerAttackState attackState;

    protected int attackCounter;

    protected virtual void Awake()
    {
        baseAnim = transform.Find("Base").GetComponent<Animator>();
        weaponAnim = transform.Find("Weapon").GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks)
            attackCounter = 0;

        baseAnim.SetBool("attack", true);
        weaponAnim.SetBool("attack", true);

        baseAnim.SetInteger("attackCounter", attackCounter);
        weaponAnim.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        baseAnim.SetBool("attack", false);
        weaponAnim.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.attackState = state;
    }
    public virtual void AnimationTurnOffFlipTrigger()
    {
        attackState.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        attackState.SetFlipCheck(true);
    }

    public virtual void AnimationStartMovementTrigger()
    {
        attackState.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        attackState.SetPlayerVelocity(0f);
    }

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
    }

    public virtual void AnimationActionTrigger()
    {
        
    }

}
