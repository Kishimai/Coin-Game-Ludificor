using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedShmoney : MonoBehaviour
{

    public List<GameObject> digitPlaces = new List<GameObject>();

    private float timeUntilDeletion = 0.5f;

    private Vector3 endPos;

    private bool lerpTo = false;

    // Update is called once per frame
    void Update()
    {
        // Lerp to end position when it recieves it
        if (lerpTo)
        {
            if (timeUntilDeletion > 0)
            {
                timeUntilDeletion -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetEndPos(Vector3 pos)
    {
        endPos = pos;
        lerpTo = true;
    }
}
