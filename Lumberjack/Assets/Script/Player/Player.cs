using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public GameObject attackCollision;

    public int maxHealth;
    public int currentHealth;

    public float speed;

    private float hAxis;
    private float vAxis;

    private bool walkDown;

    private Vector3 moveVector;

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

        InputKey();
        Move();
        Turn();
      
        if (Input.GetMouseButtonDown(0))
        {
            Attack1();
        }
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
