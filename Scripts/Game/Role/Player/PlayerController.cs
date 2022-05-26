using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public enum PlayerStateType
{
    Idle,Move,Jump,Fall,Land,Roll,Hit,Death
}

[Serializable]
public class PlayerParameter
{
    public PlayerInput input;   //  玩家按键动作表
    
    [Header("Module")]
    public Animator animator;   //  动画组件
    public Rigidbody2D rig; //  重力组件
    public CharacterStats character;    //  角色数值组件

    [Header("Move")] 
    public int move;    //  移动方向

    [Header("States")] 
    public bool isGround;   //  是否接触地面
    public bool isHit;  //  是否受伤
    public bool isRoll; //  是否翻滚
    
    [Header("Ground Check")] 
    public float footOffset=0.8f;    //  脚部距离
    public float groundDistance=0.8f;    //  地面检测距离
    public LayerMask groundLayer;    //  地面检测图层

    [Header("Hit Back")]
    public float backForce; //  后退力度
    public Vector2 backDirection;   //  后退方向

    [Header("Audio")] 
    public AudioData stepAudioData; //  脚步声数据
    public AudioData hitAudioData;  //  受伤声数据
    public AudioData deathAudioData;    //  死亡声数据
}

public class PlayerController : MonoBehaviour
{
    public PlayerParameter parameter;
    private IState _currentState;
    private readonly Dictionary<PlayerStateType, IState> _states = new Dictionary<PlayerStateType, IState>();

    private void Awake()
    {
        parameter.animator = GetComponent<Animator>();
        parameter.rig = GetComponent<Rigidbody2D>();
        parameter.character = GetComponent<CharacterStats>();

        //  玩家角色创建时，相机自动追踪玩家角色
        ProCamera2D.Instance.AddCameraTarget(transform);
    }

    private void Start()
    {
        _states.Add(PlayerStateType.Idle, new Player_IdleState(this));
        _states.Add(PlayerStateType.Move, new Player_MoveState(this));
        _states.Add(PlayerStateType.Jump, new Player_JumpState(this));
        _states.Add(PlayerStateType.Fall, new Player_FallState(this));
        _states.Add(PlayerStateType.Land, new Player_LandState(this));
        _states.Add(PlayerStateType.Roll, new Player_RollState(this));
        _states.Add(PlayerStateType.Hit, new Player_HitState(this));
        _states.Add(PlayerStateType.Death, new Player_DeathState(this));
        
        GameManager.Instance.RigisterPlayer(parameter.character);
        
        HUDManager.Instance.UpdateHealth(parameter.character.CurrentHealth);
        
        TransitionState(PlayerStateType.Idle);
    }

    private void Update()
    {
        parameter.move = parameter.input.moveValue.x switch
        {
            > 0 => 1,
            < 0 => -1,
            _ => parameter.move
        };

        _currentState.OnLogicUpdate();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        
        _currentState.OnPhysicsUpdate();
    }
    
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="type">状态名称</param>
    public void TransitionState(PlayerStateType type)
    {
        _currentState?.OnExit();

        _currentState = _states[type];
        _currentState.OnEnter();
    }

    /// <summary>
    /// 检测角色是否站在地面的方法
    /// </summary>
    private void GroundCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-parameter.footOffset, 0f), Vector2.down, parameter.groundDistance, parameter.groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(parameter.footOffset, 0f), Vector2.down, parameter.groundDistance, parameter.groundLayer);

        if (leftCheck || rightCheck)
            parameter.isGround = true;
        else
            parameter.isGround = false;
    }

    /// <summary>
    /// 【角色受到攻击的方法】：
    /// 当角色受到攻击时，生命值减少，面朝攻击方向，且被击退一定距离
    /// </summary>
    /// <param name="direction">面朝方向</param>
    /// <param name="backForce">击退距离</param>
    /// <param name="damage">受到伤害</param>
    public void GetHit(Vector2 direction, float backForce, int damage)
    {
        parameter.backDirection = direction;
        parameter.backForce = backForce;

        if (!parameter.isHit)
        {
            parameter.character.CurrentHealth -= damage;
            
            #if UNITY_EDITOR
                Debug.Log("当前血量：" + parameter.character.CurrentHealth);
            #endif

            HUDManager.Instance.UpdateHealth(parameter.character.CurrentHealth);
        }

        parameter.isHit = true;
    }
    
    /// <summary>
    /// 重写射线方法
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="rayDiraction">射线距离</param>
    /// <param name="length">射线长度</param>
    /// <param name="layer">检测图层</param>
    /// <returns></returns>
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDiraction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDiraction, length, layer);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDiraction * length, color);

        return hit;
    }

    #region 播放音效

    //  EventSystem 调用
    public void PlayerStepAudio()
    {
        AudioManager.Instance.PlaySfx(parameter.stepAudioData);
    }

    #endregion
}
