using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Core Input")]
    public List<GameObject> placeholder_data = new List<GameObject>();

    /// <sumamry>
    /// Calculates the next Total pay rate using the Algorithm
    /// </summary>
    public void CalculatePayRate(float BaseUp, float UpgradesHave, float FreeUpgrades){ 
        BaseUp *= 1.15f * UpgradesHave - FreeUpgrades;
    }

    public void Start() {
        Update_Data();
        Debug.Log("Test");
    }

    public void Update_Data(){ // Updates the Data's
        foreach(GameObject _data in placeholder_data){
            if(_data.GetComponent<Data_Interp>().data){
                if(_data.GetComponent<Data_Interp>().data.Unlocked) { // Checks if Data is Locked or Unlocked Naturally
                    if(_data.activeInHierarchy == false){ // Enables Progression, Adds Information
                        _data.SetActive(true);
                        Update_Information(_data.gameObject);
                    }
                } else{
                    _data.SetActive(false); // Disables Progression
                }
            } else{
                placeholder_data.Remove(_data); // First Passing // Removes GameObject that doesnt have CoinData from List
                Debug.LogError($"{_data.name} from List has no Coin Data.");
            }
        }
    }

    public void Update_Information(GameObject _data){ // Update Information About the Coin
        
    }

}