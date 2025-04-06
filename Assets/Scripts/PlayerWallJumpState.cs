using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f; //Set timer for state duration
        player.SetVelocity(4 * -player.facingDir, player.jumpForce); //Set player velocity based on jump force and dir
        player.ResetDash(); //Reset dash count
    }

    public override void Exit()
    { 
        base.Exit();
    }


    public override void Update()
    {
        base.Update();

        if(stateTimer <= 0) //If timer is up, change to air state
        {
            playerStateMachine.ChangeState(player.airState);
        }

        if(player.isGroundDetected()) //If player is on the ground, change to idle state
        {
            playerStateMachine.ChangeState(player.idleState);
        }
    }

} 
