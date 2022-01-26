using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCapsule : MonoBehaviour
{

    public Rigidbody capsuleRb;
    public GameObject eventManager;

    // Start is called before the first frame update
    void Start()
    {
        capsuleRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
    }

    // Update is called once per frame
    void Update()
    {
        if (eventManager.GetComponent<EventsManager>().initializationPhase)
        {
            capsuleRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            capsuleRb.constraints = RigidbodyConstraints.None;
        }
    }
}
