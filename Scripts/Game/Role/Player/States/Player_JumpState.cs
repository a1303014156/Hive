using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    public Player_JumpState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Jump");

        _parameter.rig.velocity = new Vector2(_parameter.rig.velocity.x, _parameter.character.JumpForce);
    }

    public void OnLogicUpdate()
    {
        //  当受伤时，切换到受伤状态
        if (_parameter.isHit)
        {
            _manager.TransitionState(PlayerStateType.Hit);
        }
        
        //  当角色下落时，切换到下落状态
        if (!(_parameter.rig.velocity.y <= 0f)) return;
        _parameter.animator.Play("Jump Transition Fall");
        _manager.TransitionState(PlayerStateType.Fall);
    }

    public void OnPhysicsUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
