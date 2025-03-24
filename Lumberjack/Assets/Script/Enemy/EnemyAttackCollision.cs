using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
   
    public int attackPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(attackPoint);
                Debug.Log("플레이어 때리기 성공!");
            }
        }
    }
}
