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

    // Added palladium and styrofoam coins
    public int palladiumCoins;
    public int styrofoamCoins;

    private float styrofoamValue = 0;
    private float palladiumValue = 0.2f;

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

        foreach (CoinData _data in CoinsAvail)
        {
            currentNumber++;
            if (gottenCoin == false && currentNumber == GottenNumber)
            {
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
        // Checks if player has palladium coins
        if (palladiumCoins > 0)
        {
            DetermineIfSpecial();
        }

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

    // Grabs the highest tier coin in CoinsAvail
    public CoinData GetHighestTierCoin()
    {
        // Gets the first coin in CoinsAvail to start comparing
        CoinData highestTier = CoinsAvail[0];

        foreach(CoinData _data in CoinsAvail)
        {
            // Runs if highestTier.Order is less than the current data
            if (highestTier.Order < _data.Order)
            {
                // Assigns current data to highestTier
                highestTier = _data;
            }
        }

        return highestTier;
    }

    public CoinData GetLowestTierCoin()
    {
        CoinData lowestTier = CoinsAvail[0];

        foreach (CoinData _data in CoinsAvail)
        {
            if (lowestTier.Order > _data.Order)
            {
                lowestTier = _data;
            }
        }

        return lowestTier;
    }

    // Determines if coin should be palladium, styrofoam, etc
    public void DetermineIfSpecial()
    {
        // Makes new list of coins
        List<string> coins = new List<string>();

        // Adds "normal_coin" for each data in CoinsAvail
        foreach (CoinData _data in CoinsAvail)
        {
            coins.Add("normal_coin");
        }
        // Adds "palladium_coin" for each palladium coin
        for (int i = 0; i < palladiumCoins; ++i)
        {
            coins.Add("palladium_coin");
        }
        // Adds "styrofoam_coin" for each styrofoam coin
        for (int i = 0; i < styrofoamCoins; ++i)
        {
            coins.Add("styrofoam_coin");
        }

        // Picks random coin from new list
        string selectedCoin = coins[Random.Range(0, coins.Count)];

        // Runs if random coin is NOT "normal_coin"
        if (selectedCoin.Equals("palladium_coin"))
        {
            coinPlacement.spells.Add("palladium");
        }
        // Runs if random coin is NOT "normal_coin"
        if (selectedCoin.Equals("styrofoam_coin"))
        {
            coinPlacement.spells.Add("styrofoam");
        }
    }

    // Removes x number of styrofoam coins from collection
    public void RemoveStyrofoam(int numToRemove = 1)
    {
        if (styrofoamCoins > 0)
        {
            styrofoamCoins -= numToRemove;
        }
        
        if (styrofoamCoins < 0)
        {
            styrofoamCoins = 0;
        }
    }

    public void IncreaseStyrofoamValue(float value)
    {
        styrofoamValue += value;
    }
    
    public float GetStyrofoamValue()
    {
        if (styrofoamValue <= 0)
        {
            styrofoamValue = 0.1f;
        }
        return styrofoamValue;
    }

    public void IncreasePalladiumValue(float value)
    {
        palladiumValue += value;
    }

    public float GetPalladiumValue()
    {
        return palladiumValue;
    }

}   
