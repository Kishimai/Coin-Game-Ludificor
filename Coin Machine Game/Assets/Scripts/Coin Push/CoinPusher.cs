using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPusher : MonoBehaviour
{

    // -------------------- CoinPusher -------------------- //

    // CoinPusher is responsible for coin pusher movements and actions
    // This script should be attached to the Coin Pusher object

    // ---------------------------------------------------- //

    // Used to speed up or slow down the coin pusher
    public float pusherSpeed;
    public float defaultSpeed;
    // Used to determine the coin pusher's starting position
    public Vector3 startingPosition;
    // Used to measure the coin pusher's current position
    public Vector3 currentPosition;
    // Used to determine the positions that the coin pusher will move between
    public Vector3 positionA;
    public Vector3 positionB;
    // Holds the rigid body of the pusher object so that force can be applied to it and it can be moved
    public Rigidbody pusherRb;
    // Holds the object for the coin pusher itself
    public GameObject coinPusher;
    // Holds the layer for the stationary machine parts (walls, back drop, floor, etc)
    private int stationaryPartsLayer = 8;
    // Used to stop the coin pusher from moving during events or gameplay states/phases
    public bool allowingMovement;

    public bool surgeEvent;
    public float surgeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Gathers the coin pusher rigid body
        pusherRb = GetComponent<Rigidbody>();

        // Makes sure collision with machine parts cannot occour (otherwise the coin pusher locks up and cannot move)
        Physics.IgnoreLayerCollision(stationaryPartsLayer, 7);

        // Gathers the coin pusher object
        coinPusher = gameObject;

        // Gathers the starting position (can be changed if desired)
        //startingPosition = positionA;
        startingPosition = new Vector3(coinPusher.transform.position.x, coinPusher.transform.position.y, positionB.z);

        // Sets the starting position of the coin pusher
        coinPusher.transform.position = startingPosition;

        pusherSpeed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        // Runs if the coin pusher is allowed to move
        if (allowingMovement)
        {
            if (surgeEvent)
            {
                pusherSpeed = surgeSpeed;
            }
            else
            {
                pusherSpeed = defaultSpeed;
            }

            MovePusher();
            
            // Used to ensure the pusher's speed is modified right away when a surge event happens
            // Previously it would only set the speed when the pusher reached point a or point b, even when the event was supposed to be happening
            if (surgeEvent && pusherRb.velocity.z > 0)
            {
                pusherRb.velocity = new Vector3(0, 0, pusherSpeed);
            }
            else if (surgeEvent && pusherRb.velocity.z < 0)
            {
                pusherRb.velocity = new Vector3(0, 0, -pusherSpeed);
            }
        }
    }

    // Responsible for moving the coin pusher between two positions (A and B)
    void MovePusher()
    {
        // Constantly gathers the pusher's current position for comparing
        currentPosition = coinPusher.transform.position;

        // Runs if the coin pusher is at OR past position A
        // Mathf.approximately is used because floats and doubles shouldn't be compared with ==
        if (Mathf.Approximately(currentPosition.z, positionA.z) || currentPosition.z < positionA.z)
        {
            // Moves pusher in the positive Z direction, expecting to reach position B
            pusherRb.velocity = new Vector3(0, 0, pusherSpeed);
        }

        // Runs if the coin pusher is at OR past position B
        // Mathf.approximately is used because floats and doubles shouldn't be compared with ==
        if (Mathf.Approximately(currentPosition.z, positionB.z) || currentPosition.z > positionB.z)
        {
            // Moves pusher in the positive Z direction, expecting to reach position A
            pusherRb.velocity = new Vector3(0, 0, -pusherSpeed);
        }
    }
}
