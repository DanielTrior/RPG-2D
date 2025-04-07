using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration; //Set timer for state duration
    }
     public override void Exit()
    {
        base.Exit();

         player.SetVelocity(0, rb.velocity.y); //Set player velocity to 0 on exit
    }

    public override void Update()
    {
        base.Update();

        if(!player.isGroundDetected() && player.isWallDetected()) //If player is not on the ground and is on a wall, change to wall jump state
        {
            playerStateMachine.ChangeState(player.airState);
            return;
        }
        
        player.rb.velocity = new Vector2(player.facingRight ? player.dashSpeed : -player.dashSpeed, 0); //Set player velocity based on dash speed and direction

        if(stateTimer <= 0) //If timer is up, change to air state
        {
            playerStateMachine.ChangeState(player.idleState);
        }
    }
}
