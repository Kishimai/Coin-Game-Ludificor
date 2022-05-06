using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject comboSound;
    public GameObject goldSound;
    public GameObject diamondSound;
    public GameObject tremorSound;
    public GameObject bombSound;
    public GameObject coinSound;

    public void PlayAudioClip(string clipName, int index = 0)
    {
        GameObject audioSource;
        switch (clipName)
        {
            case "combo":
                audioSource = Instantiate(comboSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<ComboPing>().PlayAudio(index);
                break;

            case "gold":
                audioSource = Instantiate(goldSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<GoldPing>().PlayAudio();
                break;

            case "diamond":
                audioSource = Instantiate(diamondSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<DiamondPing>().PlayAudio();
                break;

            case "coin":
                audioSource = Instantiate(coinSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<CoinSound>().PlayAudio();
                break;

            case "tremor":
                break;

            case "bomb":
                break;
        }
    }
}
