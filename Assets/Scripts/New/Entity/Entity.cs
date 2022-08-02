using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public Image healthBar;
    public GameObject deathEffect;
    public AudioSource myVoice;
    public AudioClip deathvoice;
    public AudioClip damagedvoice;
    public AudioClip attackvoice;

    [Header("Attack")]
    public Transform attackPoint;

    public bool dead = false;
    protected Animator _myAnim;
    [SerializeField] protected string animatorAttack = "attack";
    public bool canAttack = true;
    public float viewEnemyDistance;
    public bool isEnemy = false;
    public float currentHP;
    public float maxHP;

    public int loseScene = 4;
    
    public Vector3 initialTransform;

    public GameObject currentTarget;
    public Action MoveAction;
    public Coroutine attackTimer;
    public float attackCooldown;

    public LayerMask targetMask;
    public float visionRange = 7;

    void Awake()
    {
        myVoice = gameObject.AddComponent<AudioSource>();
        myVoice.loop = false;
        currentHP = maxHP;
        UpdateLifeBar();
        _myAnim = GetComponent<Animator>();
        MoveAction = delegate { };
        initialTransform = transform.position;
    }

    void Update()
    {
        MoveAction();
    }

    public GameObject DetectEnemy()
    {
        Collider[] allTargets = Physics.OverlapSphere(transform.position, visionRange, targetMask);
        GameObject closestTarget = null;
        if (allTargets == null) return null;
        foreach (var item in allTargets)
        {
            if (closestTarget == null) closestTarget = item.gameObject;
            else
            {
                if (Vector3.Distance(item.gameObject.transform.position, transform.position) < Vector3.Distance(closestTarget.gameObject.transform.position, transform.position))
                {
                    closestTarget = item.gameObject;
                }
            }
        }

        return closestTarget;
    }

    public abstract void Move();

    public abstract void StartAttack();
    public abstract void EndAttack();

    public void StartOver()
    {
        gameObject.SetActive(true);
        transform.position = initialTransform;
    }

    public void EndGame()
    {
        gameObject.SetActive(false);
    }

    #region Damage
    public void TakeDamage(float dmg)
    {
        if (dead == false)
        {
            currentHP -= dmg;
            if(healthBar != null)
            {
                UpdateLifeBar();
            }
            if (currentHP <= 0) Death();
            else
            {
                if (damagedvoice != null)
                {
                    myVoice.clip = damagedvoice;
                    myVoice.Play();
                }
            }
        }
    }

    public virtual void Death()
    {
        if (dead == false)
        {
            if(deathvoice != null)
            {
                myVoice.clip = deathvoice;
                myVoice.Play();
            }
            dead = true;
            MoveAction = delegate { };
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _myAnim.SetTrigger("death");
            gameObject.layer = 0;
            GameManager.instance.RemoveEntity(this);
            StartCoroutine(DeathTimer());
        }
    }

    public void UpdateLifeBar()
    {
        if(healthBar != null) healthBar.fillAmount = currentHP / maxHP;
    }

    protected IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackCooldown);

        if (!dead)
        {
            MoveAction = Move;
            attackTimer = null;
        }
    }

    public void ExitAnimation()
    {
        _myAnim.SetFloat("speed", 0);
        _myAnim.SetTrigger("exit");
        attackTimer = StartCoroutine(AttackTimer());
    }

    private IEnumerator DeathTimer()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3);
        if (!isEnemy)
        {
            SceneChanger.instance.LoadLevel(loseScene);
            PlayerPrefs.SetInt("PowerUp", 0);
            PlayerPrefs.Save();
        }
        

        else
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Revive(float hp, float mp, bool fullRevive)
    {
        if (dead == true) dead = false;

        if (fullRevive == true)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP = hp;
        }
        _myAnim.SetTrigger("exit");
        if (isEnemy == true) GameManager.instance.AddEntity(this);
    }
    #endregion
}
