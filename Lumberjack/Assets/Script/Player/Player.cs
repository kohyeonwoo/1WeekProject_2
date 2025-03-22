using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public GameObject attackCollision;

    public int maxHealth;
    public int currentHealth;

    private Animator anim;
    private Rigidbody rigid;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack1();
        }
    }

    private void Attack1()
    {
        anim.SetTrigger("Attack1");
    }

    public void ActiveAttackCollision()
    {
        attackCollision.SetActive(true);
    }

    public void DeActiveAttackCollision()
    {
        attackCollision.SetActive(false);
    }

    public void Damage(int Damage)
    {
        currentHealth -= Damage;

        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }

}
