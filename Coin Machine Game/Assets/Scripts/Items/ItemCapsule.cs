using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCapsule : MonoBehaviour
{

    public Rigidbody capsuleRb;
    public GameObject eventManager;
    private int coinBlockLayer = 13;

    private GameObject audioManager;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 0;

    // Start is called before the first frame update
    void Start()
    {
        capsuleRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        Physics.IgnoreLayerCollision(coinBlockLayer, coinBlockLayer);
        audioManager = GameObject.FindGameObjectWithTag("audio_manager");
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = capsuleRb.velocity;
        currentSpeed = currentVelocity.magnitude;

        float difference = Mathf.Abs(currentSpeed - speedOfLastFrame);

        if (difference > soundThreshold)
        {
            audioManager.GetComponent<AudioManager>().PlayAudioClip("peg");
        }

        speedOfLastFrame = currentSpeed;

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
