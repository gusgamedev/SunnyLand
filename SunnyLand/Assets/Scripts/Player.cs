using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterController2D
{
    [Header("Player Properties")]    
    [HideInInspector]  public bool invulnerable = false;

    [Header("Particles")]
    public ParticleSystem dust;

    private FlashDamage flashDamage;    

    private float walkSpeed;
    private float runSpeed;
    
    private void Start()
    {
        flashDamage = GetComponentInChildren<FlashDamage>();        
        walkSpeed = hSpeed;
        runSpeed = hSpeed + 3f; 
    }

    public override void Update()
    {
        if (isFalling && isOnFloor)
        {
            Invoke("Dust", 0.05f);
            AudioManager.am.PlayFx(3, AudioManager.am.audioClips[5]);
        }

        if (Input.GetButtonDown("Jump") && isOnFloor)
            AudioManager.am.PlayFx(3, AudioManager.am.audioClips[2]);

        if (Input.GetButton("Run"))
            hSpeed = runSpeed;
        else if (Input.GetButtonUp("Run"))
            hSpeed = walkSpeed;
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
            Invoke("SetInvulnerableFalse", 1f);
        }
    }

    private void SetInvulnerableFalse()
    {
        invulnerable = false;
    }

}
