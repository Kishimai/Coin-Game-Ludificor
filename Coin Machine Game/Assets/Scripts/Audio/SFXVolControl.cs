using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXVolControl : MonoBehaviour
{
  public AudioMixer mixer;
  public void SetSound(float soundLevel){
    mixer.SetFloat("sfxVol", Mathf.Log(soundLevel) * 20);
  }

}
