using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{

    // -------------------- CamMovement -------------------- //

    // CamMovement is responsible for camera zoom (and special rotation if eventually desired)

    // ----------------------------------------------------- //

    // Holds the camera object
    public Camera playerCam;
    // Tracks the mouse wheel input
    public float scrollInput;
    // Minimum view for camera
    public float minView;
    // Maximum view for camera
    public float maxView;
    // Default view for camera
    public float defaultFov;
    // Zoom speed
    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Finds the camera and assigns it to playerCam
        playerCam = Camera.main;
        // Assigns the default field of view
        defaultFov = 60;
        //zoomSpeed = ;
    }

    // Update is called once per frame
    void Update()
    {
        // Tracks player mouse wheel movements and assigns it to scrollInput (value ranges from -1 to 1)
        scrollInput = Input.mouseScrollDelta.y;

        // Runs if player is attempting to zoom out AND the camera has not reached its max view
        if (scrollInput < 0 && playerCam.fieldOfView < maxView)
        {
            // Zoom out
            playerCam.fieldOfView += 1 * zoomSpeed * Time.deltaTime;
        }

        // Runs if player is attempting to zoom in AND the camera has not reached its max view
        if (scrollInput > 0 && playerCam.fieldOfView > minView)
        {
            // Zoom in
            playerCam.fieldOfView -= 1 * zoomSpeed * Time.deltaTime;
        }
    }
}
