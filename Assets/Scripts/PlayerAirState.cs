using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    override public void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space) && player.currentJumps > 0)
        {
            player.currentJumps--;
            player.stateMachine.ChangeState(player.jumpState);
        }

        if(player.isWallDetected())
            player.stateMachine.ChangeState(player.wallSlideState);

        if(player.isGroundDetected())
        {
            player.ResetDash();
            player.stateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0)
        {
            float airControlFactor = 0.7f; // Adjust this value to control air movement responsiveness
            player.SetVelocity(player.moveSpeed * airControlFactor * xInput, rb.velocity.y);
        }
    }
}

