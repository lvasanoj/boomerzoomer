using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    //Audiostuff
    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip walkingSound;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("MusicManager").GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound, 1);
        }
    }
    public void PlayWalkingSound()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(walkingSound, 1f);
    }
    public void PlayDeathSound()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(deathSound, 1f);
    }
}
