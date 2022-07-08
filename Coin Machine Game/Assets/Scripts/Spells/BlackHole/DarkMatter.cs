using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMatter : MonoBehaviour
{

    private Rigidbody rb;

    public double value;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float randX = Random.Range(-20, -5);
        float randZ = Random.Range(-20, 20);

        Vector3 newRotation = new Vector3(transform.rotation.x + randX, transform.rotation.y, transform.rotation.z + randZ);

        gameObject.transform.eulerAngles = newRotation;

        rb.AddForce(transform.up * 1024);
    }
}
