using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactEffect;
    public float speed;
    public float maxDistance;
    public float damage = 10;
    [SerializeField] private bool fromEnemy = false;
    public ObjectPool<Bullet> myCanastiten;
    public int myEnemyLayer;

    private float _currentDistance;

    private void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        _currentDistance += speed * Time.deltaTime;

        if(_currentDistance >= maxDistance)
        {
            myCanastiten.ReturnObject(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == myEnemyLayer)
        {
            if (other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage);
                if(impactEffect != null) Instantiate(impactEffect, transform.position, Quaternion.identity);
                myCanastiten.ReturnObject(this);
            }
        }

        if (other.tag == "Limits")
        {
            if (impactEffect != null) Instantiate(impactEffect);
            myCanastiten.ReturnObject(this);
        }
    }

    public void PowerUp()
    {
        damage *= 2;
    }

    #region SetActive

    private void Reset()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.Reset();
    }
    public static void TurnOff(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    #endregion
}
