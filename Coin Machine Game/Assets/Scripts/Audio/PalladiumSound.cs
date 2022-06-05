using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalladiumSound : MonoBehaviour
{
    private float timeUntilDeletion;
    public List<AudioClip> clips = new List<AudioClip>();

    public void PlayAudio()
    {
        AudioClip audioClip;

        audioClip = clips[Random.Range(0, clips.Count)];

        gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);

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
