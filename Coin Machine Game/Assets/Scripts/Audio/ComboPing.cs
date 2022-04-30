using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ComboPing : MonoBehaviour
{

    public List<AudioClip> pings = new List<AudioClip>();
    public void PlayAudio(int index)
    {
        AudioClip audioClip;

        audioClip = pings[index];

        gameObject.GetComponent<AudioSource>().clip = audioClip;

        gameObject.GetComponent<AudioSource>().Play();
    }
}
