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
        player.ResetDash();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
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

        if (xInput != 0)
        {
            float airControlFactor = 0.7f; // Adjust this value to control air movement responsiveness
            player.SetVelocity(player.moveSpeed * airControlFactor * xInput, rb.velocity.y);
        }

        if(rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(player.airState);
        }
    }

}