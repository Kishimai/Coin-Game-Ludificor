using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioThree : MonoBehaviour
{
    public List<AudioClip> coinSounds = new List<AudioClip>();
    private AudioClip chosenSound;
    private AudioSource source;

    private float timeLeft;

    public void PlayAudio()
    {
        source = GetComponent<AudioSource>();

        chosenSound = coinSounds[Random.Range(0, coinSounds.Count)];

        source.clip = chosenSound;

        timeLeft = source.clip.length;

        source.Play();

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
