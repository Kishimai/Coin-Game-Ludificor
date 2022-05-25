using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    public TestAudio singleSound;
    public TestAudioTwo multiSound;

    public GameObject coinSound;

    // Update is called once per frame
    void Update()
    {
        // Plays one of many coin sounds when player presses B
        if (Input.GetKeyDown(KeyCode.B))
        {
            multiSound.PlayAudio();
        }

        // Plays only a single coin sound when player presses V
        if (Input.GetKeyDown(KeyCode.V))
        {
            singleSound.PlayAudio();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        GameObject newSound;

        newSound = Instantiate(coinSound);

        newSound.GetComponent<TestAudioThree>().PlayAudio();
    }

}
