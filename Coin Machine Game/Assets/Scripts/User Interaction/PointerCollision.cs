using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollision : MonoBehaviour
{
    public GameObject collidedPeg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "peg")
        {
            collidedPeg = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "peg")
        {
            collidedPeg = null;
        }
    }
}
