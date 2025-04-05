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

        if(!player.isWallDetected() )
            playerStateMachine.ChangeState(player.idleState);
            

        if(xInput != 0 && player.facingDir != xInput)
        {
            if(player.facingDir < 0 && xInput > 0)
                playerStateMachine.ChangeState(player.idleState);
            else if(player.facingDir > 0 && xInput < 0)
                playerStateMachine.ChangeState(player.idleState); 
        }

        if(yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y * 0.990f);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if (player.isGroundDetected())
            playerStateMachine.ChangeState(player.idleState);
    }
}
