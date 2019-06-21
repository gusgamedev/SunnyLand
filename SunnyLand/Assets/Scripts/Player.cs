using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterController2D
{
    [Header("Player Properties")]    
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
            
            GameManager.gm.SetPlayerHealth(GameManager.gm.GetPlayerHealth() - damage);
            flashDamage.SetFlashDamage();
            Invoke("SetInvulnerableFalse", 1f);
        }
    }

    private void SetInvulnerableFalse()
    {
        invulnerable = false;
    }

}
