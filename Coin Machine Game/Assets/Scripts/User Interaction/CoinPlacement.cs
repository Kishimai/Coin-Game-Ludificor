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
    // Holds the player's currently selected coin: >> I'll be changing its Components on each placement
    public GameObject selectedCoin;
    // Used for generation codes
    public CoinGeneration generation;
    // Shows the image of the currently selected coin so the player can visualize their placements better
    private GameObject coinGuide;
    // Prevents the use of this script when gameplay is not ready yet
    // !(EventsManager is responsible for deciding game states)!
    public bool gameplayIsReady;
    private bool gameplayPaused;

    private GameObject gameManager;

    // Drop zone layer is to be assigned to the "Drop Zone" object, which is responsible for allowing the player to place coins at all
    // When the mouse cursor is hovering over the "Drop Zone" the player will be allowed to place coins
    private int dropZoneLayer = 6;
    // The layer mask is used to ensure that the mouse cursor is ONLY looking for the "Drop Zone" when the player is placing coins
    public LayerMask dropZoneLayerMask;
    // Responsible for preventing the player from dropping coins rapidly
    private float dropCooldown;
    // Drop cooldown gets its value from maxCooldown (set maxCooldown to the wanted value depending on game states, events, or other situations)
    public float maxCooldown;

    public float minXDropClamp;
    public float maxXDropClamp;
    public float yClamp;
    public float zClamp;

    public bool blitzEvent = false;
    public float blitzCooldown;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure this script knows that gameplay is not yet ready
        gameplayIsReady = false;

        // Creates the coinGuide object (Change this so that the coinGuide constantly reflects the currently selected coin)!
        coinGuide = Instantiate(testCoin, gameObject.transform.position, Quaternion.Euler(90, 0, 0));

        // Disables the coinGuide object so its not constantly shown in the scene
        coinGuide.SetActive(false);

        // Sets the layer mask to the drop zone layer
        // Doing this ensures that ONLY the drop zone layer is saved to the layer mask
        dropZoneLayerMask = (1 << dropZoneLayer);

        gameManager = GameObject.FindGameObjectWithTag("game_manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetComponent<UI_Manager>().currentUIMenu != 4)
        {
            gameplayPaused = true;
        }
        else
        {
            gameplayPaused = false;
        }

        // Runs if gameplay is ready
        if (gameplayIsReady && !gameplayPaused)
        {
            if (dropCooldown > 0)
            {
                dropCooldown -= Time.deltaTime;
            }

            // Makes coin guide appear like the selected coin (Need to strip all properties and physics interactions from the guide!)
            //coinGuide = selectedCoin;

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
            // If coin is on cooldown, the coin guide dissapears
            if (dropCooldown <= 0)
            {
                // Enables the coinGuide object so that the player can see where they are about to place a coin
                coinGuide.SetActive(true);
            }
            else
            {
                coinGuide.SetActive(false);
            }

            float clampedX = Mathf.Clamp(hit.point.x, minXDropClamp, maxXDropClamp);
            float clampedY = Mathf.Clamp(hit.point.y, yClamp, yClamp + 0.1f);
            float clampedZ = Mathf.Clamp(hit.point.z, zClamp, zClamp + 0.1f);

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, clampedZ);

            // Ensures that the coin guide's position is always where the mouse cursor is
            coinGuide.transform.position = clampedPosition;

            // Runs if the player clicks the left mouse button
            if (Input.GetButtonDown("Fire1"))
            {
                DropLogic(clampedPosition);
            }

        }

        // Runs if the mouse cursor is NOT hovering over the "Drop Zone" object
        else
        {
            // Disables the coin guide
            coinGuide.SetActive(false);
        }
    }

    void DropLogic(Vector3 clampedPosition)
    {
        if (dropCooldown <= 0 && blitzEvent == false)
        {
            generation.GetPlacementData();
            // Places the currently selected coin >> Changing its Component every placement
            Instantiate(selectedCoin, clampedPosition, Quaternion.Euler(90, 0, 0));
            dropCooldown = maxCooldown;
        }
        else if (dropCooldown <= 0 && blitzEvent == true)
        {
            generation.GetPlacementData();
            Vector3 blitzPosition = new Vector3(clampedPosition.x, clampedPosition.y, clampedPosition.z + 1.25f);
            Instantiate(selectedCoin, blitzPosition, Quaternion.Euler(90, 0, 0));
            dropCooldown = blitzCooldown;
        }
    }
}
