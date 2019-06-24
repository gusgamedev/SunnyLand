using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {           
            GameManager.gm.SetStars(GameManager.gm.GetStars() + 1);
            AudioManager.am.PlayFx(0, AudioManager.am.audioClips[4]);
            Destroy(gameObject);
        }
    }
}
