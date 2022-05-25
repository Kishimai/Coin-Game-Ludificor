using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class SaveManager : MonoBehaviour
{

    public double currentCoin = 0;
    public double moneySpentOnUpgrades = 0;
    public bool upgradeCoins = false;

    public List<string> collectedItems = new List<string>();
    public bool intakeItems = false;

    private string saveDir;

    public TextAsset currentSaveData;

    public bool happenOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        saveDir = Application.streamingAssetsPath + "/saves/";
        Directory.CreateDirectory(saveDir);

        string saveFileName = saveDir + "save" + ".txt";

        if (File.Exists(saveFileName))
        {
            StartCoroutine(Load());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        string saveFileName = saveDir + "save" + ".txt";

        Debug.Log(saveFileName);

        currentCoin = GetComponent<UI_Manager>()._currentCoin;

        moneySpentOnUpgrades = GetComponent<ShopSystem>().moneySpent;

        collectedItems = GetComponent<ItemInventory>().collectedItems;

        File.WriteAllText(saveFileName, "[ITEMS]\n");

        foreach (string item in collectedItems)
        {
            File.AppendAllText(saveFileName, item + "\n");
        }

        File.AppendAllText(saveFileName, "[MONEY_SPENT]\n");

        File.AppendAllText(saveFileName, moneySpentOnUpgrades.ToString() + "\n");

        File.AppendAllText(saveFileName, "[MONEY]\n");

        File.AppendAllText(saveFileName, currentCoin.ToString() + "\n");

    }

    public void LoadData()
    {
        string saveFileName = saveDir + "save" + ".txt";

        if (File.Exists(saveFileName))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    private IEnumerator Load()
    {

        yield return null;

        string saveFileName = saveDir + "save" + ".txt";

        List<string> fileLines = File.ReadAllLines(saveFileName).ToList();

        for (int i = 0; i < fileLines.Count; ++i)
        {
            if (fileLines[i].Equals("[ITEMS]"))
            {
                intakeItems = true;
                ++i;
            }
            else if (fileLines[i].Equals("[MONEY_SPENT]"))
            {
                intakeItems = false;
                upgradeCoins = true;
                ++i;
            }
            else if (fileLines[i].Equals("[MONEY]"))
            {
                upgradeCoins = false;
                ++i;
            }

            if (intakeItems)
            {
                collectedItems.Add(fileLines[i]);
            }
        }

        GetComponent<ItemInventory>().loadedItems = collectedItems;
    }

}
