using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName) : base(_playerStateMachine, _player, _animBoolName)
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

        if(player.isGroundDetected())
            player.stateMachine.ChangeState(player.idleState);
    }
}

