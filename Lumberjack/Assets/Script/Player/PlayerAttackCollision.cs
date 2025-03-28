using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicType { electric, explosion , large }

public class PlayerAttackCollision : MonoBehaviour
{
     public int attackPoint;

     public MagicType magicType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
            {
                if(magicType == MagicType.electric)
                {
                    attackPoint = 5;
                }

                if (magicType == MagicType.explosion)
                {
                    attackPoint = 10;
                }

                if (magicType == MagicType.large)
                {
                    attackPoint = 15;
                }

                damageable.Damage(attackPoint);
                Debug.Log("적이 맞았습니다");
            }
        }
    }

    
}
