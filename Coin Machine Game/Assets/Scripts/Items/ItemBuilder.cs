using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{

    public GameObject builder;

    public GameObject currentPlane;
    public GameObject[] allPrinterPlanes;
    public GameObject playerMachinePlane;

    public GameObject coinParent;
    public GameObject capsuleParent;

    public GameObject[] itemCapsules;

    public GameObject coin;
    public CoinGeneration generation;

    public int itemsToBuild;

    public bool initialBuildFinished;

    public bool itemRainEvent;

    // Holds the position for the new printer surface
    public Vector3 newPosition;

    public Vector3 planeBoundry;

    // Used to drop item every 60 seconds
    public float timeUntilNextItem;
    public float maxTimeUntilItem = 60f;
    public float limit = 10f;

    public bool isPaused = false;

    void Start()
    {
        builder = gameObject;
        allPrinterPlanes = GameObject.FindGameObjectsWithTag("printer_plane");

        List<GameObject> temp = new List<GameObject>();

        // Search through allPrinterPlanes to find the planes of the player's coin machine
        for (int i = 0; i < allPrinterPlanes.Length; ++i)
        {
            // If the plane's X position is 0, add it to playerMachinePlanes
            if (Mathf.Approximately(allPrinterPlanes[i].transform.position.y, 0))
            {
                temp.Add(allPrinterPlanes[i]);
            }
        }

        allPrinterPlanes = temp.ToArray();
    }

    private void FixedUpdate()
    {
        if (!initialBuildFinished)
        {
            for (int i = 0; i < allPrinterPlanes.Length; ++i)
            {
                DetermineBoundry(i);
                InitialItemBuild(allPrinterPlanes[i]);
            }

            initialBuildFinished = true;
        }
        
        if (timeUntilNextItem > 0 && itemRainEvent == false && !isPaused)
        {
            timeUntilNextItem -= Time.fixedDeltaTime;
        }

        if (timeUntilNextItem <= 0)
        {
            BuildItem();
            timeUntilNextItem = maxTimeUntilItem;
        }
        
    }

    // Determines the new printer position, build surface, build surface area, and start/end positions
    public void DetermineBoundry(int index)
    {
        // Assigns the current plane from the array of printer planes
        currentPlane = allPrinterPlanes[index];

        // Gets the current plane's position
        Vector3 planePosition = currentPlane.transform.position;
        // Gets plane scale to determine area which coins can be printed in
        Vector3 planeScale = currentPlane.transform.localScale;


        // Starts by moving printer bar to chosen plane's center
        builder.transform.position = planePosition;

        planeBoundry = new Vector3(planeScale.x - 1, 0, planeScale.z - 1);
    }

    public void InitialItemBuild(GameObject plane)
    {
        Vector3 planePos = plane.transform.position;
        itemsToBuild = Random.Range(1, 4);

        GameObject newItem;

        for (int i = 0; i < itemsToBuild + 1; ++i)
        {
            // Picks random X position within the boundry of the plane
            float randomXPosition = Random.Range(-planeBoundry.x, planeBoundry.x);
            // Picks random Z position within the boundry of the plane
            float randomZPosition = Random.Range(-planeBoundry.z, planeBoundry.z);

            // Picks random item capsule from list
            int randItem = Random.Range(0, itemCapsules.Length);

            // Makes random item capsule with random position
            newItem = Instantiate(itemCapsules[randItem], new Vector3(planePos.x + randomXPosition / 2, 2, planePos.z + randomZPosition / 2), Quaternion.identity);

            newItem.transform.SetParent(capsuleParent.transform);
        }
    }

    public IEnumerator ItemRain(int numItems)
    {
        itemRainEvent = true;

        int newNum = 0;
        Vector3 planePos = playerMachinePlane.transform.position;
        Vector3 planeScale = playerMachinePlane.transform.localScale;

        Vector3 boundry = new Vector3(planeScale.x - 1, 0, planeScale.z - 1);

        GameObject newItem;

        while (newNum < numItems)
        {
            // Picks random X position within the boundry of the plane
            float randomXPosition = Random.Range(-boundry.x, boundry.x);
            // Picks random Z position within the boundry of the plane
            float randomZPosition = Random.Range(-boundry.z, boundry.z);

            int randItem = Random.Range(0, itemCapsules.Length);

            newItem = Instantiate(itemCapsules[randItem], new Vector3(planePos.x + randomXPosition / 2, 27, planePos.z + randomZPosition / 2), Quaternion.identity);

            newItem.transform.SetParent(capsuleParent.transform);

            ++newNum;

            yield return new WaitForSeconds(1f);
        }

        itemRainEvent = false;
    }

    public void BuildItem()
    {
        GameObject newItem;

        Vector3 planePos = playerMachinePlane.transform.position;
        Vector3 planeScale = playerMachinePlane.transform.localScale;

        Vector3 boundry = new Vector3(planeScale.x - 1, 0, planeScale.z - 1);

        // Picks random X position within the boundry of the plane
        float randomXPosition = Random.Range(-boundry.x, boundry.x);
        // Picks random Z position within the boundry of the plane
        float randomZPosition = Random.Range(-boundry.z, boundry.z);

        int randItem = Random.Range(0, itemCapsules.Length);

        newItem = Instantiate(itemCapsules[randItem], new Vector3(planePos.x + randomXPosition / 2, 27, planePos.z + randomZPosition / 2), Quaternion.identity);

        newItem.transform.SetParent(capsuleParent.transform);
    }

    public void BuildCoin(GameObject styrofoam = null)
    {
        GameObject newCoin;

        Vector3 planePos = playerMachinePlane.transform.position;
        Vector3 planeScale = playerMachinePlane.transform.localScale;

        Vector3 boundry = new Vector3(planeScale.x - 1.2f, 0, planeScale.z - 1.2f);

        Vector3 randomRotation;

        // Picks random X position within the boundry of the plane
        float randomXPosition = Random.Range(-boundry.x, boundry.x);
        // Picks random Z position within the boundry of the plane
        float randomZPosition = Random.Range(-boundry.z, boundry.z);

        Vector3 position = new Vector3(planePos.x + randomXPosition / 2, 27, planePos.z + randomZPosition / 2);
        randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        if (styrofoam == null)
        {
            generation.GetPlacementData();

            newCoin = Instantiate(coin, position, Quaternion.Euler(randomRotation));

            newCoin.transform.SetParent(coinParent.transform);
        }
        else
        {
            newCoin = Instantiate(styrofoam, position, Quaternion.Euler(randomRotation));

            newCoin.transform.SetParent(coinParent.transform);
        }
    }

    public void ReduceBuildCooldown(float value)
    {
        maxTimeUntilItem -= value;
        timeUntilNextItem -= value;
        if (maxTimeUntilItem < limit)
        {
            maxTimeUntilItem = limit;
        }
    }
}
