using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] float collisionRadius = 0.5f;
    [SerializeField] Vector2 offset = Vector2.zero;
    public LayerMask playerLayer;


    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle((Vector2)transform.position + offset, collisionRadius, playerLayer);

        if (hit != null)
        {
            if (hit.CompareTag("Player"))
            {
                Player player = hit.GetComponent<Player>();

                if (!player.invulnerable)
                {
                    player.TakeDamage(1);
                    player.jump = true;
                        

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
