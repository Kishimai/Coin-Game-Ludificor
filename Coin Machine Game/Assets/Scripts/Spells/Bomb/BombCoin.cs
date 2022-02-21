using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCoin : MonoBehaviour
{

    public GameObject explosion;

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
