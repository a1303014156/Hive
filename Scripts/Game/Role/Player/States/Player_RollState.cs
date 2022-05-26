using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RollState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    private AnimatorStateInfo _info;

    public Player_RollState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Roll");

        _parameter.isRoll = true;

        _parameter.rig.velocity = new Vector2(_parameter.move * _parameter.character.RollForce, _parameter.rig.velocity.y);
    }

    public void OnLogicUpdate()
    {
        _info = _parameter.animator.GetCurrentAnimatorStateInfo(0);
        
        //  当动画播放完后，切换到待机状态
        if (_info.normalizedTime>=0.95f)
        {
            _manager.TransitionState(PlayerStateType.Idle);
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
        _parameter.isRoll = false;
    }
}
