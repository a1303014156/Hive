using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;   //  子弹飞行速度
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    [Header("Audio")] 
    [SerializeField] private AudioData metalAudioData;  //  击中钢铁时的音效数据
    [SerializeField] private AudioData dirtAudioData;   //  击中土地时的音效数据
    [SerializeField] private AudioData bodyAudioData;   //  击中身体时的音效数据

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(Vector2 direction)
    {
        _rigidbody.velocity = direction * speed;
    }

    //  EventSystem 调用
    public void Finish()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _rigidbody.velocity = new Vector2(0, 0);
        
        if (other.CompareTag("GROUND"))
        {
            AudioManager.Instance.PlayRandomSfx(dirtAudioData);
        }

        if (other.CompareTag("ENEMY"))
        {
            AudioManager.Instance.PlayRandomSfx(bodyAudioData);
        }

        if (other.CompareTag("DOOR"))
        {
            AudioManager.Instance.PlayRandomSfx(metalAudioData);
        }
        _animator.SetTrigger("Explode");
    }
}
