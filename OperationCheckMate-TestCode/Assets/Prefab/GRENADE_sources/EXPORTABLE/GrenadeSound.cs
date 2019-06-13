using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSound : MonoBehaviour {

	public AudioClip[] boomclips;
	private AudioSource randomSound;
	public float killFaster = 1f;
	
	void Start ()
	{
		randomSound = gameObject.GetComponent<AudioSource>();
		Sound();
	}
	
	void Sound ()
    {
        randomSound.clip = boomclips[Random.Range(0, boomclips.Length)];
		randomSound.Play();
		Destroy(gameObject, randomSound.clip.length - killFaster);
    }
}
