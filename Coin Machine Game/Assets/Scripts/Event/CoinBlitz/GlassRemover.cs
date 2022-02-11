using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRemover : MonoBehaviour
{
    // Object which swipes across glass panel and makes particles
    public GameObject animatedObject;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public Vector3 glassPanelStartPosition;
    public Vector3 glassPanelEndPosition;

    private float journeyLength;

    public float travelSpeed;

    public ParticleSystem glassEffect;

    public GameObject glassObject;

    private GameObject eventManager;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
    }

    // MAKE PEGS RECEDE INTO MACHINE WHEN BLITZ HAPPENS

    public IEnumerator RemoveGlass()
    {
        eventManager.GetComponent<EventsManager>().animationFinished = false;

        animatedObject.transform.localPosition = startPosition;

        //animatedObject.SetActive(true);

        glassEffect.Play(true);

        float startTime = Time.time;

        journeyLength = Vector3.Distance(startPosition, endPosition);

        float timeSpent = 0;

        while (true)
        {
            timeSpent += Time.deltaTime;

            float distanceCovered = (Time.time - startTime) * travelSpeed;

            float fractionOfJourney = distanceCovered / journeyLength;

            animatedObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            glassObject.transform.localPosition = Vector3.Lerp(glassPanelStartPosition, glassPanelEndPosition, fractionOfJourney);

            if (Mathf.Approximately(animatedObject.transform.localPosition.z, endPosition.z))
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //glassObject.SetActive(false);

        eventManager.GetComponent<EventsManager>().animationFinished = true;
    }

    public IEnumerator RebuildGlass()
    {
        eventManager.GetComponent<EventsManager>().animationFinished = false;

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

            glassObject.transform.localPosition = Vector3.Lerp(glassPanelEndPosition, glassPanelStartPosition, fractionOfJourney);

            if (Mathf.Approximately(animatedObject.transform.localPosition.z, startPosition.z))
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        //glassObject.SetActive(true);

        eventManager.GetComponent<EventsManager>().animationFinished = true;
    }
}
