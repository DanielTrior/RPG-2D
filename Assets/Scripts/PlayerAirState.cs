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

        if(Input.GetKeyDown(KeyCode.Space) && player.currentJumps > 0) //If player presses space and has jumps left, change to jump state
        {
            player.currentJumps--;
            player.stateMachine.ChangeState(player.jumpState);
        }

        if(player.isWallDetected()) //If player is on the wall, change to wall slide state
            player.stateMachine.ChangeState(player.wallSlideState);

        if(player.isGroundDetected()) //If player is on the ground, change to idle state
        {
            player.ResetDash();
            player.stateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0) // If player is moving in the air, apply air control
        {
            float airControlFactor = 0.7f; // Adjust this value to control air movement responsiveness
            player.SetVelocity(player.moveSpeed * airControlFactor * xInput, rb.velocity.y);
        }
    }
}

