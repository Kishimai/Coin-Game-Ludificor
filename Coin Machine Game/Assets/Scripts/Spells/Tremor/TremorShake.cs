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
    private float pilesDuration;

    public void Shake(Vector3 spotOfDirt)
    {
        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("coin");

        StartCoroutine(DirtyDirtThatsDirtingDirty(spotOfDirt));

        foreach (GameObject coin in allCoins)
        {
            StartCoroutine(coin.GetComponentInParent<CoinLogic>().Tremor(tremorDuration, tremorPower));
        }

    }

    public IEnumerator DirtyDirtThatsDirtingDirty(Vector3 theSpot)
    {
        float duration = tremorDuration;

        bool moveUp = true;

        float elapsedTime = 0;
        float secondElapsedTime = 0;

        theDirt.SetActive(true);
        dirtFriends.SetActive(true);

        Vector3 startSpot = new Vector3(theSpot.x, 1, theSpot.z);
        Vector3 endSpot = new Vector3(theSpot.x, -1, theSpot.z);

        Vector3 dirtPilesStart = Vector3.zero;
        Vector3 dirtPilesEnd = new Vector3(0, -2, 0);

        pilesDuration = sinkDuration / 4;

        GameObject dirt = Instantiate(theDirt, theSpot, Quaternion.identity);

        while (elapsedTime < sinkDuration)
        {
            duration -= Time.deltaTime;

            elapsedTime += Time.deltaTime;

            Vector3 relativeStart = Vector3.zero;

            float fractComplete = elapsedTime / sinkDuration;

            dirt.transform.position = Vector3.Lerp(startSpot, endSpot, fractComplete);

            dirtFriends.transform.position = dirtPilesEnd;

            if (secondElapsedTime >= pilesDuration || Mathf.Approximately(secondElapsedTime, pilesDuration))
            {
                moveUp = false;
                secondElapsedTime = 0;
                relativeStart = dirtFriends.transform.position;
            }

            if (moveUp)
            {
                secondElapsedTime += Time.deltaTime;
                float secondFractComplete = elapsedTime / pilesDuration;
                dirtFriends.transform.position = Vector3.Lerp(dirtPilesEnd, dirtPilesStart, secondFractComplete);
            }
            else
            {
                secondElapsedTime += Time.deltaTime;
                float secondFractComplete = elapsedTime / pilesDuration;
                dirtFriends.transform.position = Vector3.Lerp(relativeStart, dirtPilesEnd, secondFractComplete);
            }

            yield return new WaitForFixedUpdate();
        }

        Destroy(dirt);

        dirtFriends.SetActive(false);
    }
}
