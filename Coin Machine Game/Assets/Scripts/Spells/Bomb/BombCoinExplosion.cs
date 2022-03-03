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

    private Vector3 startRadius = Vector3.zero;
    private Vector3 endRadius;
    private Vector3 endRadiusForForce;

    private void Start()
    {
        gameObject.transform.localScale = startRadius;
        endRadius = new Vector3(radiusOfExplosion, radiusOfExplosion, radiusOfExplosion);
        endRadiusForForce = new Vector3(radiusOfForce, radiusOfForce, radiusOfForce);
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
        forceBubble.transform.localScale += (expansionRate * rateModifier) * Time.deltaTime;

        if (forceBubble.transform.localScale.x > endRadiusForForce.x - 0.1)
        {
            forceBubble.SetActive(false);
        }

        if (gameObject.transform.localScale.x > endRadius.x - 0.1)
        {
            Destroy(gameObject);
        }
    }
}
