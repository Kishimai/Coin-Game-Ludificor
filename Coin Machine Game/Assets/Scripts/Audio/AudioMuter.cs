using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMuter : MonoBehaviour
{
  public AudioMixer mixer;
  public bool muted;
  public void MuteToggle(){
    muted = !muted;
    Mute();
  }
    private void Mute(){
      if(muted == true){
        mixer.SetFloat("masterVol", -80f);
      }
      else{
        mixer.SetFloat("masterVol",0f);
      }
    }


}
