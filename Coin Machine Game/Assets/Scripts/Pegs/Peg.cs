using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    public float coinValueModifier;
    private float defaultCoinValueModifier = 0;

    private bool amModified;

    void Start()
    {
        amModified = false;
        coinValueModifier = defaultCoinValueModifier;
    }

    public void ModifyPeg(float pegValueModifier, Material pegMaterial)
    {
        gameObject.GetComponent<MeshRenderer>().material = pegMaterial;
        coinValueModifier = pegValueModifier;
        amModified = true;
    }

    void OnTriggerEnter(Collider other)
    {
        // Runs if colliding with coin and this peg is modified
        if (other.gameObject.tag == "coin" && amModified)
        {
            // Multiply coin's value by modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateBumper();

            other.gameObject.transform.parent.GetComponent<CoinLogic>().pegValueModifier = coinValueModifier;
        }
    }
}
