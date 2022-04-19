using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddedShmoney : MonoBehaviour
{

    public List<GameObject> digitPlaces = new List<GameObject>();

    private Vector3 endPos;

    private bool lerpTo = false;

    private float elapsedTime = 0;

    private float duration = 0.5f;

    private Vector3 startPos;

    float objectAlpha = 1f;

    void Start()
    {
        //startPos = transform.position;
        //transform.localPosition = new Vector3(81, 36, 0);
        //startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Lerp to end position when it recieves it
        if (lerpTo)
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / duration;

            objectAlpha = Mathf.Abs(percentComplete - 1);

            transform.position = Vector3.Lerp(startPos, endPos, percentComplete);

            foreach (Transform child in transform)
            {
                Image number = child.gameObject.GetComponent<Image>();

                Color c = number.color;

                c.a = objectAlpha;

                number.color = c;
            }

            if (Vector3.Distance(transform.position, endPos) < 0.1)
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

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
        transform.localPosition = startPos;
    }
}
