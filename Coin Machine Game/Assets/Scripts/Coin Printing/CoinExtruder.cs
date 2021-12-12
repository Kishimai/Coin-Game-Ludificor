using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinExtruder : MonoBehaviour
{

    // -------------------- CoinExtruder -------------------- //

    // CoinExtruder is responsible for collision detection and extruder specific logic on the printer
    // This script is attached to each print extruder
    // If the extruder with this script encounters a collision, it stores that information for the printer to use

    // ------------------------------------------------------ //

    // Used to temporarily mark a collision event
    // The value of isClear is sent to CoinRowPrinter
    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        // Starts by making sure the extruder is set to clear
        isClear = true;
    }

    // Runs when the extruder encounters a collision event
    private void OnTriggerEnter(Collider other)
    {
        // Runs once, as soon as the extruder encounters a collision with an object with the "machine_colliders" tag
        if (other.gameObject.tag == "machine_colliders")
        {
            isClear = false;
        }
    }

    // Runs every frame that a collision event is taking place
    private void OnTriggerStay(Collider other)
    {
        // Runs every frame that the extruder is colliding with an object with the "machine_colliders" tag
        if (other.gameObject.tag == "machine_colliders")
        {
            isClear = false;
        }
    }

    // Runs when the extruder stops colliding with an object
    private void OnTriggerExit(Collider other)
    {
        // Runs when the extruder stops colliding with an object with the "machine_colliders" tag
        if (other.gameObject.tag == "machine_colliders")
        {
            isClear = true;
        } 
    }

}
