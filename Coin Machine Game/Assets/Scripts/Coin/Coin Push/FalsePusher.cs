using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePusher : MonoBehaviour
{

    // -------------------- CoinPusher -------------------- //

    // CoinPusher is responsible for coin pusher movements and actions
    // This script should be attached to the Coin Pusher object

    // ---------------------------------------------------- //

    // Used to speed up or slow down the coin pusher
    public float pusherSpeed;
    // Used to determine the coin pusher's starting position
    public Vector3 startingPosition;
    // Used to measure the coin pusher's current position
    public Vector3 currentPosition;
    // Used to determine the positions that the coin pusher will move between
    public Vector3 positionA;
    public Vector3 positionB;
    private Vector3 relativeStart;
    private Vector3 relativeEnd;
    // Holds the rigid body of the pusher object so that force can be applied to it and it can be moved
    public Rigidbody pusherRb;
    // Holds the object for the coin pusher itself
    public GameObject coinPusher;
    // Holds the layer for the stationary machine parts (walls, back drop, floor, etc)
    private int stationaryPartsLayer = 8;
    // Used to stop the coin pusher from moving during events or gameplay states/phases
    public bool allowingMovement;

    public float timer;

    private float savedSpeed;

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
        startingPosition = new Vector3(coinPusher.transform.position.x, coinPusher.transform.position.y, positionA.z);

        relativeStart = startingPosition;
        relativeEnd = positionA;

        // Sets the starting position of the coin pusher
        coinPusher.transform.position = relativeStart;

        timer = Random.Range(1.0f, 3.0f);

        allowingMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (timer <= 0 && allowingMovement)
        {
            MovePusher();
        }
    }

    void MovePusher()
    {

        // Constantly gathers the pusher's current position for comparing
        currentPosition = coinPusher.transform.position;

        // if pusher is within 0.2 units of pos a
        if (currentPosition.z < positionA.z + 0.05f)
        {
            // Current position equal to posA plus 0.35 (makes it slightly further away from posA)
            currentPosition.z = positionA.z + 0.1f;

            relativeStart = currentPosition;
            relativeEnd = new Vector3(currentPosition.x, currentPosition.y, positionB.z);

            transform.position = relativeStart;

            pusherSpeed = Mathf.Abs(pusherSpeed);

            pusherRb.velocity = new Vector3(0, 0, pusherSpeed);

        }
        // if pusher is within 0.2 unis of pos b
        else if (currentPosition.z > positionB.z - 0.05f)
        {
            // Current position equal to posB minus 0.35 (makes it slightly further away from posB)
            currentPosition.z = positionB.z - 0.1f;

            relativeStart = currentPosition;
            relativeEnd = new Vector3(currentPosition.x, currentPosition.y, positionA.z);

            transform.position = relativeStart;

            pusherSpeed = -pusherSpeed;

            pusherRb.velocity = new Vector3(0, 0, pusherSpeed);
        }

    }

    public void PausePusher()
    {
        savedSpeed = pusherRb.velocity.z;
        pusherRb.velocity = Vector3.zero;
        allowingMovement = false;
    }
    public void UnpausePusher()
    {
        pusherRb.velocity = new Vector3(0, 0, savedSpeed);
        allowingMovement = true;
    }
}

