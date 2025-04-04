using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    override public void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update(); 

        if(rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(player.airState);
        }
    }

}