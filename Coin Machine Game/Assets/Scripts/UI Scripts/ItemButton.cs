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
    public Sprite bulldozeVoucher;
    public Sprite moreCoin;
    public Sprite coinStorm;
    public Sprite blitzDuration;
    public Sprite surgeDuration;
    public Sprite uncommonDice;
    public Sprite rareDice;
    public Sprite vipVoucher;
    public Sprite palladiumCoin;
    public Sprite usefulMaterials;
    public Sprite polishingKit;
    public Sprite palladiumPeg;
    public Sprite pollution;
    public Sprite cleanupInitiative;
    public Sprite moreStyrofoam;
    public Sprite fasterDropping;
    public Sprite prizeRain;
    public Sprite prizeStorm;

    public Color32 common;
    public Color32 uncommon;
    public Color32 rare;

    public Image image;

    private void Start()
    {
        common = new Color32(common.r, common.g, common.b, 255);
        uncommon = new Color32(uncommon.r, uncommon.g, uncommon.b, 255);
        rare = new Color32(rare.r, rare.g, rare.b, 255);

        DetermineIcon("initialize");
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
                image.color = common;
                return midasShard;

            case "midas_crystal":
                image.color = uncommon;
                return midasCrystal;

            case "midas_relic":
                image.color = rare;
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
                image.color = uncommon;
                return bombVoucher;

            case "tremor_voucher":
                image.color = uncommon;
                return tremorVoucher;

            case "bulldoze_voucher":
                image.color = rare;
                return bulldozeVoucher;

            case "more_coins":
                image.color = uncommon;
                return moreCoin;

            case "coin_storm":
                image.color = rare;
                return coinStorm;

            case "blitz_duration":
                image.color = common;
                return blitzDuration;

            case "surge_duration":
                image.color = common;
                return surgeDuration;

            case "better_prizes":
                image.color = rare;
                return uncommonDice;

            case "great_prizes":
                image.color = rare;
                return rareDice;

            case "vip_voucher":
                image.color = rare;
                return vipVoucher;

            case "palladium_coin":
                image.color = rare;
                return palladiumCoin;

            case "useful_materials":
                image.color = common;
                return usefulMaterials;

            case "polishing_kit":
                image.color = common;
                return polishingKit;

            case "palladium_peg":
                image.color = rare;
                return palladiumPeg;

            case "pollution":
                image.color = rare;
                return pollution;

            case "cleanup_initiative":
                image.color = rare;
                return cleanupInitiative;

            case "more_styrofoam":
                image.color = common;
                return moreStyrofoam;

            case "faster_dropping":
                image.color = common;
                return fasterDropping;

            case "prize_rain":
                image.color = common;
                return prizeRain;

            case "prize_storm":
                image.color = rare;
                return prizeStorm;

            case "initialize":
                image.color = common;
                return null;

            default:
                return null;
        }

    }

    public void GetItem()
    {
        GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>().newItem = selectedItem;
    }
}
