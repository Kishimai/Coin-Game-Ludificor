using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Input")]
    public List<CoinData> CoinData_List = new List<CoinData>();
    [BoxGroup("Input")]
    public UI_Manager UI;
    [BoxGroup("Input")]
    public CoinGeneration CoinGen;

    [BoxGroup("Main Input")]
    public CoinData CurrentCoin;
    [BoxGroup("Main Input")]
    public GameObject ObjectContent;

    [BoxGroup("Output")]
    public CoinData UpdatedCoin;

    public GameObject audioManager;
    public GameObject exclamation;
    private GameObject steamManager;

    private int currentOrder;
    private int maxNumCoins = 5;
    private bool finished = false;

    public int loadedUpgrades = 0;
    public int currentUpgrades = 0;

    public bool loadUpgrades = false;
    public double loadedCost = 0;

    private float soundCooldown = 0;


    public void Start(){
        steamManager = GameObject.FindGameObjectWithTag("steam_manager");
        audioManager = GameObject.FindGameObjectWithTag("audio_manager");
        RestartData();
    }

    private void Update() {

        if (UI._currentCoin >= CurrentCoin.currentCost)
        {
            exclamation.SetActive(true);
        }
        else
        {
            exclamation.SetActive(false);
        }

        if (soundCooldown > 0)
        {
            soundCooldown -= Time.deltaTime;
        }

        if(UpdatedCoin != CurrentCoin){
            ApplyInfoData();
        }

        if (loadUpgrades && currentUpgrades < loadedUpgrades)
        {
            UI._currentCoin += (CurrentCoin.currentCost + 1);
            UpgradeCoin();
        }
        else if (finished == false && loadUpgrades)
        {
            finished = true;
            UI._currentCoin = UI.coim;
        }
    }
    
    public void ApplyInfoData(){ // Setting infos of Each Coin Data
        ObjectContent.transform.GetChild(0).GetComponent<Image>().sprite = CurrentCoin.CoinArt;
        ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("0");
        ObjectContent.transform.GetChild(2).GetComponent<TMP_Text>().text = CurrentCoin.Name;
        ObjectContent.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentValue} > <color=green>${CurrentCoin.currentValue+CurrentCoin.AddPerLevel}</color>";

        for (int i = 0; i < 10; i++){ // Disabiling Visual Levels
            ObjectContent.transform.GetChild(4).transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);                  
        }

        UpdatedCoin = CurrentCoin;
        UnlockVisualLevel(0);

        if (CurrentCoin.Name == "Copper Coin")
        {
            CurrentCoin.CurrentLevel++;

            CurrentCoin.currentCost = CalculateValue(CurrentCoin.BaseCost, CurrentCoin.CurrentLevel, CurrentCoin.levelsForFree);
            // Might need to have elif for if coin is level 1, where its values should be base values and its current val is base
            //CurrentCoin.currentValue += CurrentCoin.AddPerLevel;
            ObjectContent.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentValue} > <color=green>${CurrentCoin.currentValue + CurrentCoin.AddPerLevel}</color>";
            ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentCost}";

            var child = ObjectContent.transform.GetChild(4);
            child.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

            CoinGen.CoinsAvail.Add(CurrentCoin);
        }

    }

    public void RestartData(){
        foreach(CoinData _data in CoinData_List){
            _data.currentCost = _data.BaseCost;
            _data.CurrentLevel = 0;
            _data.currentValue = _data.StartingValue;

            if(_data.name != "Copper"){
                _data.Unlocked = false;
            } else {
                _data.Unlocked = true;
            }

            _data.levelsForFree = 0;
        }

        currentOrder = 1;
    }

    public void UnlockVisualLevel(int UnlockTill){
        if (CurrentCoin.CurrentLevel < 11)
        {
            var child = ObjectContent.transform.GetChild(4);
            //child.GetChild(UnlockTill).transform.GetChild(0).gameObject.SetActive(true);

            if (UnlockTill > 0)
            {
                if (!CoinGen.CoinsAvail.Contains(CurrentCoin))
                {
                    CoinGen.CoinsAvail.Add(CurrentCoin);
                }
                child.GetChild(UnlockTill - 1).transform.GetChild(0).gameObject.SetActive(true);
            }

            // If player gets more than 5 coins at a time, remove lowest tier coin
            if (CoinGen.CoinsAvail.Count > maxNumCoins)
            {
                CoinGen.RemoveLowestTierCoin();
            }

            ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("$0");
        }
        else
        {
            ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("$0");
        }

        if (CurrentCoin.Name.Equals("God Coin"))
        {
            GodProgress();
        }

    }

    public void UpgradeCoin(){
        // try removing CurrentCoin.CurrentLevel != 10 // --------------------------
        if(UI._currentCoin >= CurrentCoin.currentCost) {// && CurrentCoin.CurrentLevel != 10){

            currentUpgrades++;

            CurrentCoin.CurrentLevel++;

            if (soundCooldown <= 0)
            {
                audioManager.GetComponent<AudioManager>().ShopSound();
                soundCooldown = 0.3f;
            }

            if (CurrentCoin.CurrentLevel == 10){

                ++currentOrder;

                foreach(CoinData _data in CoinData_List){
                    if(CurrentCoin.Order != 16){
                        if(currentOrder == _data.Order){ // Now tracks current order in shop and compares to data in list
                            CurrentCoin = _data;
                            CheckUpgradeProgress();
                        }
                    } else{
                        // Cant Upgrade Maxed out all coins
                        //finished = true;
                    }
                }

                CurrentCoin.currentCost = CalculateValue(CurrentCoin.BaseCost, CurrentCoin.CurrentLevel, CurrentCoin.levelsForFree);
                ObjectContent.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentValue} > <color=green>${CurrentCoin.currentValue + CurrentCoin.AddPerLevel}</color>";
                //ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("$0");
                //ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentCost}";

                UnlockVisualLevel(CurrentCoin.CurrentLevel);

            }
            else
            {
                UI._currentCoin -= CurrentCoin.currentCost;
                CurrentCoin.currentCost = CalculateValue(CurrentCoin.BaseCost, CurrentCoin.CurrentLevel, CurrentCoin.levelsForFree);
                // Might need to have elif for if coin is level 1, where its values should be base values and its current val is base
                CurrentCoin.currentValue += CurrentCoin.AddPerLevel;
                ObjectContent.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentValue} > <color=green>${CurrentCoin.currentValue + CurrentCoin.AddPerLevel}</color>";
                //ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("$0");
                //ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentCost}";

                UnlockVisualLevel(CurrentCoin.CurrentLevel);
            }

        } else{
            audioManager.GetComponent<AudioManager>().PlayAudioClip("denied");
        }
    }

    public double CalculateValue(double BaseUpgrade, double UpdgradesHave, double FreeUpgrades){

        double exponentialScalar = 0;
        double coinValue = 0;

        exponentialScalar = System.Math.Pow(1.15f, (UpdgradesHave - FreeUpgrades));
        coinValue = BaseUpgrade * exponentialScalar;

        return System.Math.Floor(coinValue);
    }

    private void CheckUpgradeProgress()
    {
        if (CurrentCoin.Name.Equals("Emerald Coin"))
        {
            // metal coin achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("Heavy");
        }
        if (CurrentCoin.Name.Equals("Uranium Coin"))
        {
            // gem achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("Sparkly");
        }
        if (CurrentCoin.Name.Equals("Mithril Coin"))
        {
            // radioactive achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("Radioactive");
        }
        if (CurrentCoin.Name.Equals("Mithril Coin"))
        {
            // mithril achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("Mythic");
        }
        if (CurrentCoin.Name.Equals("Adamantite Coin"))
        {
            // adamantite achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("NewHeights");
        }
        if (CurrentCoin.Name.Equals("Galaxy Coin"))
        {
            // galaxy achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("FullOfStars");
        }
        if (CurrentCoin.Name.Equals("God Coin"))
        {
            // god achievement
            steamManager.GetComponent<SteamManager>().CheckAchievement("ToInfinity");
        }
    }
    
    private void GodProgress()
    {
        gameObject.GetComponent<SaveManager>().godLevels += 1;

        if (gameObject.GetComponent<SaveManager>().godLevels == 100)
        {
            steamManager.GetComponent<SteamManager>().CheckAchievement("AndBeyond");
        }
    }

}
