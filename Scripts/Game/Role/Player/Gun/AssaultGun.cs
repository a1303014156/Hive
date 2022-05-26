using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultGun : MonoBehaviour
{
    private InputActions _inputActions;
    [SerializeField] private PlayerInput input; //  玩家按键动作表
    [SerializeField] private PlayerController player;   //  玩家脚本

    [SerializeField] private float interval;    //  射击间隔时间
    [SerializeField] private GameObject bulletPrefab;   //  子弹预制体
    [SerializeField] private Transform muzzlePos;   //  枪口位置
    [SerializeField] private Vector2 mousePos;  //  鼠标位置
    [SerializeField] private Vector2 mousePos1;  //  鼠标位置
    [SerializeField] private Vector2 direction; //  发射方向
    
    private WaitForSeconds _waitForFireInterval;
    
    private Animator _animator;
    
    [Header("Audio")]
    [SerializeField] private AudioData fireAudioData;   //  开火音效数据

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();
    }

    private void OnEnable()
    {
        input.onFire += Fire;
        input.onStopFire += StopFire;
    }

    private void OnDisable()
    {
        input.onFire -= Fire;
        input.onStopFire -= StopFire;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");

        // 鼠标移动时即时反馈坐标
        _inputActions.GamePlay.MousePosition.performed += ctx => mousePos1 = ctx.ReadValue<Vector2>();
        
        input.EnableGameplayInput();
        _waitForFireInterval = new WaitForSeconds(interval);
    }

    private void Update()
    {
        if (Camera.main != null) mousePos = Camera.main.ScreenToWorldPoint(mousePos1);

        if (mousePos.x<player.transform.position.x)
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
            transform.localScale = new Vector3(-1, -1, 1);
        }else if (mousePos.x>player.transform.position.x)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
            transform.localScale = new Vector3(1, 1, 1);
        }

        Shoot();
    }

    private void Shoot()
    {
        var transform1 = transform;
        var position = transform1.position;

        direction = (mousePos-new Vector2(position.x,position.y)).normalized;
        transform1.right = direction;
    }

    private void Fire()
    {
        StartCoroutine(nameof(FireCoroutine));
    }

    private void StopFire()
    {
        StopCoroutine(nameof(FireCoroutine));
    }

    /// <summary>
    /// 发射子弹的携程
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            _animator.SetTrigger("Fire");

            // var bullet = Instantiate(bulletPrefab, muzzlePos.position,Quaternion.identity);

            var bullet = PoolManager.Release(bulletPrefab, muzzlePos.position);

            AudioManager.Instance.PlayRandomSfx(fireAudioData);

            bullet.GetComponent<Bullet>().SetSpeed(direction);

            yield return _waitForFireInterval;
        }
    }
}
