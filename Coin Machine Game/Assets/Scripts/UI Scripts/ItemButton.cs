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

    public Color32 common;
    public Color32 uncommon;
    public Color32 rare;

    private Image image;

    public void Start()
    {
        image = GetComponent<Image>();
    }

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
                image.color = Color.white;
                return midasShard;

            case "midas_crystal":
                image.color = Color.green;
                return midasCrystal;

            case "midas_relic":
                image.color = Color.red;
                return midasRelic;

            case "peg_remove_mk1":
                image.color = common;
                return pegRemoveMk1;

            case "peg_remove_mk2":
                image.color = uncommon;
                return pegRemoveMk2;

            case "peg_remove_mk3":
                image.color = rare;
                return pegRemoveMk3;

            case "golden_peg":
                image.color = uncommon;
                return goldPeg;

            case "diamond_peg":
                image.color = rare;
                return diamondPeg;

            case "combo_peg":
                image.color = rare;
                return comboPeg;

            case "bomb_voucher":
                image.color = rare;
                return bombVoucher;

            case "tremor_voucher":
                image.color = rare;
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
