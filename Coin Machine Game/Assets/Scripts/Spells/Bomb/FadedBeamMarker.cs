using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadedBeamMarker : MonoBehaviour
{

    public GameObject parentCoin;

    // Update is called once per frame
    void Update()
    {
        if (parentCoin == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 newTransform = new Vector3(parentCoin.transform.position.x, parentCoin.transform.position.y + 4.9f, parentCoin.transform.position.z);

            transform.position = newTransform;
        }
    }
}
