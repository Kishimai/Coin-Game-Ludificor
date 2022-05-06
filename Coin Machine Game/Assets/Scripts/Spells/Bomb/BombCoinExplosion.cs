using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCoinExplosion : MonoBehaviour
{
    public GameObject forceBubble;

    public float explosionDuration;
    public float forceDuration;

    private float elapsedTime;

    public float radiusOfExplosion;
    public float radiusOfForce;
    public Vector3 expansionRate;
    public float rateModifier;
    public float explosionForce;
    public float upwardsForce = 3f;

    private Vector3 startRadius = Vector3.zero;
    private Vector3 endRadius;
    private Vector3 endRadiusForForce;

    private void Start()
    {
        gameObject.transform.localScale = startRadius;
        endRadius = new Vector3(radiusOfExplosion, radiusOfExplosion, radiusOfExplosion);
        endRadiusForForce = new Vector3(radiusOfExplosion, radiusOfExplosion, radiusOfExplosion);
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

    private void LateUpdate()
    {
        //forceBubble.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //other.GetComponentInParent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "bomb_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "tremor_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "bulldoze_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "palladium_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
        if (other.gameObject.tag == "styrofoam_coin")
        {
            other.GetComponentInParent<Rigidbody>().AddExplosionForce(explosionForce, gameObject.transform.position, gameObject.transform.localScale.x, 3f);
        }
    }
}
