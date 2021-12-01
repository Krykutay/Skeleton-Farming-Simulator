using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;
    public DeadState deadState;
    public RespawnState respawnState;
    public TeleportState teleportState;

    void StartAttack()
    {
        attackState.StartAttack();
    }

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

    void Teleported()
    {
        teleportState.TeleportStarted();
    }

    void TeleportEnded()
    {
        teleportState.TeleportEnded();
    }
}
