using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleDrone : MonoBehaviour
{
    public AudioClip clip;
    //public float waitTime = 1.35f;
    public float waitTime = 4f;

    public void PlayAudio()
    {
        StartCoroutine(Wait());
    }

    public void DestroySelf()
    {
        GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
    }

    private IEnumerator Wait()
    {
        gameObject.GetComponent<AudioSource>().clip = clip;

        gameObject.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(waitTime);

        DestroySelf();
    }
}
