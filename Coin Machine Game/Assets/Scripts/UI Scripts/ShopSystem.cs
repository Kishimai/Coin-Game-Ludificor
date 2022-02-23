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
    

    public void Start(){
        ApplyInfoData();
    }
    
    public void ApplyInfoData(){ // Setting infos of Each Coin Data
        foreach(GameObject _object in Coin_list){
            CoinData dataInterp = _object.GetComponent<Data_Interp>().data;
            _object.transform.GetChild(0).GetComponent<Image>().color = dataInterp.CoinColor;
            _object.transform.GetChild(2).GetComponent<TMP_Text>().text = dataInterp.Name;
            _object.transform.GetChild(3).GetComponent<TMP_Text>().text = dataInterp.StartingValue.ToString();
            
            if(dataInterp.Unlocked == true){ // means unlocked already for Setted Value
                UnlockVisualLevel(_object, 1);
            }
        }
    }

    public void UnlockVisualLevel(GameObject Object, int UnlockTill){ // Will unlock Levels Trying to figure out how to set the level and it'll recognize it
        
    }

    public void UpgradeCoin(GameObject _object){
        
    }

    public float CalculateValue(float BaseUpgrade, float UpdgradesHave, float FreeUpgrades){
        return BaseUpgrade *= 1.15f * UpdgradesHave - FreeUpgrades;
    }

        
    
}
