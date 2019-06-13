using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceOfSound : MonoBehaviour
{

    public AudioClip[] boomclips;
    private AudioSource randomSound;

    void Start()
    {
        randomSound = gameObject.GetComponent<AudioSource>();
        randomSound.clip = boomclips[Random.Range(0, boomclips.Length)];
        randomSound.Play();
        Destroy(gameObject, randomSound.clip.length);
    }
}