using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;
    public DeadState deadState;
    public RespawnState respawnState;

    void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    void FinishedAttack()
    {
        attackState.FinishAttack();
    }

    void Dead()
    {
        deadState.Dead();
    }

    void Respawned()
    {
        respawnState.Respawned();
    }
}
