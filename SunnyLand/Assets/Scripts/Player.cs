using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterController2D
{
    [Header("Player Properties")]    
    public int health = 3;
    [HideInInspector]  public bool invulnerable = false;

    private FlashDamage flashDamage;

    private void Start()
    {
        flashDamage = GetComponentInChildren<FlashDamage>();
    }
    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            health -= damage;
            flashDamage.SetFlashDamage();
            Invoke("SetInvulnerableFalse", 1f);
        }
    }

    private void SetInvulnerableFalse()
    {
        invulnerable = false;
    }

}
