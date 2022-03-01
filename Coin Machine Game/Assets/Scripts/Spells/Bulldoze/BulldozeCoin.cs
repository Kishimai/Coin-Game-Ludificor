using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozeCoin : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "coin_pusher")
        {
            collision.transform.GetComponent<CoinPusher>().StartBulldoze();
            Destroy(gameObject);
        }
    }

}
