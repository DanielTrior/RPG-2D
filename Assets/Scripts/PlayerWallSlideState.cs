using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKey(KeyCode.Space)) //If player presses space, change to jump state
        {
            playerStateMachine.ChangeState(player.wallJumpState);
            return;
        }
        if(!player.isWallDetected() ) //If player is not on the wall, change to idle state
            playerStateMachine.ChangeState(player.idleState);
            

        if(xInput != 0 && player.facingDir != xInput) //If player is moving in the opposite direction of the wall, change to idle state
        {
            if(player.facingDir < 0 && xInput > 0)
                playerStateMachine.ChangeState(player.idleState);
            else if(player.facingDir > 0 && xInput < 0)
                playerStateMachine.ChangeState(player.idleState); 
        }

        if(yInput < 0) //If player is moving down, apply downward velocity
            rb.velocity = new Vector2(0, rb.velocity.y * 0.990f);
        else //If player is not moving down, apply reduced downward velocity
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if (player.isGroundDetected()) //If player is on the ground, change to idle state
            playerStateMachine.ChangeState(player.idleState);
    }
}
