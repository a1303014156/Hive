using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hive.Character
{
    [CreateAssetMenu(menuName = "Character Stats/Data", fileName = "New Data")]
    public class CharacterData_SO : ScriptableObject
    {
        [Header("基本数值")] 
        public int maxHealth;
        public int currentHealth;
        public int moveSpeed;
        public int jumpForce;
        public int rollForce;
    }
}