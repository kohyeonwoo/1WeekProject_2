using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerSpawnMonster : MonoBehaviour, IDamageable
{

    public Rigidbody rigid;
    public Animator anim;
    public SkinnedMeshRenderer[] meshes;
    public NavMeshAgent nav;

    public Transform target;

    public int maxHealth;
    public int currentHealth;
    
    public bool bChase;
    public bool bAttack;

    public GameObject playerAttackObject1;
    public GameObject playerAttackObject2;
    public GameObject playerAttackObject3;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();

        currentHealth = maxHealth;

        Invoke("ChaseStart", 1.0f);
     
    }

    private void Update()
    {
        ChaseTarget();
    }

    private void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;

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
        if (nav.enabled)
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
        float targetRadius = 1.0f;
        float targetRange = 1.5f;

       
            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
                targetRadius,
                transform.forward,
                targetRange,
                LayerMask.GetMask("Enemy"));

            if (rayHits.Length > 0 && !bAttack)
            {
                 Attack();
            }
     
    }

    private void Attack()
    {
        bChase = false;
        bAttack = true;
        anim.SetBool("bAttack", true);
    
    }

    public void AttackEnd()
    {

        bChase = true;
        bAttack = false;
        DeActiveAttackObject1();
        DeActiveAttackObject2();
        anim.SetBool("bAttack", false);
    }

    IEnumerator ChangeColor()
    {

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.white;
        }
    }

    public void ActiveAttackObject1()
    {
        playerAttackObject1.SetActive(true);
    }

    public void DeActiveAttackObject1()
    {
        playerAttackObject1.SetActive(false);
    }

    public void ActiveAttackObject2()
    {
        playerAttackObject2.SetActive(true);
    }

    public void DeActiveAttackObject2()
    {
        playerAttackObject2.SetActive(false);
    }

    public void ActiveAttackObject3()
    {
        playerAttackObject3.SetActive(true);
    }

    public void DeActiveAttackObject3()
    {
        playerAttackObject3.SetActive(false);
    }

    public void Damage(int Damage)
    {
        currentHealth -= Damage;
        StartCoroutine(ChangeColor());
        AudioManager.Instance.PlaySFX("PlayerHitSound");
       
        if (currentHealth <= 0)
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
    }

    private void EraseBody()
    {
        Destroy(this.gameObject);
    }

}
