using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCoin : MonoBehaviour
{

    public GameObject explosion;

    private Rigidbody coinRb;

    private void Start()
    {
        coinRb = GetComponent<Rigidbody>();
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

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
}
