
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    [SerializeField] private Bullet _bullet;
    [SerializeField] private int _spawnQuantity = 3;
    private ObjectPool<Bullet> _objPool;
    [HideInInspector] public float damage = 10;
    public bool fromBoss = false;


    private void Awake()
    {
        _objPool = new ObjectPool<Bullet>(BulletReturn, Bullet.TurnOn, Bullet.TurnOff, _spawnQuantity);
    }

    public void ShootMethod()
    {
        var b = _objPool.GetObject();
        b.GetComponent<Bullet>().myCanastiten = _objPool;
        b.GetComponent<Bullet>().mySpawner = this;
        if (PlayerPrefs.GetInt("PowerUp") == 1 && fromBoss == false)
        {
            b.GetComponent<Bullet>().powerUpEffect.SetActive(true);
            b.GetComponent<Bullet>().damage = damage * 2;
        }
        
        b.transform.position = transform.position;
        b.transform.forward = transform.forward;
    }

    public Bullet BulletReturn()
    {
        return Instantiate(_bullet);
    }
}
