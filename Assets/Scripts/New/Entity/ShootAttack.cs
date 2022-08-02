using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : AttackScript
{
    [SerializeField] private Entity _entity;
    [SerializeField] private Animator _myAnim;
    [SerializeField] private BulletSpawner _spawner;

    public override void StartAttack()
    {
        if (!_entity.canAttack) return;

        var potato = _entity.DetectEnemy();
        if (potato != null)
        {
            _entity.transform.LookAt(potato.transform.position);
        }

        _myAnim.SetTrigger(animatorString);
        _entity.MoveAction = delegate { };
        _entity.canAttack = false;
    }

    public override void EndAttack()
    {
        if (_spawner != null) _spawner.ShootMethod();


        _entity.MoveAction = _entity.Move;
        _entity.canAttack = true;
        _myAnim.SetTrigger("exit");
        _entity.currentTarget = null;
    }
}
