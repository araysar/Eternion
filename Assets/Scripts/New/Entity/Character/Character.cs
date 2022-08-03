using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Entity
{
    [HideInInspector] public Rigidbody rb;
    [SerializeField] private AttackScript _attack;
    [SerializeField] private Controller _controller;
    public GameObject powerUpEffect;
    public bool healAvailable = true;

    void Start()
    {
        if (PlayerPrefs.GetFloat("PlayerHP") != 0) currentHP = PlayerPrefs.GetFloat("PlayerHP");
        else
        {
            currentHP = maxHP;
            PlayerPrefs.SetFloat("PlayerHP", maxHP);
        }

        if (PlayerPrefs.GetInt("PowerUp") == 1) powerUpEffect.SetActive(true);
        else powerUpEffect.SetActive(false);

        UpdateLifeBar();
        rb = GetComponent<Rigidbody>();
        MoveAction = Move;
       _myAnim.SetFloat("speed", 0f);

        isEnemy = false;
        GameManager.instance.AddEntity(this);
        GameManager.instance.player = gameObject;
    }                                       

    public override void Move()
    {
        if (_controller == null) return;

        if (_controller.MoveDirection() != Vector3.zero)
        {
            rb.velocity = -_controller.MoveDirection() * FlyWeightPointer.Character.speed;
            _myAnim.SetFloat("speed", rb.velocity.magnitude);
            transform.forward = Vector3.ClampMagnitude(-_controller.MoveDirection(), 0.01f);
        }
        else
        {
            rb.velocity = Vector3.zero;
            _myAnim.SetFloat("speed", 0);
        }
    }

    public override void StartAttack()
    {
        if (_attack == null) return;
        if (dead == true) return;

        rb.velocity = Vector3.zero;
        _attack.StartAttack();
    }

    public override void EndAttack()
    {
        if (_attack == null) return;

        if (attackvoice != null)
        {
            myVoice.clip = attackvoice;
            myVoice.Play();
        }
        _attack.EndAttack();
    }
}
