using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public GameObject attackCollision;
    public GameObject playerUI;
    public Slider healthBar; 

    public int maxHealth;
    public int currentHealth;

    public float speed;

    public TrailRenderer trailRenderer;

    private float hAxis;
    private float vAxis;

    private bool walkDown;
    public bool bMove;

    private Vector3 moveVector;

    private Animator anim;
    private Rigidbody rigid;

    public Material mat;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;

        maxHealth = 50;
        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        bMove = true;
    }

    private void Update()
    {
        
        if(bMove)
        {
            InputKey();
            Move();
            Turn();
        }
         
        if (Input.GetMouseButtonDown(0))
        {
            Attack1();
        }
    }

    private void FixedUpdate()
    {
        FreezeRotation();
    }

    private void InputKey()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        walkDown = Input.GetButton("Walk");
    }

    private void Move()
    {
        moveVector = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVector * speed * (walkDown ? 0.3f : 1.0f) * Time.deltaTime;

        //if(walkDown)
        //{
        //    this.transform.position += moveVector * speed * 0.3f * Time.deltaTime;
        //}
        //else
        //{
        //    transform.position += moveVector * speed * Time.deltaTime;
        //} //--> if 구문 사용 방법

        anim.SetBool("bRun", moveVector != Vector3.zero);
        anim.SetBool("bWalk", walkDown);

    }
    private void Turn()
    {
        transform.LookAt(this.transform.position + moveVector);
    }
   
    private void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    private void Attack1()
    {
        anim.SetTrigger("Attack1");
    }

    public void PlayFootStepSound()
    {
        AudioManager.Instance.PlaySFX("PlayerFootStepSound");
    }

    public void ActiveAttackCollision()
    {
        attackCollision.SetActive(true);
        trailRenderer.enabled = true;
        AudioManager.Instance.PlaySFX("PlayerAttackSound");
        bMove = false;
    }

    public void DeActiveAttackCollision()
    {
        attackCollision.SetActive(false);
        trailRenderer.enabled = false;
    }

    public void BMoveTrue()
    {
        bMove = true;
    }

    public void BMoveFalse()
    {
        bMove = false;
    }

    IEnumerator ChangeColor()
    {
        mat.color = Color.red;

        yield return new WaitForSeconds(1.0f);

        mat.color = Color.white;
    }

    public void Damage(int Damage)
    {
        currentHealth -= Damage;
        healthBar.value = currentHealth;
        AudioManager.Instance.PlaySFX("PlayerHitSound");
        StartCoroutine(ChangeColor());

        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
        playerUI.SetActive(false);
        GameManager.Instance.ActiveGameOverPanel();
    }

}
