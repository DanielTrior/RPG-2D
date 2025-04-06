using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
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

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y); //Set player velocity based on input and speed

        if(xInput == 0) //If player is not moving, change to idle state
            player.stateMachine.ChangeState(player.idleState);

        if(player.isWallDetected()) //If player is on the wall, change to idle slide state
            if(player.facingDir < 0 && xInput < 0)
                player.stateMachine.ChangeState(player.idleState);
            else if(player.facingDir > 0 && xInput > 0)
                player.stateMachine.ChangeState(player.idleState);
    }
}
