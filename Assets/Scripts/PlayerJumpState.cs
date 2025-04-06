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
        player.ResetDash(); // Reset dash count when jumping
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);  // Apply jump force to the player
    }

    override public void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update(); 

        if(Input.GetKeyDown(KeyCode.Space) && player.currentJumps > 0) // If player presses space and has jumps left, change to jump state
        {
            player.currentJumps--;
            player.stateMachine.ChangeState(player.jumpState);
        }

        if (xInput != 0) // If player is moving in the air, apply air control
        {
            float airControlFactor = 0.7f; // Adjust this value to control air movement responsiveness
            player.SetVelocity(player.moveSpeed * airControlFactor * xInput, rb.velocity.y);
        }

        if(rb.velocity.y < 0) // If player is falling, change to air state
        {
            player.stateMachine.ChangeState(player.airState);
        }
    }

}