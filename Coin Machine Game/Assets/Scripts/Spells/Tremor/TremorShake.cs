using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TremorShake : MonoBehaviour
{
    public GameObject theDirt;
    public GameObject dirtFriends;

    public float tremorPower;
    public float tremorDuration;

    public float sinkDuration;

    private bool active;
    private float remainingDuration;

    public void Update()
    {
        if (remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;
        }
        else
        {
            active = false;
        }
    }

    public void Shake(Vector3 spotOfDirt)
    {
        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("coin");

        StartCoroutine(DirtyDirtThatsDirtingDirty(spotOfDirt));

        foreach (GameObject coin in allCoins)
        {
            if (coin.activeInHierarchy)
            {
                coin.GetComponentInParent<CoinLogic>().TremorEvent(tremorDuration, tremorPower);
            }
        }

        active = true;
        remainingDuration += tremorDuration;

    }

    public IEnumerator DirtyDirtThatsDirtingDirty(Vector3 theSpot)
    {

        StartCoroutine(DirtPiles());

        float duration = tremorDuration;

        float elapsedTime = 0;

        theDirt.SetActive(true);

        Vector3 startSpot = new Vector3(theSpot.x, 1, theSpot.z);
        Vector3 endSpot = new Vector3(theSpot.x, -1, theSpot.z);

        GameObject dirt = Instantiate(theDirt, theSpot, Quaternion.identity);

        while (elapsedTime < sinkDuration)
        {
            duration -= Time.deltaTime;

            elapsedTime += Time.deltaTime;

            Vector3 relativeStart = Vector3.zero;

            float fractComplete = elapsedTime / sinkDuration;

            dirt.transform.position = Vector3.Lerp(startSpot, endSpot, fractComplete);

            yield return new WaitForFixedUpdate();
        }

        Destroy(dirt);
    }

    public IEnumerator DirtPiles()
    {
        Vector3 dirtPilesStart = Vector3.zero;
        Vector3 dirtPilesEnd = new Vector3(0, -2, 0);

        dirtFriends.SetActive(true);

        float elapsedTime = 0;

        float travelDuration = tremorDuration / 4;
        float idleDuration = tremorDuration / 2;

        dirtFriends.transform.position = dirtPilesEnd;

        while (elapsedTime < travelDuration)
        {
            elapsedTime += Time.deltaTime;

            float fractComplete = elapsedTime / travelDuration;

            dirtFriends.transform.position = Vector3.Lerp(dirtPilesEnd, dirtPilesStart, fractComplete);

            yield return new WaitForFixedUpdate();
        }

        elapsedTime = 0;

        while (elapsedTime < idleDuration)
        {
            elapsedTime += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        elapsedTime = 0;

        while (elapsedTime < travelDuration)
        {
            elapsedTime += Time.deltaTime;

            float fractComplete = elapsedTime / travelDuration;

            dirtFriends.transform.position = Vector3.Lerp(dirtPilesStart, dirtPilesEnd, fractComplete);

            yield return new WaitForFixedUpdate();
        }

        dirtFriends.SetActive(false);
    }
}
