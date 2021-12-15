using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Core Input")]
    public List<GameObject> placeholder_data = new List<GameObject>();
    [BoxGroup("Current Output")]
    public CoinData _currentData;

    /// <sumamry>
    /// Calculates the next Total pay rate using the Algorithm
    /// </summary>
    public void CalculatePayRate(float BaseUp, float UpgradesHave, float FreeUpgrades){ 
        BaseUp *= 1.15f * UpgradesHave - FreeUpgrades;
    }

    public void Start() {
       // Update Current Datas   
    }

    public void Update_Data(){
        foreach(GameObject _data in placeholder_data){
            _currentData = _data.GetComponent<Data_Interp>().data;
        }
    }
}
