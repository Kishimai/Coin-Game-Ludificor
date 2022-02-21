using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCoinExplosion : MonoBehaviour
{

    public float durationOfExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        durationOfExplosion -= Time.deltaTime;

        if (durationOfExplosion <= 0)
        {
            Destroy(gameObject);
        }
    }
}
