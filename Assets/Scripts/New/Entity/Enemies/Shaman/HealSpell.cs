using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : AttackScript
{
    [Header("GameObjects")]
    [SerializeField] private GameObject castHealEffect;
    [SerializeField] private GameObject launchHealEffect;
    [SerializeField] private GameObject interruptEffect;
    [HideInInspector] public GameObject currentTarget;


    [Header("Audio Clips")]
    [SerializeField] private AudioClip castHealClip;
    [SerializeField] private AudioClip launchHealClip;
    [SerializeField] private AudioClip spellInterruptedClip;

    private AudioSource castHealAS;
    private AudioSource healLaunchAS;
    private Entity _myEntity;
    private Coroutine actionCoroutine;

    [Header("Misc")]
    [SerializeField] private float hpLostToInterrupt;
    [SerializeField] private float castingTime;
    [SerializeField] private float hpHealed;
    [SerializeField] private string animatorCastName;
    [SerializeField] private string animatorLaunchName;

    private void Start()
    {
        currentTarget = gameObject;
        castHealAS = gameObject.AddComponent<AudioSource>();
        healLaunchAS = gameObject.AddComponent<AudioSource>();
        castHealAS.clip = castHealClip;
        healLaunchAS.clip = launchHealClip;

        _myEntity = GetComponentInParent<Entity>();
    }

    public override void StartAttack()
    {
        EndAttack();
    }

    public override void EndAttack()
    {
        if (actionCoroutine != null) return;

        actionCoroutine = StartCoroutine(Healing());
    }

    private IEnumerator Healing()
    {
        castHealEffect.SetActive(true);
        castHealAS.Play();
        GetComponentInParent<Animator>().SetTrigger(animatorCastName);
        float entityHp = _myEntity.currentHP;
        bool interrupt = false;
        for (float i = 0; interrupt == false ; i += Time.deltaTime)
        {
            if ((entityHp - _myEntity.currentHP) >= hpLostToInterrupt)
            {
                castHealEffect.SetActive(false);
                StartCoroutine(TimerParticles(interruptEffect, interruptEffect.GetComponentInChildren<ParticleSystem>().main.duration + interruptEffect.GetComponentInChildren<ParticleSystem>().startLifetime));
                castHealAS.Stop();
                healLaunchAS.clip = spellInterruptedClip;
                healLaunchAS.Play();
                interrupt = true;
            }

            if (_myEntity.currentHP <= 0)
            {
                healLaunchAS.Stop();
                castHealEffect.SetActive(false);
                yield break;
            }

            if (i >= castingTime)
            {
                castHealEffect.SetActive(false);
                _myEntity.currentHP += hpHealed;
                if (_myEntity.currentHP > _myEntity.maxHP) _myEntity.currentHP = _myEntity.maxHP;

                _myEntity.UpdateLifeBar();
                StartCoroutine(TimerParticles(launchHealEffect, launchHealEffect.GetComponentInChildren<ParticleSystem>().main.duration));
                healLaunchAS.Stop();
                healLaunchAS.clip = launchHealClip;
                healLaunchAS.Play();
                interrupt = true;
            }
            transform.LookAt(_myEntity.currentTarget.transform);
            yield return null;
        }

        _myEntity.ExitAnimation();
        actionCoroutine = null;
    }

    private IEnumerator TimerParticles(GameObject particle, float timeToDisable)
    {
        particle.SetActive(true);
        yield return new WaitForSeconds(timeToDisable);
        particle.SetActive(false);
    }

    
}
