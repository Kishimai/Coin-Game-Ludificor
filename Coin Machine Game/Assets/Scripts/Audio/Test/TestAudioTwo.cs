using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioTwo : MonoBehaviour
{
    public List<AudioClip> coinSounds = new List<AudioClip>();
    private AudioClip chosenSound;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        chosenSound = coinSounds[Random.Range(0, coinSounds.Count)];

        source.clip = chosenSound;

        source.Play();
    }
}
