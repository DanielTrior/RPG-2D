using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine playerStateMachine;    
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    protected string animBoolName;  //animation name for animator controller
    protected float stateTimer; //timer for state duration

    protected bool triggerCalled; //trigger for attack animation

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
        triggerCalled = false;
    }

    public virtual void Exit()
    {
         player.animator.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        player.animator.SetFloat("yVelocity", rb.velocity.y);   
    }

    public virtual void AnimationFinishTrigger()    // Trigger for animation finish event
    {
        triggerCalled = true;
    }
}
