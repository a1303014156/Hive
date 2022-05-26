using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    public Player_IdleState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }
    
    public void OnEnter()
    {
        _parameter.animator.Play("Idle");
        
        //  锁定X，Y轴，防止在斜坡上滑下
        _parameter.rig.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void OnLogicUpdate()
    {
        //  当按下移动键时。切换到移动状态
        if (_parameter.input.Move)
        {
            _manager.TransitionState(PlayerStateType.Move);
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
        
    }

    public void OnExit()
    {
        //  解除Y轴的限制
        _parameter.rig.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
