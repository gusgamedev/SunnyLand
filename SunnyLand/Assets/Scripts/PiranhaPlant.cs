using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : Enemy
{
    [Header("Plant Properties")]
    public float timeNextAttack = 0.8f;

    private bool canAttack = true;
    public float playerDistance = 0f;
    private Transform target; 
    private Animator anim; 
    private AudioSource attackFx; 
   
    // Update is called once per frame

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        attackFx = GetComponent<AudioSource>();
    }
    void Update()
    {
        playerDistance = (target.position - transform.position).magnitude;

        if ((transform.position.x > target.position.x && facingRight) || (transform.position.x < target.position.x && !facingRight)) 
        {
            Flip();        
        } 

        if (playerDistance < 3f && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {       
        canAttack = false;
        attackFx.Play();
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
        transform.Rotate(0,180,0);

    }

}
