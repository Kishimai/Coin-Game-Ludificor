using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    private float coinValueModifier;
    private float defaultCoinValueModifier = 1;

    void Start()
    {
        coinValueModifier = defaultCoinValueModifier;
    }

    public void ModifyPeg(float pegValueModifier, Material pegMaterial)
    {
        gameObject.GetComponent<MeshRenderer>().material = pegMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            // Multiply coin's value by modifier
        }
    }
}
