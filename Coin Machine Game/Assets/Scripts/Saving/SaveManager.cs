using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class SaveManager : MonoBehaviour
{

    public double currentCoin = 0;
    public int upgradesGot = 0;
    public bool upgradeCoins = false;
    public bool getMoney = false;
    public bool getCapsules = false;
    public bool getGodLevels = false;
    public bool getBombCount = false;
    public int unclaimedCapsules = 0;
    public int godLevels = 0;
    public int bombsExploded = 0;

    public List<string> collectedItems = new List<string>();
    public bool intakeItems = false;

    private string saveDir;

    public TextAsset currentSaveData;

    public bool happenOnce = false;

    public GameObject savedIcon;

    private GameObject steamManager;

    // Start is called before the first frame update
    void Start()
    {

        currentCoin = 0;
        upgradesGot = 0;
        unclaimedCapsules = 0;
        godLevels = 0;
        bombsExploded = 0;

        saveDir = Application.streamingAssetsPath + "/saves/";
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }
        
        string saveFileName = saveDir + "save" + ".txt";

        if (File.Exists(saveFileName))
        {
            StartCoroutine(Load());
        }

        steamManager = GameObject.FindGameObjectWithTag("steam_manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (bombsExploded == 100)
        {
            bombsExploded += 1;
            steamManager.GetComponent<SteamManager>().CheckAchievement("MainGoal");
        }
    }

    public void SaveData()
    {
        string saveFileName = saveDir + "save" + ".txt";

        Debug.Log(saveFileName);

        currentCoin = GetComponent<UI_Manager>()._currentCoin;

        upgradesGot = GetComponent<ShopSystem>().currentUpgrades;

        collectedItems = GetComponent<ItemInventory>().collectedItems;

        unclaimedCapsules = GetComponent<ItemInventory>().availablePrizes;

        File.WriteAllText(saveFileName, "[ITEMS]\n");

        foreach (string item in collectedItems)
        {
            File.AppendAllText(saveFileName, item + "\n");
        }

        File.AppendAllText(saveFileName, "[MONEY_SPENT]\n");

        File.AppendAllText(saveFileName, upgradesGot.ToString() + "\n");

        File.AppendAllText(saveFileName, "[MONEY]\n");

        File.AppendAllText(saveFileName, currentCoin.ToString() + "\n");

        File.AppendAllText(saveFileName, "[CAPSULES]\n");

        File.AppendAllText(saveFileName, unclaimedCapsules.ToString() + "\n");

        File.AppendAllText(saveFileName, "[GOD_LEVELS]\n");

        File.AppendAllText(saveFileName, godLevels.ToString() + "\n");

        File.AppendAllText(saveFileName, "[BOMBS]\n");

        File.AppendAllText(saveFileName, bombsExploded.ToString() + "\n");

        StartCoroutine(ShowIcon());

    }

    public IEnumerator ShowIcon()
    {
        savedIcon.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        savedIcon.SetActive(false);
    }

    public void LoadData()
    {
        string saveFileName = saveDir + "save" + ".txt";

        if (File.Exists(saveFileName))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    public void DeleteData()
    {
        string saveFileName = saveDir + "save" + ".txt";

        if (File.Exists(saveFileName))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            File.WriteAllText(saveFileName, "[ITEMS]\n");

            File.AppendAllText(saveFileName, "[MONEY_SPENT]\n");

            File.AppendAllText(saveFileName, "0\n");

            File.AppendAllText(saveFileName, "[MONEY]\n");

            File.AppendAllText(saveFileName, "0\n");

            File.AppendAllText(saveFileName, "[CAPSULES]\n");

            File.AppendAllText(saveFileName, "0\n");

            File.AppendAllText(saveFileName, "[GOD_LEVELS]\n");

            File.AppendAllText(saveFileName, "0\n");

            File.AppendAllText(saveFileName, "[BOMBS]\n");

            File.AppendAllText(saveFileName, "0\n");

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
            if (fileLines[i].Equals("[MONEY_SPENT]"))
            {
                intakeItems = false;
                upgradeCoins = true;
                ++i;
            }
            if (fileLines[i].Equals("[MONEY]"))
            {
                upgradeCoins = false;
                getMoney = true;
                ++i;
            }
            if (fileLines[i].Equals("[CAPSULES]"))
            {
                getMoney = false;
                getCapsules = true;
                ++i;
            }
            if (fileLines[i].Equals("[GOD_LEVELS]"))
            {
                getCapsules = false;
                getGodLevels = true;
                ++i;
            }
            if (fileLines[i].Equals("[BOMBS]"))
            {
                getGodLevels = false;
                getBombCount = true;
                ++i;
            }

            if (intakeItems)
            {
                collectedItems.Add(fileLines[i]);
            }
            if (upgradeCoins)
            {
                upgradesGot = Convert.ToInt32(fileLines[i]);
            }
            if (getMoney)
            {
                currentCoin = Convert.ToDouble(fileLines[i]);
            }
            if (getCapsules)
            {
                unclaimedCapsules = Convert.ToInt32(fileLines[i]);
            }
            if (getGodLevels)
            {
                godLevels = Convert.ToInt32(fileLines[i]);
            }
            if (getBombCount)
            {
                bombsExploded = Convert.ToInt32(fileLines[i]);
            }
        }

        GetComponent<ItemInventory>().loadedItems = collectedItems;

        GetComponent<ShopSystem>().loadedUpgrades = upgradesGot;
        GetComponent<ShopSystem>().loadUpgrades = true;

        //GetComponent<UI_Manager>()._currentCoin = currentCoin;

        GetComponent<UI_Manager>().coim = currentCoin;

        GetComponent<ItemInventory>().availablePrizes = unclaimedCapsules;
    }

}
