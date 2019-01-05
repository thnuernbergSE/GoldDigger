using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip walkingSound;
    public static AudioClip attackSound;
    public static AudioClip bombSound;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
        walkingSound = Resources.Load<AudioClip>("walking");
        attackSound = Resources.Load<AudioClip>("attack");
        bombSound = Resources.Load<AudioClip>("bombExplosion");

        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "walking": audioSrc.PlayOneShot(walkingSound);
                break;
            case "attack": audioSrc.PlayOneShot(attackSound);
                break;
            case "bombExplosion": audioSrc.PlayOneShot(attackSound);
                break;
        }
    }
}
