using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Core Input")] // Placeholder data of each UI placeholder from Shop Panel
    public List<GameObject> placeholder_data = new List<GameObject>();
    [BoxGroup("Core Input")] // UI Manager Used for Communicating.
    public UI_Manager _manager;
    [BoxGroup("Core Input")] // Coin Generation used for Changing Components of CoinGeneration.CurrentItem
    public CoinGeneration generation;
    [BoxGroup("Current Data")] // Used for Data listing, Checking if Appropriate data is Listed
    public bool FirstDataSet = false;

    /// <sumamry>
    /// Calculates the next Total pay rate.
    /// </summary>
    public float CalculatePayRate(float BaseUp, float UpgradesHave, float FreeUpgrades){
        return BaseUp *= 1.15f * UpgradesHave - FreeUpgrades;
    }

    public void Start() {
        Update_Data(); // Updates the Listed Data by removing unavailble Coins.
    }

    private void Update() {
        if(Input.GetKey(KeyCode.BackQuote)){ // **Debugging** adds 100 coins per frame while BackQuote is Pressed
            _manager._currentCoin += 1000;
        }
    }

    /// <summary>
    /// Updates Information about the Shop Listed Coins.
    /// </summary>  
    public void Update_Data(){
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
                        generation.CoinsAvail.Add(_data);
                    }
                } else{
                    _object.SetActive(false); // Disables Progression
                }
            } else{
                placeholder_data.Remove(_object); // First Passing, Removes GameObject that doesnt have CoinData from List
                Debug.LogError($"{_data.name} from List has no Coin Data.");
            }
        }
        FirstDataSet = true;
    }

    /// <summary>
    /// Specifically Updates a Specific Shop Coin and their information.
    /// </summary>
    /// <param name="_data">The data passed in for a Update</param>
    public void Update_Information(GameObject _data){ // Update Information About the Coin
        var _Title = _data.GetComponent<Data_Interp>().data.Name; // Getting Values
        var _Price = _data.GetComponent<Data_Interp>().data.currentCost;
        var _Value = _data.GetComponent<Data_Interp>().data.currentValue;

        _data.transform.GetChild(2).GetComponent<Text>().text = $"{_Title}"; // Adding Values
        _data.transform.GetChild(1).GetComponentInChildren<Text>().text = $"BUY ✰ {_Price:0}";
    }

    /// <summary>
    /// Check Each Data if it is on upgrade Threshold
    /// </summary>
    /// <param name="_Object">Data Being Checked: UI</param>
    public void Check_Datas(GameObject _Object){
        var _data = _Object.transform.parent.GetComponent<Data_Interp>().data;

        if(_data.CurrentLevel == 8){
            foreach(GameObject _object in placeholder_data){
                var object_data = _object.GetComponent<Data_Interp>().data;
                if(_data.Order + 1 == object_data.Order){
                    object_data.Unlocked = true;
                    Update_Data();
                }
            }
        }
    }

    public void VisualLevel(int Lvl, GameObject data){ // Not the most Efficient way of Coding Visual Level Will be changed
        var Levels = data.transform.GetChild(4);
        Levels.GetChild(Lvl -= 1).gameObject.SetActive(true);
        Debug.Log("Passed");
    }


    /// <summary>
    /// Upgrades the Coin Data that is Passed in
    /// </summary>
    /// <param name="Data">Passed in Data to Check for Upgrade</param>
    public void UpgradeCoin(GameObject Data){
        var _data = Data.transform.parent.GetComponent<Data_Interp>().data;
        if(_manager._currentCoin >= _data.currentCost){

            // Saves cost of current upgrade to subtract currentCoin (fixes bug where currentCoin went into negative value)
            float temp = _data.currentCost;

            if(_data.CurrentLevel < 10){
                _data.CurrentLevel += 1;
                _data.currentCost = CalculatePayRate(_data.BaseCost, _data.CurrentLevel, 0);
                _data.currentValue += _data.AddPerLevel;
                Update_Information(Data.transform.parent.gameObject);
                _manager._currentCoin -= temp;
            }
            
            Check_Datas(Data);
            VisualLevel(_data.CurrentLevel, Data);
        }
    }

}
