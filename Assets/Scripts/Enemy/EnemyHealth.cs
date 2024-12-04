using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] public CapsuleCollider bodyCollider;
    [SerializeField] public SphereCollider headCollider;
    [SerializeField] float headshotMultiplier = 2f;

    bool isDead = false;

    private void Start()
    {
        if (bodyCollider == null)
        {
            bodyCollider = GetComponent<CapsuleCollider>();
        }
        if (headCollider == null)
        {
            headCollider = GetComponent<SphereCollider>();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    public void TakeHeadshot(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage * headshotMultiplier;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");

        if (bodyCollider != null && headCollider != null)
        {
            bodyCollider.enabled = false;
            headCollider.enabled = false;
        }
    }
}
