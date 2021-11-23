using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator baseAnim;
    protected Animator weaponAnim;

    protected PlayerAttackState state;

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

        baseAnim.SetBool("attack", true);
        weaponAnim.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        baseAnim.SetBool("attack", false);
        weaponAnim.SetBool("attack", false);

        gameObject.SetActive(false);
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

}
