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
    public GameObject NewCoin;

    public CoinData RandomCoin(){
        var gottenCoin = false;
        int GottenNumber = Random.Range(1, listedCoins.Count);
        var currentNumber = 0;
        foreach(CoinData _data in listedCoins){
            currentNumber++;

            if(gottenCoin == false && currentNumber == GottenNumber){
                return _data;
            }
        }

        return null;
    }

    public void AddCoin(CoinData data){
        listedCoins.Add(data);
    }

    public void returnObject(){
        var generatedNumber = Random.Range(1, CoinsAvail.Count);
        var numberHave = 0;

        foreach(CoinData _data in CoinsAvail){
            numberHave++;
            if(numberHave == generatedNumber){
                CoinPrinter.currentItem.GetComponent<Data_Interp>().data = _data;
                CoinPrinter.currentItem.GetComponent<Renderer>().material = _data.materialColor;
            }
        }
    }

}   
