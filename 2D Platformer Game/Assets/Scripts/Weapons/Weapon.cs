using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] SO_WeaponData _weaponData;

    public SO_WeaponData weaponData { get => _weaponData; protected set => _weaponData = value; }

    protected Animator weaponAnim;

    protected PlayerAttackState attackState;

    protected int attackCounter;

    float _comboStartTime = Mathf.NegativeInfinity;

    protected virtual void Awake()
    {
        weaponAnim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= _weaponData.amountOfAttacks || Time.time >= _comboStartTime + weaponData.comboDuration)
            attackCounter = 0;

        _comboStartTime = Time.time;

        weaponAnim.SetBool("attack", true);
        weaponAnim.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        attackCounter++;

        weaponAnim.SetBool("attack", false);

        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.attackState = state;
    }

    public virtual void AnimationActionTrigger()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        attackState.AnimationFinishTrigger();
    }

    public virtual void AnimationCancelled()
    {
        attackState.AnimationFinishTrigger();
    }

}
