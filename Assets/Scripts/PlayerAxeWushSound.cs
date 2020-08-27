using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeWushSound : MonoBehaviour
{   [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private AudioClip[] woosh_Sounds;
    // Update is called once per frame

    void PlayWooshSound()
    {
        AudioSource.clip = woosh_Sounds[Random.Range(0, woosh_Sounds.Length)];
        AudioSource.Play();
    }
    void Update()
    {
        
    }
}
