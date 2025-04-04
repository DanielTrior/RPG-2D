using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine playerStateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    private string animBoolName;

    public PlayerState(PlayerStateMachine _playerStateMachine, Player _player, string _animBoolName)
    {
        this.playerStateMachine = _playerStateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }

}
