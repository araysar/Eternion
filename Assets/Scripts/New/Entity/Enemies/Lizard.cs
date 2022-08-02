using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Entity
{
    public Rigidbody rb;
    public float attackRange;
    public float damage = 20;
    public float attackAreaEffect;
    [SerializeField] private Vector3[] wayPoints;
    private int currentWayPoint = 0;

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
        Vector3 destination = wayPoints[currentWayPoint];
        if (potato != null)
        {
            destination = potato.transform.position;
            if (Vector3.Distance(destination, transform.position) < attackRange)
            {
                if (attackTimer != null) return;

                StartAttack();
            }
        }
        else
        {
            if (Vector3.Distance(wayPoints[currentWayPoint], transform.position) < 0.1f)
            {
                if (currentWayPoint == wayPoints.Length - 1) currentWayPoint = 0;
                else currentWayPoint++;
                return;
            }

        }
        _myAnim.SetFloat("speed", destination.magnitude);
        transform.LookAt(destination);
        transform.position += transform.forward * FlyWeightPointer.Lizard.speed * Time.deltaTime;

    }

    public override void StartAttack()
    {
        MoveAction = delegate { };
        _myAnim.SetTrigger("attack");

    }

    public override void EndAttack()
    {
        Collider[] allTargets = Physics.OverlapSphere(attackPoint.position, attackAreaEffect, targetMask);
        if(attackvoice != null)
        {
            myVoice.clip = attackvoice;
            myVoice.Play();
        }
        foreach (var item in allTargets)
        {
            item.GetComponent<Entity>().TakeDamage(damage);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackAreaEffect);
    }
}
