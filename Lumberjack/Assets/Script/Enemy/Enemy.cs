using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    protected Rigidbody rigid;
    protected Animator anim;
    protected Material mat;
    protected NavMeshAgent nav;

    protected int maxHealth;
    protected int currentHealth;
    protected int attackPoint;

    public Transform target;

    public void Init()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        mat = GetComponent<MeshRenderer>().material;    
    }

    public void ChaseTarget()
    {
        nav.SetDestination(target.position);
    }

    protected void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
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
