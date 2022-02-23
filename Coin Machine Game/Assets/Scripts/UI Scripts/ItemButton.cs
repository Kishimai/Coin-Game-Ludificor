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

        Dictionary<string, string> unformattedDict = selectedItemDict;
        Dictionary<string, string> formattedDict;
        string itemScriptName = "";
        string itemName = "";
        string itemDescription = "";

        foreach (KeyValuePair<string, string> dict in unformattedDict)
        {
            itemScriptName = dict.Key;
            itemName = dict.Key;
            itemDescription = dict.Value;
        }

        string[] words = itemName.Split('_');

        for (int i = 0; i < words.Length; ++i)
        {
            string capitalizedWord = char.ToUpper(words[i][0]) + words[i].Substring(1);
            words[i] = capitalizedWord;
        }

        itemName = string.Join(" ", words);

        formattedDict = new Dictionary<string, string>
        {
            {itemName, itemDescription}
        };
        

        foreach (KeyValuePair<string, string> item in formattedDict)
        {

            buttonText.text = item.Key;
            description.text = item.Value;

            selectedItem = itemScriptName;
        }
    }

    public void GetItem()
    {
        GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>().newItem = selectedItem;
    }
}
