﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float initialCooldown;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject explosion;
    [SerializeField] private BoxCollider hitBox;
    [SerializeField] private float durationEffect;
    [SerializeField] private GameObject trapBadEffect;
    [SerializeField] private GameObject trapGoodEffect;
    private void Start()
    {

        hitBox = GetComponentInChildren<BoxCollider>();
        StartCoroutine(Timer(true));
    }

    private void EnableTrap()
    {
        StartCoroutine(Timer(false));
    }

    private IEnumerator Timer(bool firstTime)
    {
        if(firstTime) yield return new WaitForSeconds(initialCooldown);

        hitBox.enabled = true;
        if (trapBadEffect != null) trapBadEffect.SetActive(true);
        if (trapGoodEffect != null) trapGoodEffect.SetActive(false);
        yield return new WaitForSeconds(durationEffect);
        hitBox.enabled = false;
        if (trapBadEffect != null) trapBadEffect.SetActive(false);
        if (trapGoodEffect != null) trapGoodEffect.SetActive(true);
        yield return new WaitForSeconds(cooldown);
        EnableTrap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Entity>() != null)
        {
            other.GetComponent<Entity>().TakeDamage(damage);
            Instantiate(explosion, other.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
