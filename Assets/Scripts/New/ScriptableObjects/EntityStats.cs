using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "EntityStats")]
public class EntityStats : ScriptableObject
{
    public float maxHP;
    public float currentHP;
    public float maxMP;
    public float currentMP;
    public float attackDamage;
    public float magicDamage;
    public float speed;
}
