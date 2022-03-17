using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lightning : MonoBehaviour
{
    public void LightningStrike(Vector3 targetPosition)
    {
        gameObject.transform.position = targetPosition;
        //GetComponent<ParticleSystem>().Play();
        GetComponent<VisualEffect>().Play();
    }
}
