using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Shaman : Entity
{
    [Header("Stats")]
    [SerializeField] private bool onFight = false;
    [SerializeField] private float percentLifeToHeal = 90;
    [SerializeField] private float timeToStartFight = 2;
    [SerializeField] private AttackScript[] attackScript;
    private AttackScript currentAttack;
    [SerializeField] private float attackDelay;

    void Start()
    {
        GameManager.instance.bossFightEvent += StartFight;
        MoveAction = delegate { };
    }
    public override void Move()
    {
        if (onFight && currentHP > 0 && currentTarget != null)
        {
            currentAttack = attackScript[Random.Range(0, attackScript.Length)];
            StartAttack();

            transform.LookAt(currentTarget.transform);
        }
    }
    public void StartFight()
    {
        StartCoroutine(StartingFight());
    }

    public IEnumerator StartingFight()
    {
        yield return new WaitForSeconds(timeToStartFight);
        MoveAction = Move;
        onFight = true;
        currentTarget = DetectEnemy();
    }

    public override void StartAttack()
    {
        if (currentAttack == null) return;
        if (dead == true) return;
        MoveAction = delegate { };

        currentAttack.StartAttack();
    }

    public override void EndAttack()
    {
        if (dead == true) return;
        currentAttack.EndAttack();
    }

}
