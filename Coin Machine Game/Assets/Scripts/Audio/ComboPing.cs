using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ComboPing : MonoBehaviour
{
    private float timeUntilDeletion;
    public List<AudioClip> pings = new List<AudioClip>();
    public void PlayAudio(int index)
    {
        AudioClip audioClip;

        audioClip = pings[index];

        gameObject.GetComponent<AudioSource>().clip = audioClip;

        gameObject.GetComponent<AudioSource>().Play();

        timeUntilDeletion = gameObject.GetComponent<AudioSource>().clip.length;

        timeUntilDeletion += 0.25f;

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        while (timeUntilDeletion > 0)
        {
            timeUntilDeletion -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
