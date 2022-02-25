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

    //public SpriteRenderer spriteRenderer;

    public GameObject itemImageRenderer;

    public Sprite itemImage;

    public Sprite midasShard;
    public Sprite midasCrystal;
    public Sprite midasRelic;
    public Sprite pegRemoveMk1;
    public Sprite pegRemoveMk2;
    public Sprite pegRemoveMk3;
    public Sprite goldPeg;
    public Sprite diamondPeg;
    public Sprite comboPeg;
    public Sprite bombVoucher;
    public Sprite tremorVoucher;

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
            itemImage = DetermineIcon(dict.Key);

            //spriteRenderer.sprite = itemImage;

            //itemImage.

            itemImageRenderer.GetComponent<Image>().sprite = itemImage;

            //itemImageRenderer.GetComponent<SpriteRenderer>().size = new Vector2(20, 20);

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

    private Sprite DetermineIcon(string item)
    {
        switch (item)
        {
            case "midas_shard":
                return midasShard;

            case "midas_crystal":
                return midasCrystal;

            case "midas_relic":
                return midasRelic;

            case "peg_remove_mk1":
                return pegRemoveMk1;

            case "peg_remove_mk2":
                return pegRemoveMk2;

            case "peg_remove_mk3":
                return pegRemoveMk3;

            case "golden_peg":
                return goldPeg;

            case "diamond_peg":
                return diamondPeg;

            case "combo_peg":
                return comboPeg;

            case "bomb_voucher":
                return bombVoucher;

            case "tremor_voucher":
                return tremorVoucher;

            default:
                return null;
        }

    }

    public void GetItem()
    {
        GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>().newItem = selectedItem;
    }
}
