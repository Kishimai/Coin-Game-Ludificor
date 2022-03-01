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

    private int currentOrder;


    public void Start(){
        RestartData();
    }

    private void Update() {
        if(UpdatedCoin != CurrentCoin){
            ApplyInfoData();
        }
    }
    
    public void ApplyInfoData(){ // Setting infos of Each Coin Data
        ObjectContent.transform.GetChild(0).GetComponent<Image>().color = CurrentCoin.CoinColor;
        ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("0");
        ObjectContent.transform.GetChild(2).GetComponent<TMP_Text>().text = CurrentCoin.Name;
        ObjectContent.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentValue} > <color=green>${CurrentCoin.currentValue+CurrentCoin.AddPerLevel}</color>";

        for (int i = 0; i < 10; i++){ // Disabiling Visual Levels
            ObjectContent.transform.GetChild(4).transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);                  
        }

        UpdatedCoin = CurrentCoin;
        UnlockVisualLevel(0);
    }

    public void RestartData(){
        // Data did not reset correctly at the start of each game
        // Instead of using _data on all of these lines, it used CurrentCoin
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
        var child = ObjectContent.transform.GetChild(4);
        child.GetChild(UnlockTill).transform.GetChild(0).gameObject.SetActive(true);

        if(!CoinGen.CoinsAvail.Contains(CurrentCoin)){
            CoinGen.CoinsAvail.Add(CurrentCoin);
        }
    }

    public void UpgradeCoin(){
        if(UI._currentCoin >= CurrentCoin.currentCost && CurrentCoin.CurrentLevel != 10){

            CurrentCoin.CurrentLevel++; // <--- Moving level increase here fixed the bug of second coin purchase being $0
            UI._currentCoin -= CurrentCoin.currentCost;
            CurrentCoin.currentCost = CalculateValue(CurrentCoin.BaseCost, CurrentCoin.CurrentLevel, CurrentCoin.levelsForFree);
            CurrentCoin.currentValue += CurrentCoin.AddPerLevel;
            //CurrentCoin.CurrentLevel++; <--- Level previously increased here
            ObjectContent.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${CurrentCoin.currentValue} > <color=green>${CurrentCoin.currentValue + CurrentCoin.AddPerLevel}</color>";
            ObjectContent.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = CurrentCoin.currentCost.ToString("0");

            // Changed int from 7 to 9, now it unlocks next coin when upgrade reaches maximum
            if (CurrentCoin.CurrentLevel == 9){
                ++currentOrder; // Increases currentOrder by 1, every time coin a coin is fully upgraded
                foreach(CoinData _data in CoinData_List){
                    if(CurrentCoin.Order != 12){
                        if(currentOrder == _data.Order){ // Now tracks current order in shop and compares to data in list
                            CurrentCoin = _data;
                        }
                        // Had to change this if statement, as it was skipping coins in the list
                        //if (CurrentCoin.Order + 1 == _data.Order)
                        //{
                        //    CurrentCoin = _data;
                        //}
                    } else{
                        // Cant Upgrade Maxed out all coins.
                    }
                }
            }

            UnlockVisualLevel(CurrentCoin.CurrentLevel);
        } else{
            // Cant Upgrade anymore 10 has been reached
        }
    }

    public float CalculateValue(float BaseUpgrade, float UpdgradesHave, float FreeUpgrades){
        return BaseUpgrade *= 1.15f * UpdgradesHave - FreeUpgrades;
    }

        
    
}
