using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicVolControl : MonoBehaviour
{
  public AudioMixer mixer;
  public void SetSound(float soundLevel){
    mixer.SetFloat("musicVol", Mathf.Log(soundLevel) * 20);
  }

}
