using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{

    public GameObject builder;

    public GameObject currentPlane;
    public GameObject[] allPrinterPlanes;
    public GameObject playerMachinePlane;

    public GameObject[] itemCapsules;

    public int itemsToBuild;

    public bool initialBuildFinished;

    // Holds the position for the new printer surface
    public Vector3 newPosition;

    public Vector3 planeBoundry;

    // Start is called before the first frame update
    void Start()
    {
        builder = gameObject;
        allPrinterPlanes = GameObject.FindGameObjectsWithTag("printer_plane");

        List<GameObject> temp = new List<GameObject>();

        // Search through allPrinterPlanes to find the planes of the player's coin machine
        for (int i = 0; i < allPrinterPlanes.Length; ++i)
        {
            // If the plane's X position is 0, add it to playerMachinePlanes
            if (Mathf.Approximately(allPrinterPlanes[i].transform.position.y, 0))// || allPrinterPlanes[i].transform.position.x == 0 && allPrinterPlanes[i].transform.position.y == 0)
            {
                temp.Add(allPrinterPlanes[i]);
                //playerMachinePlanes.Add(allPrinterPlanes[i]);
            }
        }

        allPrinterPlanes = temp.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        // Order of actions
        // Initialization phase:
        // Determine boundry
        // Start by building item capsules on all planes

        // Gameplay phase:
        // After that, work only within the boundry of the real coin machine's printer planes for the remainder of the game
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

        for (int i = 0; i < itemsToBuild + 1; ++i)
        {
            // Picks random X position within the boundry of the plane
            float randomXPosition = Random.Range(-planeBoundry.x, planeBoundry.x);
            // Picks random Z position within the boundry of the plane
            float randomZPosition = Random.Range(-planeBoundry.z, planeBoundry.z);

            //Instantiate Item

            //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Finds position of build plane then picks a random point within that boundry to build item
            //sphere.transform.position = new Vector3(planePos.x + randomXPosition / 2, 0, planePos.z + randomZPosition / 2);

            Instantiate(itemCapsules[0], new Vector3(planePos.x + randomXPosition / 2, 6, planePos.z + randomZPosition / 2), Quaternion.identity);
        }
    }

}
