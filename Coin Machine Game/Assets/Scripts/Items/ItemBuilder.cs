using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{

    public GameObject builder;

    public GameObject currentPlane;
    public GameObject[] allPrinterPlanes;
    public List<GameObject> playerMachinePlanes = new List<GameObject>();

    public GameObject[] itemCapsules;

    public int itemsToBuild;

    public bool initialBuild;

    // Holds the position for the new printer surface
    public Vector3 newPosition;

    public Vector3 planeBoundry;

    // Start is called before the first frame update
    void Start()
    {
        builder = gameObject;
        allPrinterPlanes = GameObject.FindGameObjectsWithTag("printer_plane");
        initialBuild = true;

        // Search through allPrinterPlanes to find the planes of the player's coin machine
        for (int i = 0; i < allPrinterPlanes.Length; ++i)
        {
            // If the plane's X position is 0, add it to playerMachinePlanes
            if (Mathf.Approximately(allPrinterPlanes[i].transform.position.x, 0) || allPrinterPlanes[i].transform.position.x == 0)
            {
                playerMachinePlanes.Add(allPrinterPlanes[i]);
            }
        }
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
        if (initialBuild)
        {
            for (int i = 0; i < allPrinterPlanes.Length; ++i)
            {
                DetermineBoundry(i);
                InitialItemBuild();
            }

            initialBuild = false;
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

        planeBoundry = new Vector3(planeScale.x - 0.2f, 0, planeScale.z - 0.2f);

    }

    public void InitialItemBuild()
    {
        itemsToBuild = Random.Range(1, 4);

        for (int i = 0; i < itemsToBuild + 1; ++i)
        {
            // Picks random X position within the boundry of the plane
            float randomXPosition = Random.Range(-planeBoundry.x, planeBoundry.x);
            // Picks random Z position within the boundry of the plane
            float randomZPosition = Random.Range(-planeBoundry.z, planeBoundry.z);

            //Instantiate Item
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(randomXPosition, 0, randomZPosition);
        }
    }

}
