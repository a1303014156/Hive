using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DeathState : IState
{
    private readonly PlayerController _manager;
    private readonly PlayerParameter _parameter;

    private AnimatorStateInfo _info;

    public Player_DeathState(PlayerController manager)
    {
        this._manager = manager;
        this._parameter = manager.parameter;
    }

    public void OnEnter()
    {
        _parameter.animator.Play("Death");
        
        //  播放死亡音效
        AudioManager.Instance.PlaySfx(_parameter.deathAudioData);
    }

    public void OnLogicUpdate()
    {
        _info = _parameter.animator.GetCurrentAnimatorStateInfo(0);

        //  当动画播放完成后，销毁对象
        if (_info.normalizedTime>=0.95f)
        {
            
        }
    }

    public void OnPhysicsUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
