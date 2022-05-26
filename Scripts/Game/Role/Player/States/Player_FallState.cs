using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    private float _timer;

    public Player_FallState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Fall");
    }

    public void OnLogicUpdate()
    {
        _timer += Time.deltaTime;
        
        //  当受伤时，进入受伤状态
        if (_parameter.isHit)
        {
            _manager.TransitionState(PlayerStateType.Hit);
        }

        //  当接触到地面时，判断在空中的时间是否超过1秒，是则切换到落地状态，否则切换到待机状态
        if (!_parameter.isGround) return;
        _manager.TransitionState(_timer >= 1 ? PlayerStateType.Land : PlayerStateType.Idle);
    }

    public void OnPhysicsUpdate()
    {
        
    }

    public void OnExit()
    {
        //  计时器清零
        _timer = 0;
    }
}
