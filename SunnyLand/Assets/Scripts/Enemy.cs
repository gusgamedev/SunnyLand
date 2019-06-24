using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    protected FlashDamage damageEffect;
    protected bool isVisible = false;
    protected Rigidbody2D rb;
    protected bool facingRight = false;

    [HideInInspector] public bool invunerable = false;


    [Header("Properties")]
    [SerializeField] protected int health = 3;
    

    [Header("Movement")]    
    [SerializeField] protected float speed = 4;

    [Header("Effects")]
    [SerializeField] protected GameObject explosion;


    // Start is called before the first frame update
    protected void Start()
    {
        damageEffect = GetComponentInChildren<FlashDamage>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) 
    {
        if (!invunerable)
        {
            health -= damage;
            damageEffect.SetFlashDamage();
            AudioManager.am.PlayFx(2, AudioManager.am.audioClips[1]);

            if (health <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                AudioManager.am.PlayFx(1, AudioManager.am.audioClips[0]);
            }

            Invoke("SetInvulnerableFalse", 1f);
        }
    }

    protected void SetInvulnerableFalse()
    {
        invunerable = false;
    }

    protected void OnBecameVisible()
    {
        isVisible = true;
    }

    protected void OnBecameInvisible()
    {
        isVisible = false;
    }

    

}
