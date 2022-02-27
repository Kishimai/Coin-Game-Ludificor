using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Input")]
    public List<GameObject> Coin_list = new List<GameObject>();
    [BoxGroup("Input")]
    public UI_Manager UI;
    [BoxGroup("Input")]
    public CoinGeneration CoinGen;

    GameObject Mostleast; // Most Recent Gameobject that was made
    
    
    public void Start(){
        RestartData();
        ApplyInfoData();
    }

    private void Update() {

    }
    
    public void ApplyInfoData(){ // Setting infos of Each Coin Data
        foreach(GameObject _object in Coin_list){
            CoinData dataInterp = _object.GetComponent<Data_Interp>().data;
            _object.transform.GetChild(0).GetComponent<Image>().color = dataInterp.CoinColor;
            _object.transform.GetChild(2).GetComponent<TMP_Text>().text = dataInterp.Name;
            _object.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${dataInterp.currentValue}";

            for (int i = 0; i < 10; i++){
                _object.transform.GetChild(4).transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);                  
            }
            
            if(dataInterp.Unlocked == true){ // Means unlocked already for Setted Value
                UnlockVisualLevel(_object, 0);
            } else{
                _object.SetActive(false); // Awaiting for Unlock
            }
        }
    }

    public void RestartData(){
        foreach(GameObject _object in Coin_list){
            var data = _object.GetComponent<Data_Interp>().data;
            data.currentCost = data.BaseCost;
            data.CurrentLevel = 0;
            data.currentValue = data.StartingValue;

            if(data.name != "Copper"){
                data.Unlocked = false;
            } else {
                data.Unlocked = true;
                CoinGen.CoinsAvail.Add(_object.GetComponent<Data_Interp>().data);
            }

            data.levelsForFree = 0;
        }
    }

    public void UnlockVisualLevel(GameObject _object, int UnlockTill){
        var child = _object.transform.GetChild(4);
        child.GetChild(UnlockTill).transform.GetChild(0).gameObject.SetActive(true);

        if(!CoinGen.CoinsAvail.Contains(_object.GetComponent<Data_Interp>().data)){
            CoinGen.CoinsAvail.Add(_object.GetComponent<Data_Interp>().data);
        }
    }

    public void DeleteCoin(GameObject _Object){
        
    }

    public void UpgradeCoin(GameObject _object){
        var data = _object.GetComponent<Data_Interp>().data;
        if(UI._currentCoin >= data.currentCost && data.CurrentLevel != 10){
            UI._currentCoin -= data.currentCost;
            data.currentCost = CalculateValue(data.BaseCost, data.CurrentLevel, data.levelsForFree);
            data.currentValue += data.AddPerLevel;
            data.CurrentLevel++;
            UnlockVisualLevel(_object, data.CurrentLevel);
            _object.transform.GetChild(3).GetComponent<TMP_Text>().text = $"${data.currentValue}";  

            // Changed int from 7 to 9, now it unlocks next coin when upgrade reaches maximum
            if(data.CurrentLevel == 9){
                foreach(GameObject CoinObject in Coin_list){
                    if(data.Order + 1 > 6){

                    }

                    if(CoinObject.GetComponent<Data_Interp>().data.Order == data.Order + 1){ // Checking each Coin for the Next Order
                        CoinObject.SetActive(true); // Set Active so that it is Recognized within the System
                        UnlockVisualLevel(CoinObject, 0);
                    }
                }
            }
        } else{
            // Cant Upgrade anymore 10 has been reached
        }
    }

    public float CalculateValue(float BaseUpgrade, float UpdgradesHave, float FreeUpgrades){
        return BaseUpgrade *= 1.15f * UpdgradesHave - FreeUpgrades;
    }

        
    
}
