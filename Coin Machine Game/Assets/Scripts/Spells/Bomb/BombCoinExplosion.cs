using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCoinExplosion : MonoBehaviour
{
    public float explosionDuration;

    private float elapsedTime;

    public float radiusOfExplosion;

    private Vector3 startRadius = Vector3.zero;
    private Vector3 endRadius;

    private void Start()
    {
        gameObject.transform.localScale = startRadius;
        endRadius = new Vector3(radiusOfExplosion, radiusOfExplosion, radiusOfExplosion);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        float fractComplete = elapsedTime / explosionDuration;

        gameObject.transform.localScale = Vector3.Lerp(startRadius, endRadius, fractComplete);

        if (gameObject.transform.localScale.x > endRadius.x - 0.1)
        {
            Destroy(gameObject);
        }
    }
}
