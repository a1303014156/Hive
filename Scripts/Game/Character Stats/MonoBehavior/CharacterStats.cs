using System;
using System.Collections;
using System.Collections.Generic;
using Hive.Character;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO templateData;
    public CharacterData_SO characterData;
    public AttackData_SO attackData;

    private void Awake()
    {
        if (templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }

    #region Read form Data_SO

    public int MaxHealth
    {
        get { if (characterData != null) return characterData.maxHealth;else return 0; }
        set { characterData.maxHealth = value; }
    }
    
    public int CurrentHealth
    {
        get { if (characterData != null) return characterData.currentHealth;else return 0; }
        set { characterData.currentHealth = value; }
    }

    public int MoveSpeed
    {
        get { if (characterData != null) return characterData.moveSpeed;else return 0; }
        set { characterData.moveSpeed = value; }
    }
    
    public int JumpForce
    {
        get { if (characterData != null) return characterData.jumpForce;else return 0; }
        set { characterData.jumpForce = value; }
    }
    
    public int RollForce
    {
        get { if (characterData != null) return characterData.rollForce;else return 0; }
        set { characterData.rollForce = value; }
    }

    #endregion
}
