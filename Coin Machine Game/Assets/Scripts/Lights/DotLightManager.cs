using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DotLightManager : MonoBehaviour
{

    public GameObject rightLights;
    public GameObject leftLights;

    public GameObject[] allLights;

    public List<GameObject> rightRail;
    public List<GameObject> leftRail;
    
    public float frenzyEffectDuration;

    public int maxNumLightsPerRail;

    public int defaultNumberOfFlashes;
    public float flashOnTime;

    // Start is called before the first frame update
    void Start()
    {
        CompileLights();

        Idle();
    }

    private void CompileLights()
    {
        // All Lights //
        allLights = GameObject.FindGameObjectsWithTag("dot_light");

        // Back Lights //
        foreach (Transform child in rightLights.transform)
        {
            rightRail.Add(child.gameObject);
        }

        foreach(Transform child in leftLights.transform)
        {
            leftRail.Add(child.gameObject);
        }
    }

    public void Idle()
    {
        // All lights assume their idle colors and materials
        // Can make it so it changes every time idle is called if desired

        StopAllCoroutines();

        foreach (GameObject light in allLights)
        {
            light.GetComponent<DotLight>().IdleAppearance();
        }

    }

    public IEnumerator Flash(int inputNumFlashes = 0)
    {
        // All lights assume a single color and turn off and on a set number of times with a set duration of off and on time

        int flashCounter = 0;
        int numFlashes = 0;

        if (inputNumFlashes == 0)
        {
            numFlashes = defaultNumberOfFlashes;
        }
        else
        {
            numFlashes = inputNumFlashes;
        }

        while (flashCounter < numFlashes)
        {
            ++flashCounter;

            foreach (GameObject light in allLights)
            {
                light.GetComponent<DotLight>().Flash();
                //Debug.Log("Flashing Light");
            }

            yield return new WaitForSeconds(flashOnTime);
        }

        Idle();

    }

    public IEnumerator Frenzy(float duration)
    {
        // All lights flash on and off at random intervals

        // Randomly choose a light every set duration to invert the state of (off -> on, on -> off)

        float timeLeft = duration;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            GameObject chosenLight = allLights[Random.Range(0, allLights.Length)];

            chosenLight.GetComponent<DotLight>().Frenzy();

            yield return new WaitForFixedUpdate();
        }

        Idle();
    }

    public IEnumerator Scroll(float duration)
    {

        float timeLeft = duration;
        int index = 0;

        GameObject rightLight;
        GameObject leftLight;

        foreach (GameObject light in allLights)
        {
            light.GetComponent<DotLight>().IdleScroll();
        }

        while (timeLeft > 0)
        {

            timeLeft -= Time.deltaTime;

            rightLight = rightRail.ElementAt(index);
            leftLight = leftRail.ElementAt(index);

            rightLight.GetComponent<DotLight>().Scroll();
            leftLight.GetComponent<DotLight>().Scroll();

            yield return new WaitForFixedUpdate();

            rightLight.GetComponent<DotLight>().Scroll();
            leftLight.GetComponent<DotLight>().Scroll();

            ++index;

            if (index >= maxNumLightsPerRail)
            {
                index = 0;
            }

        }

        Idle();

    }
}
