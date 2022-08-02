using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixSpell : AttackScript
{
    [Header("GameObjects")]
    [SerializeField] private GameObject castPhoenixEffect;

    [SerializeField] private BulletSpawner _phoenixSpawner;
    [HideInInspector] public GameObject currentTarget;


    [Header("Audio Clips")]
    [SerializeField] private AudioClip castPhoenixClip;
    [SerializeField] private AudioClip launchPhoenixClip;

    private AudioSource phoenixCastAS;
    private AudioSource phoenixLaunchAS;
    private Entity _myEntity;

    [Header("Misc")]
    [SerializeField] private float castingTime;
    [SerializeField] private string animatorCastName = "Teleport";
    [SerializeField] private string animatorLaunchName = "FireBalls";

    
    private Coroutine actionCoroutine;

    private void Start()
    {
        currentTarget = gameObject;
        phoenixLaunchAS = gameObject.AddComponent<AudioSource>();
        phoenixCastAS = gameObject.AddComponent<AudioSource>();
        phoenixLaunchAS.clip = launchPhoenixClip;
        phoenixCastAS.clip = castPhoenixClip;

        _myEntity = GetComponentInParent<Entity>();
    }

    private void Update()
    {
        if (actionCoroutine != null) _myEntity.transform.LookAt(_myEntity.currentTarget.transform);
    }
    public override void StartAttack()
    {
        EndAttack();
    }

    public override void EndAttack()
    {
        if (actionCoroutine != null) return;

        actionCoroutine = StartCoroutine(Phoenix());
    }

    private IEnumerator Phoenix()
    {
        castPhoenixEffect.SetActive(true);
        phoenixCastAS.Play();
        yield return new WaitForSeconds(castingTime);
        castPhoenixEffect.SetActive(false);

        GetComponentInParent<Animator>().SetTrigger(animatorLaunchName);
        yield return new WaitForSeconds(0.15f);
        phoenixCastAS.Stop();
        phoenixLaunchAS.clip = launchPhoenixClip;
        phoenixLaunchAS.Play();

        if (_phoenixSpawner != null) _phoenixSpawner.ShootMethod();

        _myEntity.GetComponent<Enemy_Shaman>().ExitAnimation();
        actionCoroutine = null;
    }
}
