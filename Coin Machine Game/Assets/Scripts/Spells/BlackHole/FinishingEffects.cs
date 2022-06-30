using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingEffects : MonoBehaviour
{

    private float timeUntilDeletion = 1f;

    private void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilDeletion -= Time.deltaTime;
        if (timeUntilDeletion <= 0)
        {
            Destroy(gameObject);
        }
    }
}
