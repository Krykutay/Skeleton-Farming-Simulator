using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;

    void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    void FinishedAttack()
    {
        attackState.FinishAttack();
    }
}
