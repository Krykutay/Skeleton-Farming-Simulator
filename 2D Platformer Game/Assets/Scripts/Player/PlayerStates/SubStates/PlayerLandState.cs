using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SoundManager.Instance.Play(SoundManager.SoundTags.PlayerLand);
        CinemachineShake.Instance.ShakeCamera(1.75f, 0.1f);
        DustJumpParticlePool.Instance.Get(player._groundCheck.position, Quaternion.Euler(-90f, 0f, 0f));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;
        
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
        
    }
}
