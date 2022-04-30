using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPing : MonoBehaviour
{
    public List<AudioClip> pings = new List<AudioClip>();
    public void PlayAudio()
    {
        AudioClip audioClip;

        audioClip = pings[Random.Range(0, pings.Count)];

        gameObject.GetComponent<AudioSource>().clip = audioClip;

        gameObject.GetComponent<AudioSource>().Play();
    }
}
