using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    public Vector3 holePos;
    // Three seconds spent consuming, 1 spent idle
    public float timeUntilCollapse = 4f;

    // Start is called before the first frame update
    void Start()
    {
        holePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilCollapse > 0)
        {
            timeUntilCollapse -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            Vector3 direction = (other.transform.position - holePos).normalized;
            Vector3 rotationalForce = new Vector3(Random.Range(0, 50), Random.Range(0, 50), Random.Range(0, 50));

            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(-direction * 100);
            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().AddTorque(rotationalForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            Vector3 direction = (other.transform.position - holePos).normalized;
            Vector3 rotationalForce = new Vector3(Random.Range(0, 50), Random.Range(0, 50), Random.Range(0, 50));

            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(-direction * 100);
            other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().AddTorque(rotationalForce);
        }
    }
}
