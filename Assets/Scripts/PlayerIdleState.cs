using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
    }
    
    override public void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.isWallDetected())  //Stop player from moving if they are on a wall   
            if(player.facingDir < 0 && xInput < 0)  
                return;
            else if(player.facingDir > 0 && xInput > 0)
                return;

        if(xInput != 0 && !player.isBusy)   //If player is not busy, change to move state
            player.stateMachine.ChangeState(player.moveState);
    }

}
