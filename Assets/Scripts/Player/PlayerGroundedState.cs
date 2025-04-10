using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState  
{
    public PlayerGroundedState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    
    override public void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        //Attack
        if(Input.GetKey(KeyCode.Mouse0))    //Primary attack    
        {
            playerStateMachine.ChangeState(player.primaryAttackState);
            return;
        }

        if(!player.isGroundDetected())  //If player is not on the ground, change to air state
        {
            playerStateMachine.ChangeState(player.airState);
        }
        

        if(Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected())  //If player is on the ground, change to jump state
        { 
            player.currentJumps--;
            player.stateMachine.ChangeState(player.jumpState);
        }
    }
}
