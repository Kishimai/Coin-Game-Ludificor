using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour
{

    public string[] commonItems;
    public string[] uncommonItems;
    public string[] rareItems;

    public string chosenItem;

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
    public string RollNewItem()
    {
        return ChooseItem(RandomItemRarity());
    }

    private int RandomItemRarity()
    {
        int itemRarity;

        itemRarity = Random.Range(0, 99);

        return itemRarity;
    }

    private string ChooseItem(int rarity)
    {
        if (rarity <= 49)
        {
            chosenItem = commonItems[Random.Range(0, commonItems.Length)];
        }
        else if (rarity <= 89)
        {
            chosenItem = uncommonItems[Random.Range(0, uncommonItems.Length)];
        }
        else if (rarity <= 99)
        {
            chosenItem = rareItems[Random.Range(0, rareItems.Length)];
        }

        return chosenItem;
    }
}
