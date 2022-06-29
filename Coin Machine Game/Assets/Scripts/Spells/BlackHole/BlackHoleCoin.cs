using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCoin : MonoBehaviour
{

    private Rigidbody coinRb;

    public GameObject audioManager;
    public AudioManager audio;

    public GameObject blackHoleObject;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 5;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = GetComponent<Rigidbody>();

        audioManager = GameObject.FindGameObjectWithTag("audio_manager");

        audio = audioManager.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = coinRb.velocity;
        currentSpeed = currentVelocity.magnitude;

        float difference = Mathf.Abs(currentSpeed - speedOfLastFrame);

        if (difference > soundThreshold)
        {
            audio.PlayAudioClip("coin");
        }

        speedOfLastFrame = currentSpeed;
    }

    public void GetBumped()
    {
        Vector3 bumpForce = new Vector3(0, 100, 0);

        coinRb.AddForce(bumpForce);
    }

    public void Absorb()
    {
        Debug.LogWarning("Black hole not fully implemented");
        GameObject blackHole = Instantiate(blackHoleObject, transform.position, Quaternion.identity);
        Vector3 newPosition = new Vector3(blackHole.transform.position.x, 1.5f, blackHole.transform.position.z);
        blackHole.transform.position = newPosition;
        Destroy(gameObject);
    }
}
