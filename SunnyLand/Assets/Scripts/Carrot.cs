using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.gm.SetPlayerHealth(GameManager.gm.GetPlayerHealth() + 1);
            AudioManager.am.PlayFx(0, AudioManager.am.audioClips[3]);
            Destroy(gameObject);
        }
    }
}
