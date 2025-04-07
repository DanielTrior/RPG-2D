using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter; //Counter for the number of attacks in the combo
    private float listTimeAttacked; //Time of the last attack in the combo
    private float comboWindow = 2f; //Time window for combo attacks
    public PlayerPrimaryAttackState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(comboCounter > 2 || Time.time > listTimeAttacked + comboWindow) //Reset combo counter if it exceeds 2 or if the time since the last attack exceeds the combo window
        {
            comboCounter = 0;
        }

        player.animator.SetInteger("ComboCounter", comboCounter); //Set the combo counter in the animator

        float attackDirX = player.facingDir; //change direction of attack based on input
        if(xInput != 0) //If there is input, set the attack direction to the input direction

        {
            attackDirX = xInput;
        }

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDirX, player.attackMovement[comboCounter].y); //Set player velocity based on attack movement and direction

        stateTimer = .1f; //Set timer for state duration
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f); //dont let player move for a bit after attack

        comboCounter ++; //Increment the combo counter
        listTimeAttacked = Time.time;  //Set the time of the last attack to the current time
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <0) //If timer is up, change to air state

        {
            player.ZeroVelocity();
        }

        if(triggerCalled) //If the trigger is called, set the trigger in the animator and reset the triggerCalled variable

        {
            playerStateMachine.ChangeState(player.idleState);
            return;
        }
    }
}

