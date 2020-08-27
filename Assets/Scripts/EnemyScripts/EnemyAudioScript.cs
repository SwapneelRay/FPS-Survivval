using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioScript : MonoBehaviour
{
    
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip Scream_clip, die_clip;

    [SerializeField]
    private AudioClip[] attack_clip;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void Play_ScreamSound()
    {
        audioSource.clip = Scream_clip;
        audioSource.Play();
    }

    public void Play_AttackSound()
    {
        audioSource.clip = attack_clip[Random.Range(0, attack_clip.Length)];

        audioSource.Play();
    }

    public void Play_DeadSound()
    {
        audioSource.clip = die_clip;
        audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
