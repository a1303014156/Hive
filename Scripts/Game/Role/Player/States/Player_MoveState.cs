using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    public Player_MoveState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Move");
    }

    public void OnLogicUpdate()
    {
        //  当松开移动键时，切换到待机状态
        if (!_parameter.input.Move)
        {
            _manager.TransitionState(PlayerStateType.Idle);
        }
        
        //  当按下跳跃键时，切换到跳跃状态
        if (_parameter.input.jump)
        {
            _manager.TransitionState(PlayerStateType.Jump);
        }
        
        //  当按下翻滚键时，切换到翻滚状态
        if (_parameter.input.roll)
        {
            _manager.TransitionState(PlayerStateType.Roll);
        }
        
        //  当受伤时，切换到受伤状态
        if (_parameter.isHit)
        {
            _manager.TransitionState(PlayerStateType.Hit);
        }
    }

    public void OnPhysicsUpdate()
    {
        _parameter.rig.velocity = new Vector2(_parameter.move * _parameter.character.MoveSpeed, _parameter.rig.velocity.y);
    }

    public void OnExit()
    {
        
    }
}
