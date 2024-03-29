﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterController2D
{
    [Header("Player Properties")]    
    [HideInInspector]  public bool invulnerable = false;
       
    [Header("Particles")]
    public ParticleSystem dust;

    private FlashDamage flashDamage;    
        
    private void Start()
    {
        flashDamage = GetComponentInChildren<FlashDamage>();               
    }

    public override void Update()
    {
        if (isAlive)
        {
            if (isFalling && isOnFloor)
            {
                Invoke("Dust", 0.05f);                
            }

            if (Input.GetButtonDown("Jump") && isOnFloor)
                AudioManager.am.PlayFx(3, AudioManager.am.audioClips[2]);
        }

        base.Update();
        
    }

    private void Dust()
    {
        dust.Play();
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            AudioManager.am.PlayFx(3, AudioManager.am.audioClips[1]);
            GameManager.gm.SetPlayerHealth(GameManager.gm.GetPlayerHealth() - damage);
            flashDamage.SetFlashDamage();
            Invoke("SetInvulnerableFalse", 0.8f);

            if (GameManager.gm.GetPlayerHealth() <= 0)
                PlayerDie();
        }
    }

    void PlayerDie()
    {
        isAlive = false;
        jump = true;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }

    private void SetInvulnerableFalse()
    {
        invulnerable = false;
    }

    




}
