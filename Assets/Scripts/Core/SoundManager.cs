using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audio;
    
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound) {
        audio.PlayOneShot(sound);
    }
}
