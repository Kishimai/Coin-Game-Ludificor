using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [BoxGroup("Input")]
    public GameObject CoinMachine;
    [BoxGroup("Input")]
    public GameObject background;
    [BoxGroup("Input")]
    public GameObject eventManager;

    [BoxGroup("UI")] // Component Panels
    public GameObject Selection, Settings, Collectibles, InGame, PausedGame, ShopInGame, ShopButton, 
    PrizeSelection, OpenCapsuleButton, ItemSelection, LoadingScreen, ComboPegSelection;
    [BoxGroup("UI")]
    public Slider _music, _sfx;
    [BoxGroup("UI")]
    public Text _musicText, _sfxText;
    [BoxGroup("UI")]
    public Toggle _muteAllToggle;
    [BoxGroup("UI")]
    public Text _CoinText;
    [BoxGroup("UI")]
    public TMP_Dropdown _aspectRatio;
    [BoxGroup("UI")]
    public Toggle windowedToggle;

    [BoxGroup("Settings")]
    public bool _muteAll = false;
    [BoxGroup("Settings")]
    [Range(1, 100)]
    public float _musicVolume, _sfxVoume;
    [BoxGroup("Settings")]
    public bool isPaused = false;
    [BoxGroup("Settings")]
    public bool windowed = false;


    [BoxGroup("Current Datas")]
    public float _currentCoin;
    [BoxGroup("Current Datas")]
    public int currentUIMenu;

    [BoxGroup("Loading")]
    public Text LoadingText;
    [BoxGroup("Loading")]
    public List<string> loading_statements = new List<string>();

    /// <summary>
    /// Uses for Changing Panel.
    /// </summary>
    /// <param name="data">Specific Change to Panel</param>
    public void Update_UI(int data){
        switch(data){
            case 1: // Settings
                Settings.SetActive(true);
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                isPaused = true;

                currentUIMenu = 1;
                break;
            case 2: // Collectibles
                Collectibles.SetActive(true);
                Selection.SetActive(false);
                Settings.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                isPaused = false;

                currentUIMenu = 2;
                break;
            case 3: // Selection
                Selection.SetActive(true);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                InGame.SetActive(false);
                background.SetActive(true);
                PausedGame.SetActive(false);
                ShopInGame.SetActive(false);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                // isPaused = true;  Disabled So Depending on where last layer was from it will be paused or not

                currentUIMenu = 3;
                break;
            case 4: // InGame UI Panel
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(true);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                isPaused = false;

                currentUIMenu = 4;
                break;
            case 5: // Paused Game Panel
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(true);
                InGame.SetActive(false);
                PausedGame.SetActive(true);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                isPaused = true;

                currentUIMenu = 5;
                break;
            case 6: // Menu for selecting item capsules
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(false);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(true);
                OpenCapsuleButton.SetActive(true);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                isPaused = false;

                currentUIMenu = 6;
                break;
            case 7: // Loading screen while game is preparing
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(false);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(true);
                ComboPegSelection.SetActive(false);

                currentUIMenu = 7;
                break;

            case 8: // Item Capsule Selection
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(false);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(true);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(true);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(false);

                isPaused = true;

                currentUIMenu = 8;
                break;

            case 9: // Combo peg placement
                Selection.SetActive(false);
                Collectibles.SetActive(false);
                Settings.SetActive(false);
                background.SetActive(false);
                InGame.SetActive(false);
                PausedGame.SetActive(false);

                PrizeSelection.SetActive(false);
                OpenCapsuleButton.SetActive(false);
                ItemSelection.SetActive(false);
                LoadingScreen.SetActive(false);
                ComboPegSelection.SetActive(true);

                isPaused = false;

                currentUIMenu = 9;
                break;
        }
    }

    public void Start() {

        Update_UI(7); // Makes sure game is showing "Loading" screen when coins are being set up

        LoadLoadingStatement();
    }

    public void LoadLoadingStatement(){
        var picked = Random.Range(1, loading_statements.Count);
        var counted = 0;

        
        foreach(string _text in loading_statements){
            counted++;
            if(counted == picked){
                LoadingText.text = _text;
            }
        }
    }

    public void Update(){ // Updates Values on Setting: Probably Shouldnt be here.
        _musicVolume = _music.value;
        _sfxVoume = _sfx.value;
        _sfxText.text = _sfxVoume.ToString("0");
        _musicText.text = _musicVolume.ToString("0");
        _muteAll = _muteAllToggle.enabled;
        _CoinText.text = $"$ {_currentCoin.ToString("0")}";
        windowed = windowedToggle.enabled;

        if (Input.GetKey(KeyCode.Tab))
        {
            _currentCoin += 10000;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(currentUIMenu == 5){
                Update_UI(4);
            } else {
                Update_UI(5);
            }
        }

        if(windowed == true && Screen.fullScreen == true){ // windowed enabled, screen not windowed
            Screen.fullScreen = false;
        } else if(windowed == false && Screen.fullScreen == false){ // not windowed, screen windowed
            Screen.fullScreen = true;
        }   
    }

    public void UpdateSettings(){
        if(isPaused == true){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Used for Buttons on Shop
    /// </summary>
    public void Shop(string functionToUse){ // Supposed to be more functions to this yet it really isnt
        switch(functionToUse){
            case "OpenOrClose":
                ShopInGame.SetActive(!ShopInGame.activeSelf);
                ShopButton.SetActive(!ShopButton.activeSelf);
                break;
        }
    }

    public void ProjectSettings(string Function){
       if(Function == "WindowResolutionUpdate"){ // Updates by comparing the list from the Current Resolution via: !=

       }    
    }

    public void QuitGame(){
        Application.Quit();
    }

}
