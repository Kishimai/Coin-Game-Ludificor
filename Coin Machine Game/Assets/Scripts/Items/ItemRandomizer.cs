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

    //public string chosenItem;

    private Dictionary<string, string> chosenItem;

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
        return ChooseItem(RandomItemRarity());
    }

    private int RandomItemRarity()
    {
        int itemRarity;

        itemRarity = Random.Range(0, 99);

        return itemRarity;
    }

    private Dictionary<string, string> ChooseItem(int rarity)
    {
        if (rarity <= 49)
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
        else if (rarity <= 89)
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
        else if (rarity <= 99)
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
}
