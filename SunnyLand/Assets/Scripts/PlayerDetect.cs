using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    [SerializeField] float collisionRadius = 0.5f;    
    [SerializeField] private Vector2 offset = Vector2.zero;

    public LayerMask playerLayer;
    private Enemy enemy;

    private void Start()
    {           
        enemy = GetComponent<Enemy>();        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle((Vector2)transform.position + offset, collisionRadius, playerLayer);

        if (hit != null)
        {
            if (hit.CompareTag("Player"))
            {
                Player player = hit.GetComponent<Player>();

                if (!player.invulnerable && !enemy.invunerable)
                {
                    if (player.isFalling && (player.transform.position.y > transform.position.y))
                    {
                        enemy.TakeDamage(1);
                        player.jump = true;
                    }
                    else
                    {
                        player.TakeDamage(1);
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, collisionRadius);

    }
}
