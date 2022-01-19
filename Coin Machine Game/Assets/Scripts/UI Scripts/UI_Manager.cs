using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [BoxGroup("Input")]
    public GameObject CoinMachine;
    [BoxGroup("Input")]
    public GameObject background;

    [BoxGroup("UI")] // for panels of the player
    public GameObject Selection, Settings, Collectibles, InGame, PausedGame, ShopInGame, ShopButton, CapsuleItemSelection;
    [BoxGroup("UI")]
    public Slider _music, _sfx;
    [BoxGroup("UI")]
    public Text _musicText, _sfxText;
    [BoxGroup("UI")]
    public Toggle _muteAllToggle;
    [BoxGroup("UI")]
    public Text _CoinText;

    [BoxGroup("Settings")]
    public bool _muteAll = false;
    [BoxGroup("Settings")]
    [Range(1, 100)]
    public float _musicVolume, _sfxVoume;

    [BoxGroup("Current Datas")]
    public float _currentCoin;

    public void Update_UI(int data){
        switch(data){
            case 1: // Settings UI Panel
                Settings.SetActive(true);
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);

                Debug.Log("Case: 1");

                break;
            case 2: // Collectibles UI Panel
                Collectibles.SetActive(true);
                Selection.SetActive(false);
                Settings.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);

                Debug.Log("Case: 2");

                break;
            case 3: // Selection UI Panel
                Selection.SetActive(true);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);
                ShopInGame.SetActive(false);

                Debug.Log("Case: 3");

                break;
            case 4: // InGame UI Panel
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(true);
                PausedGame.SetActive(false);

                CapsuleItemSelection.SetActive(false);

                Debug.Log("Case: 4");

                break;
            case 5: //
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(true);
                InGame.SetActive(false);
                PausedGame.SetActive(true);

                Debug.Log("Case: 5");

                break;
            case 6:
                CapsuleItemSelection.SetActive(true);

                Debug.Log("Case: 6");

                break;
        }
    }

    public void Start() {
        Update_UI(3); // Makes Sure to make it in Starting Position First
    }

    public void Update(){
        _musicVolume = _music.value;
        _sfxVoume = _sfx.value;
        _sfxText.text = _sfxVoume.ToString("0");
        _musicText.text = _musicVolume.ToString("0");
        _muteAll = _muteAllToggle.enabled;
        _CoinText.text = $"✰ {_currentCoin.ToString("0")}";
    }

    /// <summary>
    /// Main Core Function of the Shop System
    /// </summary>
    /// <param name="functionToUse">Which Function you want to use</param>
    public void Shop(string functionToUse){
        switch(functionToUse){
            case "OpenOrClose":
                ShopInGame.SetActive(!ShopInGame.activeSelf);
                ShopButton.SetActive(!ShopButton.activeSelf);
                break;
        }
    }

    public void QuitGame(){
        Application.Quit();
    }

}
