using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioClip coinSound;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = coinSound;
    }

    public void PlayAudio()
    {
        source.Play();
    }
}
