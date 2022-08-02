using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackScript : MonoBehaviour
{
    public string animatorString = "attack";
    public abstract void StartAttack();
    public abstract void EndAttack();
}
