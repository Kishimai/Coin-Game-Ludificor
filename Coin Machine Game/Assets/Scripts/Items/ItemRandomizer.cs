using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemRandomizer : MonoBehaviour
{

    //public string[] commonItems;
    //public string[] uncommonItems;
    //public string[] rareItems;

    public Dictionary<string, string> commonItems;
    public Dictionary<string, string> uncommonItems;
    public Dictionary<string, string> rareItems;

    private List<string> ignoredItems = new List<string>();

    public int commonRarity = 49;
    public int uncommonRarity = 89;
    public int rareRarity = 99;

    //public string chosenItem;

    private Dictionary<string, string> chosenItem;

    public GameObject pegManager;
    public GameObject playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        commonItems = GetComponent<ItemInventory>().commonItems;
        uncommonItems = GetComponent<ItemInventory>().uncommonItems;
        rareItems = GetComponent<ItemInventory>().rareItems;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call this method to pick a new item
    public Dictionary<string, string> RollNewItem()
    {
        Dictionary<string, string> newDict = ChooseItem(RandomItemRarity());
        Dictionary<string, string> dictCheck = null;

        foreach (KeyValuePair<string, string> item in newDict)
        {
            dictCheck = DetermineIfItemIsValid(item.Key);
        }

        if (dictCheck != null)
        {
            return dictCheck;
        }
        else
        {
            return newDict;
        }
    }

    private int RandomItemRarity()
    {
        int itemRarity;

        itemRarity = Random.Range(0, 99);

        return itemRarity;
    }

    private Dictionary<string, string> ChooseItem(int rarity)
    {
        if (rarity <= commonRarity)
        {
            string[] commonDictKeys = commonItems.Keys.ToArray();

            string chosenKey = commonDictKeys[Random.Range(0, commonDictKeys.Length)];

            foreach (KeyValuePair<string, string> item in commonItems)
            {
                if (chosenKey.Equals(item.Key))
                {
                    chosenItem = new Dictionary<string, string>
                    {
                        { item.Key, item.Value }
                    };
                }
            }

            //chosenItem = commonItems[Random.Range(0, commonItems.Length)];
        }
        else if (rarity <= uncommonRarity)
        {

            string[] uncommonDictKeys = uncommonItems.Keys.ToArray();

            string chosenKey = uncommonDictKeys[Random.Range(0, uncommonDictKeys.Length)];

            foreach (KeyValuePair<string, string> item in uncommonItems)
            {
                if (chosenKey.Equals(item.Key))
                {
                    chosenItem = new Dictionary<string, string>
                    {
                        { item.Key, item.Value }
                    };
                }
            }

            //chosenItem = uncommonItems[Random.Range(0, uncommonItems.Length)];
        }
        else if (rarity <= rareRarity)
        {

            string[] rareDictKeys = rareItems.Keys.ToArray();

            string chosenKey = rareDictKeys[Random.Range(0, rareDictKeys.Length)];

            foreach (KeyValuePair<string, string> item in rareItems)
            {
                if (chosenKey.Equals(item.Key))
                {
                    chosenItem = new Dictionary<string, string>
                    {
                        { item.Key, item.Value }
                    };
                }
            }
            //chosenItem = rareItems[Random.Range(0, rareItems.Length)];
        }

        return chosenItem;
    }

    private Dictionary<string, string> DetermineIfItemIsValid(string item)
    {
        Dictionary<string, string> newDict = null;

        // Temporary system to prevent items from appearing when their abilities cannot be used
        // REPLACE THIS WITH A METHOD WHICH TAKES THE ITEM AS INPUT AND SEARCHES FOR A NEW ITEM IN THAT RARITY CATEGORY
        // EXCLUDING THE PASSED ITEM
        if (item.Equals("peg_remove_mk1") && pegManager.GetComponent<PegManager>().unmodifiedPegs.Count == 0)
        {
            newDict = GetFromAvailable("common", "peg_remove");
        }
        else if (item.Equals("peg_remove_mk2") && pegManager.GetComponent<PegManager>().unmodifiedPegs.Count == 0)
        {
            newDict = GetFromAvailable("uncommon", "peg_remove");
        }
        else if (item.Equals("peg_remove_mk3") && pegManager.GetComponent<PegManager>().unmodifiedPegs.Count == 0)
        {
            newDict = GetFromAvailable("rare", "peg_remove");
        }
        else if (item.Equals("combo_peg") && pegManager.GetComponent<PegManager>().unmodifiedPegs.Count == 0)
        {
            newDict = GetFromAvailable("rare", "combo_peg");
        }
        else if (item.Equals("uncommon_dice") && commonRarity <= 9)
        {
            newDict = GetFromAvailable("uncommon", "uncommon_dice");
        }
        else if (item.Equals("rare_dice") && uncommonRarity <= 19)
        {
            newDict = GetFromAvailable("rare", "rare_dice");
        }
        else if (item.Equals("more_coins") && playerCamera.GetComponent<CoinPlacement>().guaranteedDrops >= playerCamera.GetComponent<CoinPlacement>().maxAdditionalDrops)
        {
            newDict = GetFromAvailable("uncommon", "more_coins");
        }
        else if (item.Equals("coin_storm") && playerCamera.GetComponent<CoinPlacement>().guaranteedDrops >= playerCamera.GetComponent<CoinPlacement>().maxAdditionalDrops)
        {
            newDict = GetFromAvailable("rare", "coin_storm");
        }

        if (newDict != null)
        {
            return newDict;
        }
        else
        {
            return null;
        }
    }

    public Dictionary<string, string> GetFromAvailable(string rarity, string itemToIgnore)
    {
        Dictionary<string, string> newDict = new Dictionary<string, string>();
        Dictionary<string, string> listOfItems;

        if (!ignoredItems.Contains(itemToIgnore))
        {
            ignoredItems.Add(itemToIgnore);
        }

        if (rarity.Equals("common"))
        {
            listOfItems = gameObject.GetComponent<ItemInventory>().commonItems;
        }
        else if (rarity.Equals("uncommon"))
        {
            listOfItems = gameObject.GetComponent<ItemInventory>().uncommonItems;
        }
        else
        {
            listOfItems = gameObject.GetComponent<ItemInventory>().rareItems;
        }

        bool includeItem = true;

        // Looks at all items and descriptions in the rarity category selected
        foreach (KeyValuePair<string, string> item in listOfItems)
        {

            // Checks if any item in the list of ignored items matches the current item in question
            foreach(string ignoredItem in ignoredItems)
            {
                // If item contains ignored item substring
                if (item.Key.Contains(ignoredItem))
                {
                    // If the item matches, tell it to not be included
                    includeItem = false;
                }
            }

            if (includeItem)
            {
                newDict.Add(item.Key, item.Value);
            }
        }

        string[] items = newDict.Keys.ToArray();
        string[] descriptions = newDict.Values.ToArray();

        int chosenItem = Random.Range(0, items.Length);

        newDict = new Dictionary<string, string>
        {
            { items[chosenItem], descriptions[chosenItem] }
        };

        return newDict;
    }

    public void IncreaseUncommonChance()
    {
        if (commonRarity > 10)
        {
            commonRarity -= 5;
        }
    }

    public void IncreaseRareChance()
    {
        // Only runs if uncommonRarity has more than 10 to work with
        if (uncommonRarity > 19)
        {
            uncommonRarity -= 5;
        }
        // Only runs if uncommonRarity has less than or equal to 10, and commonRarity has more than 10 to work with
        else if (commonRarity > 10)
        {
            commonRarity -= 5;
        }
    }
}
