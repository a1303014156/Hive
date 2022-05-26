using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Audio")] 
    [SerializeField] private AudioData openAudioData;
    [SerializeField] private AudioData closeAudioData;

    private void Awake()
    {
        animator.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            #if UNITY_EDITOR
                Debug.Log("开门");
            #endif
            animator.SetTrigger("Open");
            AudioManager.Instance.PlaySfx(openAudioData);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            #if UNITY_EDITOR
                Debug.Log("关门");
            #endif
            animator.SetTrigger("Close");
            AudioManager.Instance.PlaySfx(closeAudioData);
        }
    }
}
