using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCoinExplosion : MonoBehaviour
{
    public GameObject forceBubble;

    public float explosionDuration = 0.3f;
    public float forceDuration = 0.3f;

    private float elapsedTime;

    public float radiusOfExplosion = 3;
    public float radiusOfForce = 10;
    public Vector3 expansionRate = Vector3.one;
    public float rateModifier = 1.2f;
    public float explosionForce = 100;
    public float upwardsForce = 3f;

    private Vector3 startRadius = Vector3.zero;
    private Vector3 endRadius;
    private Vector3 endRadiusForForce;

    private void Start()
    {
        GameObject playerCam = GameObject.FindGameObjectWithTag("MainCamera");

        radiusOfExplosion = playerCam.GetComponent<CoinPlacement>().radiusOfExplosion;
        explosionForce = playerCam.GetComponent<CoinPlacement>().explosionForce;
        upwardsForce = playerCam.GetComponent<CoinPlacement>().upwardsForce;
        expansionRate = playerCam.GetComponent<CoinPlacement>().expansionRate;

        gameObject.transform.localScale = startRadius;
        endRadius = new Vector3(radiusOfExplosion, radiusOfExplosion, radiusOfExplosion);
        endRadiusForForce = new Vector3(radiusOfExplosion, radiusOfExplosion, radiusOfExplosion);
        //StartCoroutine(Expand());
    }

    // Update is called once per frame
    void Update()
    {
        
        elapsedTime += Time.deltaTime;

        float fractComplete = elapsedTime / explosionDuration;
        float fractCompleteForForce = elapsedTime / forceDuration;

        //gameObject.transform.localScale = Vector3.Lerp(startRadius, endRadius, fractComplete);
        //forceBubble.transform.localScale = Vector3.Lerp(startRadius, endRadiusForForce, fractCompleteForForce);

        gameObject.transform.localScale += expansionRate * Time.deltaTime;
        //forceBubble.transform.localScale += (expansionRate * rateModifier) * Time.deltaTime;

        //if (forceBubble.transform.localScale.x > endRadiusForForce.x - 0.1)
        //{
        //    forceBubble.SetActive(false);
        //}

        if (gameObject.transform.localScale.x > endRadius.x - 0.1)
        {
            Destroy(gameObject);
        }
        
    }

    private IEnumerator Expand()
    {
        while (gameObject.transform.localScale.x < endRadius.x - 0.1)
        {
            elapsedTime += Time.fixedDeltaTime;

            float fractComplete = elapsedTime / explosionDuration;
            float fractCompleteForForce = elapsedTime / forceDuration;

            gameObject.transform.localScale += expansionRate * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        //forceBubble.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //other.GetComponentInParent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 0.8f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "item")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce / 2, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "popped_peg")
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(explosionForce * 0.75f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "bomb_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 0.8f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "tremor_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 0.8f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "bulldoze_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 0.8f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "palladium_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 0.75f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "styrofoam_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 1.2f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "black_hole")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce * 1.2f, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
    }
}
