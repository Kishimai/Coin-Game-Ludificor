using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSurgeDrone : MonoBehaviour
{
    public AudioClip clip;
    //public float waitTime = 1.35f;
    public float waitTime = 0;

    public void PlayAudio()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        gameObject.GetComponent<AudioSource>().clip = clip;

        gameObject.GetComponent<AudioSource>().Play();
    }

    public void DestroySelf()
    {
        GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
    }
}
