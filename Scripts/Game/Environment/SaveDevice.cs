using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDevice : MonoBehaviour
{
    private Animator _animator;

    [Header("Canvas")] 
    [SerializeField] private Canvas tipsCanvas;

    [Header("Audio")]
    [SerializeField] private AudioData saveAudioData;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetTrigger("Save");

            tipsCanvas.enabled = true;
            
            AudioManager.Instance.PlaySfx(saveAudioData);
            
            GameManager.Instance.playerStats.CurrentHealth = GameManager.Instance.playerStats.MaxHealth;
            
            HUDManager.Instance.UpdateHealth(GameManager.Instance.playerStats.CurrentHealth);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tipsCanvas.enabled = false;
        }
    }
}
