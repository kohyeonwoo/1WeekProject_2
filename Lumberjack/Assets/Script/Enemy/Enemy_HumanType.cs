using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy_HumanType : MonoBehaviour, IDamageable
{

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

    public GameObject character;
    public GameObject regdoll;

    public Rigidbody spine;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;

        currentHealth = maxHealth;

        Invoke("ChaseStart", 2.0f);
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        regdoll.transform.position = character.transform.position;
        regdoll.transform.rotation = character.transform.rotation;

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
        float targetRadius = 1.5f;
        float targetRange = 3.0f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
            targetRadius,
            transform.forward,
            targetRange,
            LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !bAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        bChase = false;
        bAttack = true;
        anim.SetBool("bAttack", true);

        enemyAttackCollision.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyAttackCollision.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        bChase = true;
        bAttack = false;
        anim.SetBool("bAttack", false);
    }

    IEnumerator ChangeColor()
    {
        mat.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        mat.color = Color.white;
    }

    public void Damage(int Damage)
    {
        currentHealth -= Damage;
        AudioManager.Instance.PlaySFX("EnemyHitSound");
        StartCoroutine(ChangeColor());

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        GameManager.Instance.killCount++;
        bChase = false;
        nav.enabled = false;
        anim.SetTrigger("Die");
        Invoke("EraseBody", 2.0f);
        ChangeRagdoll();
    }

    public void ChangeRagdoll()
    {
        CopyCharacterTransformToRagdoll(character.transform, regdoll.transform);

        character.SetActive(false);
        regdoll.SetActive(true);

        spine.AddForce(Vector3.back * 50, ForceMode.Impulse);
    }

    private void CopyCharacterTransformToRagdoll(Transform origin, Transform ragdoll)
    {
        for (int i = 0; i < origin.childCount; i++)
        {
            if (origin.childCount != 0)
            {
                CopyCharacterTransformToRagdoll(origin.GetChild(i), ragdoll.GetChild(i));
            }

            ragdoll.GetChild(i).localPosition = origin.GetChild(i).localPosition;
            ragdoll.GetChild(i).localRotation = origin.GetChild(i).localRotation;
        }
    }

    private void EraseBody()
    {
        this.gameObject.SetActive(false);
    }
    
}
