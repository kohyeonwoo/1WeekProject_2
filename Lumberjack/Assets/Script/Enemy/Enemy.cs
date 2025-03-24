using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum EnemyType { Basic, Dash, LongDistance}

public class Enemy : MonoBehaviour, IDamageable
{

    public EnemyType enemyType;

    private Rigidbody rigid;
    private Animator anim;
    private Material mat;
    private NavMeshAgent nav;
   
    public int maxHealth;
    public int currentHealth;
    public int attackPoint;

    public bool bChase;
    public bool bAttack;

    public Transform target;
    public GameObject enemyAttackCollision;
    public GameObject bullet;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        //enemyAttackCollision = GetComponent<BoxCollider>();
        // mat = GetComponentInChildren<MeshRenderer>().material;    

        Invoke("ChaseStart", 2.0f);
    }

    private void Update()
    {
        ChaseTarget();
    }

    private void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    public void ChaseStart()
    {
        bChase = true;
        anim.SetBool("bMove", true);
    }

    public void ChaseTarget()
    {
        if(nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !bChase;
        }  
    }

    protected void FreezeVelocity()
    {
        if (bChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }   
    }

    protected void Targeting()
    {
        float targetRadius = 0.0f;
        float targetRange = 0.0f;

        switch(enemyType)
        {
            case EnemyType.Basic:
                targetRadius = 1.5f;
                targetRange = 3.0f;
                break;
            case EnemyType.Dash:
                targetRadius = 1.0f;
                targetRange = 12.0f;
                break;
            case EnemyType.LongDistance:
                targetRadius = 0.5f;
                targetRange = 25.0f;
                break;
        }

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius,
            transform.forward,
            targetRange,
            LayerMask.GetMask("Player"));

        if(rayHits.Length > 0 && !bAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        bChase = false;
        bAttack = true;
        anim.SetBool("bAttack", true);

        switch(enemyType)
        {
            case EnemyType.Basic:

                yield return new WaitForSeconds(0.2f);
                enemyAttackCollision.SetActive(true);

                yield return new WaitForSeconds(1.0f);
                enemyAttackCollision.SetActive(false);

                yield return new WaitForSeconds(1.0f);

                break;

            case EnemyType.Dash:

                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(this.transform.forward * 20, ForceMode.Impulse);
                enemyAttackCollision.SetActive(true);

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                enemyAttackCollision.SetActive(false);

                yield return new WaitForSeconds(2.0f);
                break;

            case EnemyType.LongDistance:

                yield return new WaitForSeconds(0.5f);
                //총알 생성 부분 

                break;
        }

      

        bChase = true;
        bAttack = false;
        anim.SetBool("bAttack", false);
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
        bChase = false;
        nav.enabled = false;
        anim.SetTrigger("Die");
        Invoke("EraseBody", 2.0f);
        //this.gameObject.SetActive(false);
    }

    private void EraseBody()
    {
        this.gameObject.SetActive(false);
    }
}
