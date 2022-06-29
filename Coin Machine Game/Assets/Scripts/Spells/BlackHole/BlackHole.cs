using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    private Vector3 holePos;
    private Vector3 succSize;
    private Vector3 defaultSize;
    // Three seconds spent consuming, 1 spent idle
    private float timeUntilCollapse = 4f;

    private float succForce = 60;
    private float swirlingForce = 30;
    private float eventHorizon = 0;
    private Vector3 collapseSpeed = new Vector3(0.3f, 0.3f, 0.33f);
    public GameObject swirlingObject;
    public GameObject eventHorizonObject;
    public ParticleSystem swirlingParticles;
    public GameObject explosionEffect;

    private GameObject audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio_manager");

        audioManager.GetComponent<AudioManager>().PlayAudioClip("black_hole_open");

        audioManager.GetComponent<AudioManager>().PlayAudioClip("black_hole_drone");
        
        GameObject playerCam = GameObject.FindGameObjectWithTag("MainCamera");

        defaultSize = playerCam.GetComponent<CoinPlacement>().defaultBlackHoleSize;
        succSize = playerCam.GetComponent<CoinPlacement>().blackHoleSize;

        float modifier = succSize.x / defaultSize.x;

        ParticleSystem system = swirlingObject.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = system.main;
        main.startSize = main.startSize.constant * modifier;

        swirlingParticles = system;

        swirlingParticles.Play();

        holePos = transform.position;
        eventHorizon = succSize.x / 3.2f;
        gameObject.transform.localScale = succSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilCollapse > 0)
        {
            timeUntilCollapse -= Time.deltaTime;
            if (timeUntilCollapse <= 1f && timeUntilCollapse > 0 && eventHorizonObject.transform.localScale.x > 0.1f)
            {
                eventHorizonObject.transform.localScale -= collapseSpeed * Time.deltaTime;
            }
        }
        else
        {
            audioManager.GetComponent<AudioManager>().PlayAudioClip("black_hole_close");
            GameObject effects = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effects.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (timeUntilCollapse > 1f)
        {
            if (other.CompareTag("coin"))
            {
                GameObject coin = other.gameObject.transform.parent.gameObject;

                if (Vector3.Distance(coin.transform.position, transform.position) < eventHorizon)
                {
                    Destroy(coin);
                }
                else
                {
                    //GameObject coin = other.gameObject.transform.parent.gameObject;
                    ApplyForce(coin);
                }
            }
            else if (other.CompareTag("styrofoam_coin"))
            {

                GameObject coin = other.gameObject.transform.parent.gameObject;

                if (Vector3.Distance(coin.transform.position, transform.position) < eventHorizon)
                {
                    Destroy(coin);
                }
                else
                {
                    //GameObject coin = other.gameObject.transform.parent.gameObject;
                    ApplyForce(coin);
                }
            }
            else if (other.CompareTag("palladium_coin"))
            {

                GameObject coin = other.gameObject.transform.parent.gameObject;

                if (Vector3.Distance(coin.transform.position, transform.position) < eventHorizon)
                {
                    Destroy(coin);
                }
                else
                {
                    //GameObject coin = other.gameObject.transform.parent.gameObject;
                    ApplyForce(coin);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (timeUntilCollapse > 1f)
        {
            if (other.CompareTag("coin"))
            {

                GameObject coin = other.gameObject.transform.parent.gameObject;

                if (Vector3.Distance(coin.transform.position, transform.position) < eventHorizon)
                {
                    Destroy(coin);
                }
                else
                {
                    //GameObject coin = other.gameObject.transform.parent.gameObject;
                    ApplyForce(coin);
                }
            }
            else if (other.CompareTag("styrofoam_coin"))
            {

                GameObject coin = other.gameObject.transform.parent.gameObject;

                if (Vector3.Distance(coin.transform.position, transform.position) < eventHorizon)
                {
                    Destroy(coin);
                }
                else
                {
                    //GameObject coin = other.gameObject.transform.parent.gameObject;
                    ApplyForce(coin);
                }
            }
            else if (other.CompareTag("palladium_coin"))
            {

                GameObject coin = other.gameObject.transform.parent.gameObject;

                if (Vector3.Distance(coin.transform.position, transform.position) < eventHorizon)
                {
                    Destroy(coin);
                }
                else
                {
                    //GameObject coin = other.gameObject.transform.parent.gameObject;
                    ApplyForce(coin);
                }
            }
        }
    }

    private void ApplyForce(GameObject coin)
    {
        Vector3 direction = (coin.transform.position - holePos).normalized;
        Vector3 rotationalForce = new Vector3(Random.Range(0, 50), Random.Range(0, 50), Random.Range(0, 50));

        coin.GetComponent<Rigidbody>().AddForce(-direction * succForce);
        coin.GetComponent<Rigidbody>().AddForce(coin.transform.right * swirlingForce);
        coin.GetComponent<Rigidbody>().AddTorque(rotationalForce);
    }
}
