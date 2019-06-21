using UnityEngine;
using System.Collections;

public class Patrol : Enemy
{
    [Header("Collision")]
    public LayerMask layerCollision;
    public Transform collisonDetector;
    public float distance = 1f;
       
    
    private bool canMove = true;
    // Use this for initialization

    private void Awake()
    {
        if (!facingRight)
            speed = -speed;
    }

    // Update is called once per frame
    void Update()
    {
        bool wallHit = Physics2D.Raycast(collisonDetector.position, Vector2.left, distance, layerCollision);
        bool groundHit = Physics2D.Raycast(collisonDetector.position, Vector2.down, distance, layerCollision);

        if (wallHit || !groundHit)
            Flip();
    }

    private void FixedUpdate()
    {      
        if (canMove)
            rb.velocity = new Vector2(speed, rb.velocity.y);

    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed = -speed;
    }

    public void Move()
    {
        canMove = true;
    }

    public void Stop()
    {
        canMove = false;
    }
}
