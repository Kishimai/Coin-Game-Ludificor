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
    private GameObject audioManager;

    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 0;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        audioManager = GameObject.FindGameObjectWithTag("audio_manager");
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = rb.velocity;
        currentSpeed = currentVelocity.magnitude;

        float difference = Mathf.Abs(currentSpeed - speedOfLastFrame);

        if (difference > soundThreshold)
        {
            audioManager.GetComponent<AudioManager>().PlayAudioClip("peg");
        }

        speedOfLastFrame = currentSpeed;

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
