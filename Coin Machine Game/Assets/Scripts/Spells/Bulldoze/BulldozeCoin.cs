using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozeCoin : MonoBehaviour
{

    private Rigidbody coinRb;
    public GameObject audioManager;
    public AudioManager audio;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 5;

    private void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("audio_manager");
        audio = audioManager.GetComponent<AudioManager>();
    }

    private void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin_pusher")
        {
            audio.PlayAudioClip("bulldoze");
            other.transform.GetComponent<CoinPusher>().StartBulldoze();
            GameObject lightning = GameObject.FindGameObjectWithTag("bulldoze_lightning");
            Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 2f);
            lightning.GetComponent<Lightning>().LightningStrike(pos);
            Destroy(gameObject);
        }
    }
    public void GetBumped()
    {
        Vector3 bumpForce = new Vector3(0, 100, 0);

        coinRb.AddForce(bumpForce);
    }

}
