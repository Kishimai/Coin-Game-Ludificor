using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlacement : MonoBehaviour
{

    // -------------------- CoinPlacement -------------------- //

    // CoinPlacement is attached to the player camera, and is used to detect the location of the player's mouse, to allow them to place coins into the machine
    // This script will take information from the player inventory, and use that to give them control over item and coin placement

    // ------------------------------------------------------- //

    // Replace this. (it is currently used to show the image of a coin, which helps the player see where they are about to place it)
    public GameObject testCoin;
    // Holds the player's currently selected coin
    public GameObject selectedCoin;
    // Shows the image of the currently selected coin so the player can visualize their placements better
    private GameObject coinGuide;
    // Prevents the use of this script when gameplay is not ready yet
    // !(EventsManager is responsible for deciding game states)!
    public bool gameplayIsReady;

    // Drop zone layer is to be assigned to the "Drop Zone" object, which is responsible for allowing the player to place coins at all
    // When the mouse cursor is hovering over the "Drop Zone" the player will be allowed to place coins
    private int dropZoneLayer = 6;
    // The layer mask is used to ensure that the mouse cursor is ONLY looking for the "Drop Zone" when the player is placing coins
    public LayerMask dropZoneLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure this script knows that gameplay is not yet ready
        gameplayIsReady = false;

        // Creates the coinGuide object (Change this so that the coinGuide constantly reflects the currently selected coin)!
        coinGuide = Instantiate(testCoin, gameObject.transform.position, Quaternion.identity);

        // Disables the coinGuide object so its not constantly shown in the scene
        coinGuide.SetActive(false);

        // Sets the layer mask to the drop zone layer
        // Doing this ensures that ONLY the drop zone layer is saved to the layer mask
        dropZoneLayerMask = (1 << dropZoneLayer);

    }

    // Update is called once per frame
    void Update()
    {
        // Runs if gameplay is ready
        if (gameplayIsReady)
        {
            MouseTracking();
        }

    }

    // Responsible for tracking mouse position for coin and item placement
    void MouseTracking()
    {
        // Defines a new ray, which shoots from the camera's origin to the tip of the player's cursor at all times
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Defines a raycast hit point, which stores information for the position the cursor is hovering over
        RaycastHit hit;

        // Runs only if the mouse cursor is currently hovering over the "Drop Zone" object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, dropZoneLayerMask))
        {
            // Enables the coinGuide object so that the player can see where they are about to place a coin
            coinGuide.SetActive(true);

            // Ensures that the coin guide's position is always where the mouse cursor is
            coinGuide.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.25f);

            // Runs if the player presses the shift button (used only for testing purposes and should be removed)!
            if (Input.GetButtonDown("Fire3"))
            {
                // Makes five of the currently selected coin for each shift button press
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(selectedCoin, new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.25f), Quaternion.identity);
                }
            }
            // Runs if the player clicks the left mouse button
            if (Input.GetButtonDown("Fire1"))
            {
                // Places the currently selected coin
                Instantiate(selectedCoin, new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.25f), Quaternion.identity);
            }

        }

        // Runs if the mouse cursor is NOT hovering over the "Drop Zone" object
        else
        {
            // Disables the coin guide
            coinGuide.SetActive(false);
        }
    }
}
