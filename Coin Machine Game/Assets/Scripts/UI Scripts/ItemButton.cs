using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public string selectedItem;

    public Dictionary<string, string> selectedItemDict;

    public GameObject descriptionObject;

    public Text buttonText;

    public Text description;

    public void RollNew()
    {
        selectedItemDict = GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemRandomizer>().RollNewItem();
        foreach (KeyValuePair<string, string> item in selectedItemDict)
        {
            buttonText.text = item.Key;
            description.text = item.Value;

            selectedItem = item.Key;
        }
    }

    public void GetItem()
    {
        GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>().newItem = selectedItem;
    }
}
