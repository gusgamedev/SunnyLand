using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : Enemy
{
    [Header("Plant Properties")]
    public float timeNextAttack = 2f;

    private bool canAttack = true;
    public float playerDistance = 0f;
    private Transform target; 
    private Animator anim; 
   
    // Update is called once per frame

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        
        playerDistance = (target.position - transform.position).magnitude;

        if (playerDistance < 3f && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {       
        canAttack = false;
        anim.SetTrigger("Attack");        
        Invoke("SetCanAttack", timeNextAttack);


    }



    private void SetCanAttack()
    {
        canAttack = true;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

}
