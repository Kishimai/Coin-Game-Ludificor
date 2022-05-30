using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRowPrinter : MonoBehaviour
{


    // -------------------- CoinRowPrinter -------------------- //

    // CoinRowPrinter is responsible for initial placement of every coin and item when the game starts
    // It works by locating all flat surfaces with the "printer_plane" tag and building coins on them
    // The planes can be any size (so long as they are larger than the radius of 3 coins)
    // The printer will locate them, move to them, determine its boundries, and build coins until that plane is full
    // It will then move to another plane and repeat that process until all coins and items have been placed for the initialization phase

    // If a special event requires it, the printer must be adapted to place coins during events as well

    // -------------------------------------------------------- //


    public CoinGeneration generation;
    // Holds the printer object itself
    public GameObject printerBar;
    // Holds all print surfaces for coins and items
    public GameObject[] printerPlanes;
    // Holds the currently selected surface
    public GameObject currentPlane;
    // Holds all of the objects responsible for actually creating the coins (these objects are attached to the printer bar)
    public GameObject[] printExtruders;
    // Used to work through the list of printer planes
    public int planeIndex = 0;
    // Holds the currently selected item which should be created
    public GameObject currentItem;
    public CoinData copperData;
    public GameObject coinParent;
    // Holds the position for the new printer surface
    public Vector3 newPosition;
    // Holds the position for the starting location the printer should be moved to
    public Vector3 startPosition;
    // Holds the position for the ending location the printer should be moved to
    public Vector3 endPosition;
    // Offset is used to move the printer left and right after each print (it moves this way to fit more coins in a given area)
    public float offset = 0;
    public float newOffset = 0;
    // Item thickness can be used to judge how far from the surface the printer (or an individual extruder) should be to make sure coins/items do not clip into the machine
    public float itemThickness = 0;
    // Used to tell the printer to move to another position
    public bool newPrint;
    // Used to change the state of the printer to (moving), this stops coin and item printing, as well as gives time for collision detection
    public bool moving;
    // Used to change the state of the printer to (extruding), this stops movement and uses the collision information to print coins/items
    public bool extruding;

    // !(EventsManager is responsible for deciding game states)!
    // Used only once, when the game is set to initialization phase
    public bool initializeCoins = false;
    public bool initialBuildFinished = false;


    // Start is called before the first frame update
    void Start()
    {
        // Collects the printer bar
        printerBar = gameObject;
        // Locates all valid print surfaces and assigns them to printerPlanes
        printerPlanes = GameObject.FindGameObjectsWithTag("printer_plane");
        // Locates all of the printer's extruders and assigns them to printExtruders
        printExtruders = GameObject.FindGameObjectsWithTag("print_extruder");

        // Tells the printer that it should be in the moving state
        moving = true;
        // Tells the printer that this is a new print
        newPrint = true;

    }

    private void FixedUpdate()
    {
        // Runs if printer is in initialization phase and NOT creating items or coins
        if (initializeCoins && moving && !extruding)
        {
            InitialBuild();
        }
        // Runs if printer is in initialization phase, has not yet created all of the coins/items, and is NOT currently making something
        else if (initializeCoins && planeIndex < printerPlanes.Length && !extruding)
        {
            // Increases the planeIndex to move the printer over to the next surface
            planeIndex += 1;
            // Redundant if statement prevents array from going out of range
            if (planeIndex < printerPlanes.Length)
            {
                // Tells the printer it should be moving, and its on a new print operation
                newPrint = true;
                moving = true;
            }
        }
        // Runs if the printer has finished making coins/items for every print surface
        else if (initializeCoins && planeIndex >= printerPlanes.Length && !extruding)
        {
            initialBuildFinished = true;
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        // Runs when the printer is told to create coins/items
        if (extruding)
        {
            ExtrudeCoins();
            extruding = false;
        }
    }

    // Responsible for the initial coin builds required for the game to start
    public void InitialBuild()
    {
        // Runs when a new print is supposed to occour
        if (newPrint)
        {
            DeterminePositions();
            // After the information from the new print surface is gathered, new print is disabled
            newPrint = false;
        }

        // Moves the printer the distance of the diameter of a coin
        newPosition.z += 1.075f; // Change this to use a dynamic variable, which is set to the diameter of a chosen coin

        // Runs if the printer has not yet reached the end position
        // Printer also checks to see if the new position is 1 coin diameter away from the end position (ensures coins and items dont get placed too close to the edge)
        if (newPosition.z < endPosition.z && (newPosition.z + 1.075f) < endPosition.z)
        {

            // If newOffset equals offset, set newOffset to 0 (returns printer to orignal position)
            if (Mathf.Approximately(newOffset, offset))
            {
                newOffset = currentPlane.transform.position.x;
            }
            // Else, set newOffset equal to offset (moves printer to new position)
            else
            {
                newOffset = currentPlane.transform.position.x + offset;
            }

            // Moves printer bar, taking into account the X offset, and the new Z position
            printerBar.transform.position = new Vector3(newOffset, printerBar.transform.position.y, newPosition.z);

            // Tells the printer it is time to create coins/items
            extruding = true;

        }
        // Runs if the printer has finished its current operation
        else
        {
            moving = false;
            extruding = false;
        }
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
        Vector3 printerScale = printerBar.transform.localScale;

        // Note that the printer only currently operates on the Z axis (starts at a lower Z value and moves to a greater Z value)
        // Temporarily stores Z start position
        float newStartZ;
        // Temporarily stores Z end position
        float newEndZ;

        // Starts by moving printer bar to chosen plane's center
        printerBar.transform.position = currentPlane.transform.position;

        // Determines start position (increases y pos so that coins are above surface)
        newStartZ = planePosition.z - (planeScale.z / 2) + (printerScale.z / 2);
        startPosition = new Vector3(printerBar.transform.position.x, printerBar.transform.position.y + 0.1f, newStartZ);

        // Determines end position (increases y pos so that coins are above surface)
        newEndZ = planePosition.z + (planeScale.z / 2) + (printerScale.z / 2);
        endPosition = new Vector3(printerBar.transform.position.x, printerBar.transform.position.y + 0.1f, newEndZ);

        // Moves printer bar to starting position
        printerBar.transform.position = startPosition;

        // Determine new position's starting value (just above this, printer bar is set to the correct start position, so newPosition becomes that value
        newPosition = printerBar.transform.position;

        // Determine offset value
        offset = 0.675f;

        // Item thickness is currently not used (may be implemented when desired)
        itemThickness = 0.15f;

    }

    // Responsible for putting the coins/items into the scene
    public void ExtrudeCoins()
    {
        GameObject newCoin;

        // Looks at each individual extruder in the printExtruders array
        foreach (GameObject extruder in printExtruders)
        {
            // Collects that extruder's isClear value (true/false)
            bool extruderIsClear = extruder.GetComponent<CoinExtruder>().isClear;

            // Runs if the selected extruder is NOT intersecting with an object
            if (extruderIsClear)
            {
                // Randomly alters the position of the coin/item by a slight margin (allows for more unique locations)
                Vector3 positionalNoise = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0, 0.1f), Random.Range(-0.2f, 0.2f));

                //generation.returnObject();

                // TEMPORARY, RECORD ALL COIN DATAS ON BOARD AND SPAWN COINS WITH THOSE DATAS
                currentItem.GetComponent<Data_Interp>().data = copperData;
                currentItem.GetComponent<MeshRenderer>().material = copperData.materialColor;

                // Creates the current coin/item at the extruder's position, plus the random margin
                newCoin = Instantiate(currentItem, extruder.transform.position + positionalNoise, Quaternion.identity);

                Rigidbody coinRb = currentItem.GetComponent<Rigidbody>();

                coinRb.constraints = RigidbodyConstraints.FreezePosition;

                newCoin.transform.SetParent(coinParent.transform);
            }
        }
    }


}
