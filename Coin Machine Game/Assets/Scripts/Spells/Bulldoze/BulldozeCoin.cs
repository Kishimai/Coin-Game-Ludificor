using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozeCoin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin_pusher")
        {
            other.transform.GetComponent<CoinPusher>().StartBulldoze();
            Destroy(gameObject);
        }
    }

}
