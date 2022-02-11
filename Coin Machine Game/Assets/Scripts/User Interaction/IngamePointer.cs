using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngamePointer : MonoBehaviour
{
    private GameObject gameManager;
    public GameObject pegSelectionTool;
    private GameObject selectedPeg;
    private int comboPegsInInventory;
    private bool gameplayIsReady;
    private bool gameplayPaused;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    // Drop zone layer is to be assigned to the "Drop Zone" object, which is responsible for allowing the player to place coins at all
    // When the mouse cursor is hovering over the "Drop Zone" the player will be allowed to place coins
    private int dropZoneLayer = 6;
    // The layer mask is used to ensure that the mouse cursor is ONLY looking for the "Drop Zone" when the player is placing coins
    public LayerMask dropZoneLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game_manager");

        // Remove
        gameplayIsReady = true;

        // Sets the layer mask to the drop zone layer
        // Doing this ensures that ONLY the drop zone layer is saved to the layer mask
        dropZoneLayerMask = (1 << dropZoneLayer);
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

        if (gameplayIsReady && !gameplayPaused)
        {
            MouseTracking();
        }
    }

    void MouseTracking()
    {
        // Defines a new ray, which shoots from the camera's origin to the tip of the player's cursor at all times
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Defines a raycast hit point, which stores information for the position the cursor is hovering over
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, dropZoneLayerMask))
        {
            float clampedX = Mathf.Clamp(hit.point.x, xMin, xMax);
            float clampedY = Mathf.Clamp(hit.point.y, yMin, yMax);
            float clampedZ = Mathf.Clamp(hit.point.z, -6.2f, -6.25f);

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, clampedZ);

            pegSelectionTool.transform.position = clampedPosition;
        }

        selectedPeg = pegSelectionTool.GetComponent<PointerCollision>().collidedPeg;

        if (Input.GetButtonDown("Fire1") && selectedPeg != null)
        {
            CheckInventory();
        }
    }

    void CheckInventory()
    {
        if (gameManager.GetComponent<ItemInventory>().collectedItems.Contains("combo_peg"))
        {
            ConstructComboPeg();
        }
    }

    void ConstructComboPeg()
    {
        gameManager.GetComponent<ItemInventory>().collectedItems.Remove("combo_peg");

        selectedPeg.GetComponent<Peg>().ConvertToCombo();
    }
}
