using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Entity
{
    public Rigidbody rb;
    public float attackRange;
    public Arrow arrowpref;
    public float arrowDamage;
    public float arrowSpeed;

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
        if (potato != null)
        {
            destination = potato.transform.position;
            _myAnim.SetFloat("speed", destination.magnitude);
            transform.LookAt(destination);
            transform.position += transform.forward * FlyWeightPointer.Lizard.speed * Time.deltaTime;
            if (Vector3.Distance(destination, transform.position) < attackRange && canAttack)
            {
                if (attackTimer != null) return;

                StartAttack();
            }
        }
        else
        {
            if (Vector3.Distance(initialTransform, transform.position) < 0.25f)
            {
                _myAnim.SetFloat("speed", 0);
                return;
            }

            _myAnim.SetFloat("speed", destination.magnitude);
            transform.LookAt(destination);
            transform.position += transform.forward * FlyWeightPointer.Lizard.speed * Time.deltaTime;

        }
    }

    public override void StartAttack()
    {
        MoveAction = delegate { };
        _myAnim.SetTrigger("attack");

    }

    public override void EndAttack()
    {
        if (attackvoice != null)
        {
            myVoice.clip = attackvoice;
            myVoice.Play();
        }
        Arrow arrow = Instantiate(arrowpref, transform.position, transform.rotation);
        arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
        arrow.damage = arrowDamage;
        ExitAnimation();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
