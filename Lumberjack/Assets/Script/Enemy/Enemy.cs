using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    protected int maxHealth;
    protected int currentHealth;
    protected int attackPoint;

    public void Init(int Health, int AttackPoint)
    {
        maxHealth = Health;
        currentHealth = maxHealth;
        attackPoint = AttackPoint;
    }

    public void Damage(int Damage)
    {
        currentHealth -= Damage;

        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        this.gameObject.SetActive(false);
    }
}
