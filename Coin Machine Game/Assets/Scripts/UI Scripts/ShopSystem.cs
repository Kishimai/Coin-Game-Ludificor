using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Input - Main")]
    public List<GameObject> Coin_list = new List<GameObject>();

    public void Start(){
        UnlockVisualLevel(null, 10);
    }
    
    public void ApplyInfoData(){
        foreach(GameObject _object in Coin_list){
            CoinData dataInterp = _object.GetComponent<Data_Interp>().data;
            _object.transform.GetChild(0).GetComponent<Image>().material = dataInterp.materialColor;
            _object.transform.GetChild(2).GetComponent<TMP_Text>().text = dataInterp.Name;
            _object.transform.GetChild(3).GetComponent<TMP_Text>().text = dataInterp.StartingValue.ToString();
            
            if(dataInterp.Unlocked == true){ // means unlocked already for Setted Value
                UnlockVisualLevel(_object, 1);
            }
        }
    }

    public void UnlockVisualLevel(GameObject Object, int UnlockTill){
        
    }
    
}
