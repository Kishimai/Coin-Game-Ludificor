using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegPop : MonoBehaviour
{

    private float elapsedTime = 0;
    private float duration = 0.1f;
    public Vector3 startPos = Vector3.zero;
    public Vector3 endPos;
    private CapsuleCollider col;
    private Rigidbody rb;
    private bool happened = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!happened && startPos != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, endPos) < 0.5)
            {
                col.isTrigger = false;
                rb.useGravity = true;
                happened = true;
                int randForce = Random.Range(250, 300);
                rb.AddForce(transform.forward * randForce);
            }
            else
            {
                elapsedTime += Time.deltaTime;
                float percentComplete = elapsedTime / duration;

                transform.position = Vector3.Lerp(startPos, endPos, percentComplete);
            }
        }

    }

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
        SetEndPos();
    }

    private void SetEndPos()
    {
        endPos = startPos;
        endPos.z += 4;
    }
}
