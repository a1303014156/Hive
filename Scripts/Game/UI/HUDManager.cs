using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] private Animator animator;

    [SerializeField] private Canvas hudCanvas;

    [SerializeField] private PlayerController player;

    private void Update()
    {
        if (player.transform.localScale.x==-1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void UpdateHealth(float currentHealth)
    {
        switch (currentHealth)
        {
            case 10:
                animator.Play("One Hundred Percent");
                break;
            case 9:
                animator.Play("Ninety Percent");
                break;
            case 8:
                animator.Play("Eighty Percent");
                break;
            case 7:
                animator.Play("Seventy Percent");
                break;
            case 6:
                animator.Play("Sixty Percent");
                break;
            case 5:
                animator.Play("Fifty Percent");
                break;
            case 4:
                animator.Play("Forty Percent");
                break;
            case 3:
                animator.Play("Thirty Percent");
                break;
            case 2:
                animator.Play("Twenty Percent");
                break;
            case 1:
                animator.Play("Ten Percent");
                break;
            case 0:
                hudCanvas.enabled = false;
                break;
        }
    }
}
