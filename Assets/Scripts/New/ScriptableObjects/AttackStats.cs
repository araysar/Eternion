using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "AttackStats")]
public class AttackStats : ScriptableObject
{
    public string attackName;
    public string animatorTriggerParameterName = "Attack";
    public float damage = 0;
    public bool isAoe = false;
    public AudioClip castSound;
    public AudioClip attackSound;
}
