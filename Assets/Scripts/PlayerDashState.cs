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

        stateTimer = player.dashDuration;
    }
     public override void Exit()
    {
        base.Exit();

         player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        
        player.rb.velocity = new Vector2(player.facingRight ? player.dashSpeed : -player.dashSpeed, 0);

        if(stateTimer <= 0)
        {
            playerStateMachine.ChangeState(player.idleState);
        }
    }
}
