using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMuter : MonoBehaviour
{
    public AudioMixer mixer;
    public bool muted;


    public GameObject mainMenuMute;
    public GameObject ingameMenuMute;
    public Toggle mainMenuToggle;
    public Toggle ingameMenuToggle;

    private void Start()
    {
        mainMenuToggle = mainMenuMute.GetComponent<Toggle>();
        ingameMenuToggle = ingameMenuMute.GetComponent<Toggle>();
    }

    public void MuteToggle(){
        muted = !muted;
        mainMenuToggle.SetIsOnWithoutNotify(muted);
        ingameMenuToggle.SetIsOnWithoutNotify(muted);
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
