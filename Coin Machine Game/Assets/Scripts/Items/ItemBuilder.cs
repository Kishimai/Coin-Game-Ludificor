using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{

    public GameObject builder;

    public GameObject currentPlane;
    public GameObject[] printerPlanes;

    public GameObject currentItems;

    public int planeIndex;

    // Holds the position for the new printer surface
    public Vector3 newPosition;
    // Holds the position for the starting location the printer should be moved to
    public Vector3 startPosition;
    // Holds the position for the ending location the printer should be moved to
    public Vector3 endPosition;


    // Start is called before the first frame update
    void Start()
    {
        builder = gameObject;
        printerPlanes = GameObject.FindGameObjectsWithTag("printer_plane");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Determines the new printer position, build surface, build surface area, and start/end positions
    public void DeterminePositions()
    {
        // Assigns the current plane from the array of printer planes
        currentPlane = printerPlanes[planeIndex];

        // Gets the current plane's position
        Vector3 planePosition = currentPlane.transform.position;
        // Gets plane scale to determine area which coins can be printed in
        Vector3 planeScale = currentPlane.transform.localScale;
        // Gets printer bar scale to determine the distance from plane's edge the printer bar must be, in order to prevent clipping or overlapping
        Vector3 printerScale = builder.transform.localScale;

        // Note that the printer only currently operates on the Z axis (starts at a lower Z value and moves to a greater Z value)
        // Temporarily stores Z start position
        float newStartZ;
        // Temporarily stores Z end position
        float newEndZ;

        // Starts by moving printer bar to chosen plane's center
        builder.transform.position = currentPlane.transform.position;

        // Determines start position (increases y pos so that coins are above surface)
        newStartZ = planePosition.z - (planeScale.z / 2) + (printerScale.z / 2);
        startPosition = new Vector3(builder.transform.position.x, builder.transform.position.y + 0.1f, newStartZ);

        // Determines end position (increases y pos so that coins are above surface)
        newEndZ = planePosition.z + (planeScale.z / 2) + (printerScale.z / 2);
        endPosition = new Vector3(builder.transform.position.x, builder.transform.position.y + 0.1f, newEndZ);

        // Moves printer bar to starting position
        builder.transform.position = startPosition;

        // Determine new position's starting value (just above this, printer bar is set to the correct start position, so newPosition becomes that value
        newPosition = builder.transform.position;

    }

}
