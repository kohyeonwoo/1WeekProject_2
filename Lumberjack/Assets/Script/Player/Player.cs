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

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVector = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVector * speed * Time.deltaTime;

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
