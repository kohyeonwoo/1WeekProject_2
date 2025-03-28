using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public GameObject attackObject1;
    public GameObject attackObject2;
    public GameObject attackObject3;

    public GameObject playerUI;
    public Slider healthBar;
    
    public int maxHealth;
    public int currentHealth;

    public float speed;

    public TrailRenderer trailRenderer;

    private float hAxis;
    private float vAxis;

    private bool walkDown;
    private bool bAvoid;
    public bool bMove;

    private Vector3 moveVector;
    private Vector3 avoidVector;

    private Animator anim;
    private Rigidbody rigid;

    public SkinnedMeshRenderer[] meshes;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();

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

        if (Input.GetMouseButtonDown(1))
        {
            Attack2();
        }

        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            Attack3();
        }    

        if (Input.GetButtonDown("Avoid"))
        {
            Avoid();
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

        if(bAvoid)
        {
            moveVector = avoidVector;
        }

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

    private void Attack2()
    {
        anim.SetTrigger("Attack2");
    }

    private void Attack3()
    {
        anim.SetTrigger("Attack3");
    }

    private void Avoid()
    {
        if(bAvoid && moveVector != Vector3.zero && !bAvoid)
        {
            avoidVector = moveVector;
            speed *= 2;
            anim.SetTrigger("Avoid");
            bAvoid = true;

           Invoke("AvoidEnd", 0.4f);
        }
    }

    private void AvoidEnd()
    {
        speed *= 0.5f;
        bAvoid = false;
    }

    public void PlayFootStepSound()
    {
        AudioManager.Instance.PlaySFX("PlayerFootStepSound");
    }

    public void ActiveAttackCollision1()
    {
        attackObject1.SetActive(true);
    }

    public void DeActiveAttackCollision1()
    {
        attackObject1.SetActive(false);
    }

    public void ActiveAttackCollision2()
    {
        attackObject2.SetActive(true);
    }

    public void DeActiveAttackCollision2()
    {
        attackObject2.SetActive(false);
    }

    public void ActiveAttackCollision3()
    {
        attackObject3.SetActive(true);
    }

    public void DeActiveAttackCollision3()
    {
        attackObject3.SetActive(false);
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
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);

        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            mesh.material.color = Color.black;
        }
    }

    public void Damage(int Damage)
    {
        currentHealth -= Damage;
        healthBar.value = currentHealth;
        AudioManager.Instance.PlaySFX("PlayerHitSound");
        StartCoroutine(ChangeColor());

        if (currentHealth <= 0)
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
