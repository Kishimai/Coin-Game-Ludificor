using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BombCoin : MonoBehaviour
{

    public GameObject explosion;

    private Rigidbody coinRb;

    public AudioSource boom;

    private GameObject boomAudioSource;

    public float explosionForce = 0;

    public GameObject audioManager;
    public AudioManager audio;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 5;

    private GameObject gameManager;

    private void Start()
    {
        coinRb = GetComponent<Rigidbody>();

        boomAudioSource = GameObject.FindGameObjectWithTag("bomb_sound");

        boom = boomAudioSource.GetComponent<AudioSource>();

        audioManager = GameObject.FindGameObjectWithTag("audio_manager");
        audio = audioManager.GetComponent<AudioManager>();

        gameManager = GameObject.FindGameObjectWithTag("game_manager");

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

    public void Explode()
    {
        gameManager.GetComponent<SaveManager>().bombsExploded += 1;
        GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        boom.Play();
        Destroy(gameObject);
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

    public void GetBumped()
    {
        Vector3 bumpForce = new Vector3(0, 100, 0);

        coinRb.AddForce(bumpForce);
    }

    public void IncreaseExplosionRadius(float increase, float force)
    {
        explosion.GetComponent<BombCoinExplosion>().radiusOfExplosion += increase;
        explosion.GetComponent<BombCoinExplosion>().explosionForce += force;
        explosion.GetComponent<BombCoinExplosion>().upwardsForce += 0.5f;
        explosion.GetComponent<BombCoinExplosion>().expansionRate += Vector3.one;
    }
}
