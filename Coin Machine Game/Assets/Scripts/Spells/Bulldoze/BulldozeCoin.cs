using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozeCoin : MonoBehaviour
{

    private Rigidbody coinRb;

    private void Start()
    {
        coinRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin_pusher")
        {
            other.transform.GetComponent<CoinPusher>().StartBulldoze();
            GameObject lightning = GameObject.FindGameObjectWithTag("bulldoze_lightning");
            lightning.GetComponent<Lightning>().LightningStrike(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
    public void GetBumped()
    {
        Vector3 bumpForce = new Vector3(0, 100, 0);

        coinRb.AddForce(bumpForce);
    }

}
