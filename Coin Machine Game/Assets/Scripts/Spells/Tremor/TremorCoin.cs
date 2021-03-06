using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TremorCoin : MonoBehaviour
{
    private GameObject tremorHandler;
    private Rigidbody coinRb;
    public AudioSource shake;

    public GameObject audioManager;
    public AudioManager audio;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 5;

    private GameObject tremorAudioSource;

    public float sinkDuration;

    private void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        tremorHandler = GameObject.FindGameObjectWithTag("gameplay_event_system");

        tremorAudioSource = GameObject.FindGameObjectWithTag("tremor_sound");

        shake = tremorAudioSource.GetComponent<AudioSource>();

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
        if (other.gameObject.tag == "tremor_activation")
        {
            tremorHandler.GetComponent<TremorShake>().Shake(gameObject.transform.position);
            shake.Play();
            StartCoroutine(Sink());

            //Destroy(gameObject);
        }
    }

    public IEnumerator Tremor(float tremorDuration, float tremorPower)
    {
        while (tremorDuration > 0)
        {
            tremorDuration -= Time.deltaTime;

            float xForce = Random.Range(0, tremorPower);
            float yForce = Random.Range(0, tremorPower);
            float zForce = Random.Range(0, tremorPower);

            Vector3 randomForce = new Vector3(xForce, yForce, zForce);

            coinRb.AddForce(randomForce);

            yield return new WaitForFixedUpdate();

        }
    }

    private IEnumerator Sink()
    {
        float elapsedTime = 0;

        Vector3 startPos = gameObject.transform.position;
        Vector3 endPos = new Vector3(gameObject.transform.position.x, -1, gameObject.transform.position.z);

        coinRb.detectCollisions = false;

        coinRb.constraints = RigidbodyConstraints.FreezeAll;

        while (elapsedTime < sinkDuration)
        {
            elapsedTime += Time.deltaTime;

            float fractComplete = elapsedTime / sinkDuration;

            gameObject.transform.position = Vector3.Lerp(startPos, endPos, fractComplete);

            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    public void GetBumped()
    {
        Vector3 bumpForce = new Vector3(0, 100, 0);

        coinRb.AddForce(bumpForce);
    }
}
