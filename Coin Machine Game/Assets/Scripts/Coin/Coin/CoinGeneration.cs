using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CoinGeneration : MonoBehaviour
{
    [BoxGroup("Core Output")]
    public List<CoinData> listedCoins = new List<CoinData>();
    [BoxGroup("Core Output")]
    public List<CoinData> CoinsAvail = new List<CoinData>();
    [BoxGroup("Core Input")]
    public CoinRowPrinter CoinPrinter;
    [BoxGroup("Core Input")]
    public CoinPlacement coinPlacement;

    [BoxGroup("Core Input")]
    public GameObject NewCoin;


    /// <summary>
    /// Returns a Random CoinData from List
    /// </summary>
    /// <returns>Returns Coindata</returns>
    public CoinData RandomCoin(){
        var gottenCoin = false;
        int GottenNumber = Random.Range(1, CoinsAvail.Count + 1);
        var currentNumber = 0;

        foreach(CoinData _data in CoinsAvail){
            currentNumber++;
            if(gottenCoin == false && currentNumber == GottenNumber){
                return _data;
            }
        }

        return null;
    }

    /// <summary>
    /// Add Coins to the List from the Given Data
    /// </summary>
    /// <param name="data">CoinData</param>
    public void AddCoin(CoinData data){
        listedCoins.Add(data);
    }

    /// <summary>
    /// Get data for Placement
    /// </summary>
    public void GetPlacementData(){
        CoinData _data = RandomCoin();
        coinPlacement.selectedCoin.GetComponent<Data_Interp>().data = _data;
        coinPlacement.selectedCoin.GetComponent<MeshRenderer>().material = _data.materialColor;
    }

    /// <summary>
    /// Changes Components of CoinPrinter.CurrentItem onto a Randomized one from the list
    /// </summary>
    public void returnObject(){
        var generatedNumber = Random.Range(1, CoinsAvail.Count);
        var numberHave = 0;

        foreach(CoinData _data in CoinsAvail){
            numberHave++;
            if(numberHave == generatedNumber){
                Debug.Log(_data.Name);
                CoinPrinter.currentItem.GetComponent<Data_Interp>().data = _data;
                CoinPrinter.currentItem.GetComponent<Renderer>().material = _data.materialColor;
            }
        }
    }

    // Called when an item should remove the lowest tier coin
    public void RemoveLowestTierCoin()
    {
        // Runs if there's at least 1 coin in CoinsAvail
        if (CoinsAvail.Count > 1)
        {
            // Grabs first coin in list
            CoinData lowestTier = CoinsAvail[0];
            
            // For each CoinData in CoinsAvail
            foreach (CoinData data in CoinsAvail)
            {
                // Runs if lowestTier.Order is greater than current data.Order in CoinsAvail
                // Eventually it will find the coin with the lowest order and keep that one
                if (lowestTier.Order > data.Order)
                {
                    // Assigns lowestTier to data
                    lowestTier = data;
                }
            }

            // Removes the lowestTier coin from CoinsAvail
            CoinsAvail.Remove(lowestTier);

        }
    }

    public CoinData GetHighestTierCoin()
    {
        CoinData highestTier = CoinsAvail[0];

        foreach(CoinData _data in CoinsAvail)
        {
            if (highestTier.Order < _data.Order)
            {
                highestTier = _data;
            }
        }

        return highestTier;
    }

}   
