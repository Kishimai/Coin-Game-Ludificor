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
    [BoxGroup("Input")]
    public GameObject eventManager;

    [BoxGroup("UI")] // UI Components Panels
    public GameObject Selection, Settings, Collectibles, InGame, PausedGame, ShopInGame, ShopButton, CapsuleItemSelection, LoadingScreen;
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
            case 1: // Settings
                Settings.SetActive(true);
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);

                LoadingScreen.SetActive(false);

                Debug.Log("Case: 1");

                break;
            case 2: // Collectibles
                Collectibles.SetActive(true);
                Selection.SetActive(false);
                Settings.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);

                LoadingScreen.SetActive(false);

                Debug.Log("Case: 2");

                break;
            case 3: // Selection
                Selection.SetActive(true);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);
                ShopInGame.SetActive(false);

                LoadingScreen.SetActive(false);

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
                LoadingScreen.SetActive(false);

                Debug.Log("Case: 4");

                break;
            case 5: // Paused Game Panel
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(true);
                InGame.SetActive(false);
                PausedGame.SetActive(true);

                LoadingScreen.SetActive(false);

                Debug.Log("Case: 5");

                break;
            case 6: // Menu for selecting item capsules
                CapsuleItemSelection.SetActive(true);

                Debug.Log("Case: 6");

                break;
            case 7: // Loading screen while game is preparing
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(false);
                PausedGame.SetActive(false);
                CapsuleItemSelection.SetActive(false);
                LoadingScreen.SetActive(true);
                break;
        }
    }

    public void Start() {

        Update_UI(7); // Makes sure game is showing "Loading" screen when coins are being set up

        //Update_UI(3); // Makes Sure to make it in Starting Position First
        //  ^ I moved this code to the EventsManager script ^
        // When initialization phase is done, it runs Update_UI(3); and the game plays as normal

    }

    public void Update(){ // Updates Values on Setting: Probably Shouldnt be here.
        _musicVolume = _music.value;
        _sfxVoume = _sfx.value;
        _sfxText.text = _sfxVoume.ToString("0");
        _musicText.text = _musicVolume.ToString("0");
        _muteAll = _muteAllToggle.enabled;
        _CoinText.text = $"✰ {_currentCoin.ToString("0")}";
    }

    /// <summary>
    /// Used for Buttons on Shop
    /// </summary>
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
