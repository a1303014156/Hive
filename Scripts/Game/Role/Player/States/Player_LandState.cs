using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LandState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    private AnimatorStateInfo _info;

    public Player_LandState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Crouch or land");
    }

    public void OnLogicUpdate()
    {
        _info = _parameter.animator.GetCurrentAnimatorStateInfo(0);

        //  当动画播放完成时，切换到待机状态
        if (_info.normalizedTime>=0.95f)
        {
            _manager.TransitionState(PlayerStateType.Idle);
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
        
    }
}
