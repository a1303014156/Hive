using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HitState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    private AnimatorStateInfo _info;
    private IState _stateImplementation;

    public Player_HitState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Hit");
        
        AudioManager.Instance.PlaySfx(_parameter.hitAudioData);
    }

    public void OnLogicUpdate()
    {
        _info = _parameter.animator.GetCurrentAnimatorStateInfo(0);

        //  当当前生命值小于0时，切换到死亡状态
        if (_parameter.character.CurrentHealth<=0f)
        {
            _manager.TransitionState(PlayerStateType.Death);
        }

        //  当动画播放完成时，进入待机状态
        if (_info.normalizedTime>=0.95f)
        {
            _manager.TransitionState(PlayerStateType.Idle);
        }
    }

    public void OnPhysicsUpdate()
    {
        
    }

    public void OnExit()
    {
        _parameter.isHit = false;
    }
}
