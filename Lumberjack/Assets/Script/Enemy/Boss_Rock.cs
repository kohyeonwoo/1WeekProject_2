using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rock : EnemyAttackCollision
{

    private Rigidbody rigid;

    private float angularPower = 2.0f;
    private float scaleValue = 0.1f;
    private bool isShoot;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {

        while(!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f;
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        }

        
    }

}
