using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Action", menuName = "Input System/Action")]
public class PlayerInput : ScriptableObject, InputActions.IGamePlayActions, InputActions.IPauseMenuActions,InputActions.IDialogueMenuActions
{
    private InputActions _inputActions;

    private Vector2 Axes => _inputActions.GamePlay.Move.ReadValue<Vector2>();
    public event UnityAction onMove = delegate {  };
    public event UnityAction onStopMove = delegate { };
    public bool Move => AxisX != 0f;
    private float AxisX => Axes.x;
    private float AxisY => Axes.y;
    public Vector2 moveValue;

    public event UnityAction onJump = delegate {  };
    public event UnityAction OnStopJump = delegate {  };
    public bool jump;
    
    public event UnityAction onFire= delegate {  };
    public event UnityAction onStopFire= delegate {  };
    public bool fire;

    public event UnityAction onRoll= delegate {  };
    public event UnityAction onStopRoll= delegate {  };
    public bool roll;
    
    // public event UnityAction onInteraction = delegate {  };
    // public event UnityAction onStopInteraction = delegate {  };
    // public bool interaction;

    public event UnityAction onPause = delegate {  };
    public event UnityAction onUnPause = delegate {  };
    
    public event UnityAction onDialogue= delegate {  };
    public event UnityAction onStopDialogue= delegate {  };

    private void OnEnable()
    {
        _inputActions = new InputActions();
        
        _inputActions.GamePlay.SetCallbacks(this);
        _inputActions.PauseMenu.SetCallbacks(this);
        _inputActions.DialogueMenu.SetCallbacks(this);
    }
    
    private void OnDisable()
    {
        DisableAllInput();
    }
    
    /// <summary>
    /// 切换输入表
    /// </summary>
    /// <param name="actionMap">动作表名称</param>
    /// <param name="isUIInput">是否为UI</param>
    private void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
    {
        _inputActions.Disable();
        actionMap.Enable();

        if (isUIInput)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    /// <summary>
    /// 关闭所有输入表
    /// </summary>
    public void DisableAllInput() => _inputActions.Disable();
    
    /// <summary>
    /// 切换到游戏输入表
    /// </summary>
    public void EnableGameplayInput() => SwitchActionMap(_inputActions.GamePlay, true);

    /// <summary>
    /// 切换到暂停菜单输入表
    /// </summary>
    public void EnablePauseMenuInput() => SwitchActionMap(_inputActions.PauseMenu, true);

    /// <summary>
    /// 切换到对话系统输入表
    /// </summary>
    public void EnableDialogueMenuInput() => SwitchActionMap(_inputActions.DialogueMenu, true);

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onFire.Invoke();
            fire = true;
        }

        if (context.canceled)
        {
            onStopFire.Invoke();
            fire = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onMove.Invoke();
            moveValue = context.ReadValue<Vector2>().normalized;
        }

        if (context.canceled)
        {
            onStopMove.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onJump.Invoke();
            jump = true;
        }

        if (context.canceled)
        {
            OnStopJump.Invoke();
            jump = false;
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onRoll.Invoke();
            roll = true;
        }

        if (context.canceled)
        {
            onStopRoll.Invoke();
            roll = false;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onPause.Invoke();
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
    }

    public void OnUnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onUnPause.Invoke();
        }
    }

    public void OnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onDialogue.Invoke();
        }
    }

    public void OnUnDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onStopDialogue.Invoke();
        }
    }
}
