using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAoeAttack : AttackScript
{
    Entity _myEntity;
    public float attackAreaEffect = 2;
    public float damage = 20;
    public AudioClip attackClip;

    private void Start()
    {
        _myEntity = GetComponentInParent<Entity>();
    }
    public override void StartAttack()
    {
        GetComponentInParent<Animator>().SetTrigger(animatorString);
    }
    public override void EndAttack()
    {
        if (_myEntity == null) return;

        Collider[] allTargets = Physics.OverlapSphere(_myEntity.attackPoint.position, attackAreaEffect, _myEntity.targetMask);
        foreach (var item in allTargets)
        {
            item.GetComponent<Entity>().TakeDamage(damage);
            if (_myEntity.attackvoice != null)
            {
                _myEntity.myVoice.clip = attackClip;
                _myEntity.myVoice.Play();
            }
        }
    }

    public override void PowerUp(float multiplier)
    {
        throw new System.NotImplementedException();
    }
}
