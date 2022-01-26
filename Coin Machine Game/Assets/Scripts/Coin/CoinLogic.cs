using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    public Rigidbody coinRb;
    public GameObject eventManager;
    public GameObject coin;

    public int coinLayer = 11;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
    }

    // Update is called once per frame
    void Update()
    {
        // Stops coins from flying around randomly when the game is still in initialization phase
        if (eventManager.GetComponent<EventsManager>().initializationPhase)
        {
            coinRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            coinRb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "plinko_zone")
        {
            //Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);

            //Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), coin.GetComponent<BoxCollider>(), true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "plinko_zone")
        {
            //Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer, false);

            //Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), coin.GetComponent<BoxCollider>(), false);
        }
    }
}
