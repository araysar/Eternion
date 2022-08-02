using System;
using System.Collections;
using UnityEngine;

public class Golem : Entity
{
    public Rigidbody rb;
    public float attackRange;
    public AttackScript[] attack;
    private AttackScript currentAttack;
    [SerializeField] private GameObject door;

    void Start()
    {
        if (isEnemy == true) GameManager.instance.AddEntity(this);
        MoveAction = Move;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveAction();
    }

    public override void Move()
    {
        GameObject potato = DetectEnemy();
        Vector3 destination = initialTransform;
        if(potato != null)
        {
            destination = potato.transform.position;
            if (Vector3.Distance(destination, transform.position) < attackRange)
            {
                if (attackTimer != null) return;

                currentAttack = attack[UnityEngine.Random.Range(0, attack.Length)];
                StartAttack();
            }
        }
        else
        {
            if (Vector3.Distance(initialTransform, transform.position) < 0.1f)
            {
                _myAnim.SetFloat("speed", 0);
                return;
            }
            
        }
        _myAnim.SetFloat("speed", destination.magnitude);
        transform.LookAt(destination);
        transform.position += transform.forward * FlyWeightPointer.Golem.speed * Time.deltaTime;

    }

    public override void StartAttack()
    {
        MoveAction = delegate { };

        currentAttack.StartAttack();

    }

    public override void Death()
    {
        base.Death();
        if (door != null) Destroy(door);
    }

    public override void EndAttack()
    {
        currentAttack.EndAttack();
    }

}
