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

    private void Start()
    {
        coinRb = GetComponent<Rigidbody>();

        boomAudioSource = GameObject.FindGameObjectWithTag("bomb_sound");

        boom = boomAudioSource.GetComponent<AudioSource>();
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
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
        Vector3 bumpForce = new Vector3(0, 20, 0);

        //Debug.LogError("Skrunked");

        coinRb.AddForce(bumpForce);
    }
}
