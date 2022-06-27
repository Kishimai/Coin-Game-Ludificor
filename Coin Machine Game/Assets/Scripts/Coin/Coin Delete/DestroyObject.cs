using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Runs if object that collides with Coin Destroyer is a coin
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "item")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "bomb_coin")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "tremor_coin")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "bulldoze_coin")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "palladium_coin")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "styrofoam_coin")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "black_hole")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
