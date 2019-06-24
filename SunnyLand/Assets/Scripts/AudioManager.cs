using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    
    public AudioSource[] audioSources;
    public AudioClip[] audioClips;


    void Awake()
    {
        if (am == null)
            am = this;
        else if (am != null)
            Destroy(gameObject);
    }

    

    public void PlayFx(int index, AudioClip clip)
    {
        audioSources[index].clip = clip;
        audioSources[index].Play();
    }

}
