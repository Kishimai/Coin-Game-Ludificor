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
    //private Vector3 collapseSpeed = new Vector3(0.3f, 0.3f, 0.3f);
    public GameObject swirlingObject;
    public GameObject eventHorizonObject;
    public ParticleSystem swirlingParticles;
    public GameObject explosionEffect;
    public GameObject darkMatter;

    private Vector3 collapseStartSize = Vector3.zero;
    private Vector3 collapseEndSize = Vector3.zero;
    private float collapseDuration = 1f;
    private float collapseElapsedTime = 0;

    private Vector3 expandStartSize = Vector3.zero;
    private Vector3 expandEndSize = Vector3.zero;
    private float expandDuration = 0.25f;
    private float expandElapsedTime = 0;

    public int totalCombinedModifier = 0;
    public double totalCombinedValue = 0;

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
        collapseStartSize = eventHorizonObject.transform.localScale;

        expandEndSize = eventHorizonObject.transform.localScale;

        eventHorizonObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilCollapse > 0)
        {
            timeUntilCollapse -= Time.deltaTime;
            //if (timeUntilCollapse <= 1f && timeUntilCollapse > 0 && eventHorizonObject.transform.localScale.x > 0.1f)
            //{
                //eventHorizonObject.transform.localScale -= collapseSpeed * Time.deltaTime;
                //swirlingObject.transform.localScale -= collapseSpeed * Time.deltaTime;
            //}

            if (timeUntilCollapse > 3.5f)
            {
                expandElapsedTime += Time.deltaTime;
                float fractComplete = expandElapsedTime / expandDuration;

                eventHorizonObject.transform.localScale = Vector3.Lerp(expandStartSize, expandEndSize, fractComplete);
            }

            if (timeUntilCollapse <= 1f)
            {
                collapseElapsedTime += Time.deltaTime;
                float fractComplete = collapseElapsedTime / collapseDuration;

                eventHorizonObject.transform.localScale = Vector3.Lerp(collapseStartSize, collapseEndSize, fractComplete);
            }

            //if (timeUntilCollapse < 1 && !collapse)
            //{
            //    GameObject effects = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //    effects.SetActive(true);
            //    collapse = true;
            //}

        }
        else
        {
            audioManager.GetComponent<AudioManager>().PlayAudioClip("black_hole_close");
            //GameObject effects = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //effects.SetActive(true);

            GameObject dankMatter = Instantiate(darkMatter, transform.position, Quaternion.identity);

            if (totalCombinedModifier < 1)
            {
                totalCombinedModifier = 1;
            }

            double newValue = totalCombinedValue * totalCombinedModifier;

            double half = newValue / 2;
            double finalValue = newValue + half;

            dankMatter.GetComponent<DarkMatter>().value = finalValue;

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
                    totalCombinedValue += coin.GetComponent<Data_Interp>().data.currentValue;
                    totalCombinedModifier += (int) coin.GetComponent<CoinLogic>().totalValueModifier;
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
                    totalCombinedValue += coin.GetComponent<Data_Interp>().data.currentValue;
                    totalCombinedModifier += (int) coin.GetComponent<CoinLogic>().totalValueModifier;
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
                    totalCombinedValue += coin.GetComponent<Data_Interp>().data.currentValue;
                    totalCombinedModifier += (int) coin.GetComponent<CoinLogic>().totalValueModifier;
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
                    totalCombinedValue += coin.GetComponent<Data_Interp>().data.currentValue;
                    totalCombinedModifier += (int) coin.GetComponent<CoinLogic>().totalValueModifier;
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
                    totalCombinedValue += coin.GetComponent<Data_Interp>().data.currentValue;
                    totalCombinedModifier += (int) coin.GetComponent<CoinLogic>().totalValueModifier;
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
                    totalCombinedValue += coin.GetComponent<Data_Interp>().data.currentValue;
                    totalCombinedModifier += (int) coin.GetComponent<CoinLogic>().totalValueModifier;
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
