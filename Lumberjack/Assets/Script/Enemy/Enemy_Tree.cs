using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Tree : Enemy
{
    private void Start()
    {
         Init();   
    }

    //private void Update()
    //{
    //    ChaseTarget();
    //}

    private void FixedUpdate()
    {
        FreezeVelocity();
    }
}
