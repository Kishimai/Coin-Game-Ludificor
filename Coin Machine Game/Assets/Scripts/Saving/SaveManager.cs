using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{

    public double currentCoin = 0;
    public double moneySpentOnUpgrades = 0;
    public bool upgradeCoins = false;

    public List<string> collectedItems = new List<string>();
    public bool intakeItems = false;

    private string saveDir;

    public TextAsset currentSaveData;

    // Start is called before the first frame update
    void Start()
    {
        saveDir = Application.streamingAssetsPath + "/saves/";
        Directory.CreateDirectory(saveDir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        string saveFileName = saveDir + "save" + ".txt";

        currentCoin = GetComponent<UI_Manager>()._currentCoin;

        moneySpentOnUpgrades = GetComponent<ShopSystem>().moneySpent;

        collectedItems = GetComponent<ItemInventory>().collectedItems;

        File.WriteAllText(saveFileName, "[ITEMS]\n");

        foreach (string item in collectedItems)
        {
            File.AppendAllText(saveFileName, item + "\n");
        }

        File.AppendAllText(saveFileName, "[MONEY_SPENT]\n");

        File.AppendAllText(saveFileName, moneySpentOnUpgrades.ToString());

        File.AppendAllText(saveFileName, "[MONEY]\n");

        File.AppendAllText(saveFileName, currentCoin.ToString());

    }

    public void LoadData()
    {

    }

}
