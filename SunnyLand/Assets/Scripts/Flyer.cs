using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : Enemy
{
    [Header("Flayer Properties")]
    public Transform pointA;
    public Transform pointB;
    public bool lookAtPlayer = false;

    private Transform nextPoint;
    private Transform target = null;

    private void Awake()
    {  
        nextPoint = pointA;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if (pointA.localPosition.x > pointB.localPosition.x)
            Flip();
    }

    // Update is called once per frame
    void Update() 
    {

        MoveEnemy();

        if (lookAtPlayer)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            if (direction.x < 0 && facingRight || direction.x > 0 && !facingRight)
                Flip();
        }

    }

    private void MoveEnemy()
    {
        if (pointA.localPosition == transform.localPosition)
        {
            nextPoint = pointB;
            if (!lookAtPlayer)
                Flip();
        }

        if (pointB.localPosition == transform.localPosition)
        {
            nextPoint = pointA;
            if (!lookAtPlayer)
                Flip();
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, nextPoint.localPosition, speed * Time.deltaTime);

        

    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
    }
}
