using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Character Stats/Attack", fileName = "New Attack")]
public class AttackData_SO : ScriptableObject
{
    public int damage;
    public int backForce;
    public int directionX;
    public int directionY;
}
