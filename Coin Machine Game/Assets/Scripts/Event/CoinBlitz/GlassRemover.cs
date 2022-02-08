using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRemover : MonoBehaviour
{
    // Object which swipes across glass panel and makes particles
    public GameObject animatedObject;

    public Vector3 startPosition;
    public Vector3 endPosition;

    private float journeyLength;

    public float travelSpeed;

    public ParticleSystem glassEffect;

    // Start is called before the first frame update
    void Start()
    {
        // start particle effects
    }

    public IEnumerator RemoveGlass()
    {
        animatedObject.transform.localPosition = startPosition;

        //animatedObject.SetActive(true);

        glassEffect.Play(true);

        float startTime = Time.time;

        journeyLength = Vector3.Distance(startPosition, endPosition);

        float timeSpent = 0;

        while (true)
        {
            timeSpent += Time.deltaTime;

            Debug.Log(timeSpent);

            float distanceCovered = (Time.time - startTime) * travelSpeed;

            float fractionOfJourney = distanceCovered / journeyLength;

            animatedObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            if (Mathf.Approximately(animatedObject.transform.localPosition.z, endPosition.z))
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //animatedObject.SetActive(false);
    }

    public IEnumerator RebuildGlass()
    {
        animatedObject.transform.localPosition = endPosition;

        //animatedObject.SetActive(true);

        glassEffect.Play(true);

        float startTime = Time.time;

        journeyLength = Vector3.Distance(startPosition, endPosition);

        while (true)
        {
            float distanceCovered = (Time.time - startTime) * travelSpeed;

            float fractionOfJourney = distanceCovered / journeyLength;

            animatedObject.transform.localPosition = Vector3.Lerp(endPosition, startPosition, fractionOfJourney);

            if (Mathf.Approximately(animatedObject.transform.localPosition.z, startPosition.z))
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //animatedObject.SetActive(false);
    }
}
