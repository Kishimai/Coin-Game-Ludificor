using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Core Input")]
    public List<GameObject> placeholder_data = new List<GameObject>();
    [BoxGroup("Current Data's")]
    public bool FirstDataSet = false;

    /// <sumamry>
    /// Calculates the next Total pay rate using the Algorithm
    /// </summary>
    public float CalculatePayRate(float BaseUp, float UpgradesHave, float FreeUpgrades){ // Calculates value or? Price
        return BaseUp *= 1.15f * UpgradesHave - FreeUpgrades;
    }

    public void Start() {
        Update_Data();
    }

    public void Update_Data(){ // Updates the Data's
        foreach(GameObject _object in placeholder_data){
            var _data = _object.GetComponent<Data_Interp>().data;

            if(FirstDataSet == false){
                _data.currentCost = _data.BaseCost;
                _data.currentValue = _data.StartingValue;
                _data.CurrentLevel = 1;

                if(_data.Order != 1){
                    _data.Unlocked = false;
                }
            }

            if(_data){
                if(_data.Unlocked) { // Checks if Data is Locked or Unlocked Naturally
                    if(_object.activeInHierarchy == false){ // Enables Progression, Adds Information
                        _object.SetActive(true);
                        Update_Information(_object.gameObject);
                    }
                } else{
                    _object.SetActive(false); // Disables Progression
                }
            } else{
                placeholder_data.Remove(_object); // First Passing // Removes GameObject that doesnt have CoinData from List
                Debug.LogError($"{_data.name} from List has no Coin Data.");
            }
        }
        FirstDataSet = true;
    }

    public void Update_Information(GameObject _data){ // Update Information About the Coin
        var _Title = _data.GetComponent<Data_Interp>().data.Name;
        var _Price = _data.GetComponent<Data_Interp>().data.currentCost;
        var _Value = _data.GetComponent<Data_Interp>().data.currentValue;
        _data.transform.GetChild(3).GetComponent<Text>().text = _Title;
        _data.transform.GetChild(2).GetComponent<Text>().text = $"Current Buy Price | {_Price:0}";
        _data.transform.GetChild(4).GetComponent<Text>().text = $"Current Coin Value | {_Value:0}";
    }

    public void UpgradeCoin(GameObject Data){
        var _data = Data.transform.parent.GetComponent<Data_Interp>().data;
        if(_data.CurrentLevel < 5){
            _data.CurrentLevel += 1;
            _data.currentCost = CalculatePayRate(_data.BaseCost, _data.CurrentLevel, 0);
            _data.currentValue += _data.AddPerLevel;
            Update_Information(Data.transform.parent.gameObject);
        } else {
            foreach(GameObject _object in placeholder_data){
                var object_data = _object.GetComponent<Data_Interp>().data;
                if(_data.Order + 1 == object_data.Order){
                    object_data.Unlocked = true;
                    Update_Data();
                }
            }
        }
    }

}
