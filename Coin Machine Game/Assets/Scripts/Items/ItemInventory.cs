using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    // ------------------------- All possible items in the game (scripts dealing with items may pull from this) ------------------------- //
    //public string[] commonItems = new string[] { "midas_shard", "peg_remove_mk1" };
    //public string[] uncommonItems = new string[] { "midas_crystal", "peg_remove_mk2", "golden_peg", "combo_peg" };
    //public string[] rareItems = new string[] { "midas_relic", "peg_remove_mk3", "diamond_peg" };

    public Dictionary<string, string> commonItems;
    public Dictionary<string, string> uncommonItems;
    public Dictionary<string, string> rareItems;

    public List<string> allSpells = new List<string>() { "bomb", "tremor" };

    // ------------------------- Item Inventory ------------------------- //
    public List<string> collectedItems = new List<string>();
    public List<string> collectedSpells = new List<string>();
    public float coinValueModifier;
    public string newItem = "";
    public GameObject collector;
    // Number of prizes that the player can pick from the prize counter
    public int availablePrizes;
    public Text availablePrizesText;

    public GameObject pegManager;

    public GameObject playerCamera;

    private bool playerInput;

    public GameObject[] itemButtons;

    private GameObject eventManager;

    public GameObject collectionsMenu;

    private bool palladiumStyroUnlocked = false;

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
    public Sprite cleanupInitiative;
    public Sprite pollution;
    public Sprite polishingKit;
    public Sprite palladiumPeg;

    // Start is called before the first frame update
    void Start()
    {
        availablePrizes = 0;
        coinValueModifier = 1.0f;
        collector = GameObject.FindGameObjectWithTag("coin_destroyer");
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        CompileItems();
    }

    // Update is called once per frame
    void Update()
    {

        availablePrizesText.text = string.Format("Available Prizes: {0}", availablePrizes);

        // The name of the item that the player picks will be assigned to newItem
        // When newItem is assigned, it will add that item to the list of collected items and change variables like coinValueModifier

        // Runs if player picks an item from the 3 choices that the item capsule gives them
        if (newItem != "")
        {
            foreach (GameObject button in itemButtons)
            {
                button.GetComponent<ItemButton>().RollNew();
            }

            --availablePrizes;

            // Adds returned item to list of collected items
            collectedItems.Add(IntakeItem());


            if (newItem.Equals("bomb_voucher"))
            {
                collectedSpells.Add("bomb");
                GetSpell("bomb");
            }
            else if (newItem.Equals("tremor_voucher"))
            {
                collectedSpells.Add("tremor");
                GetSpell("tremor");
            }
            else if (newItem.Equals("bulldoze_voucher"))
            {
                collectedSpells.Add("bulldoze");
                GetSpell("bulldoze");
            }

            CheckRemainingPrizes();

            // Makes sure to remove item from newItem after its been added to the list
            newItem = "";

        }

        collector.GetComponent<DeleteCoins>().valueModifier = coinValueModifier;
    }

    void CompileItems()
    {
        commonItems = new Dictionary<string, string>
        {
            { "midas_shard", "Increases value of all coins by 1%" },
            {"peg_remove_mk1", "Removes 1 normal peg from the backboard" },
            {"blitz_duration", "Increases duration of coin blitz by 1 second" },
            {"surge_duration", "Increases duration of power surge by 1 second" }
        };

        uncommonItems = new Dictionary<string, string>
        {
            { "midas_crystal", "Increases value of all coins by 5%" },
            { "peg_remove_mk2", "Removes 2 normal pegs from the backboard" },
            { "golden_peg", "Converts 1 normal peg to a gilded version, doubling value of coins that touch it" },
            { "more_coins", "Adds 5% chance to drop an additional coin each coin placement (Max: 500%)" },
            { "bomb_voucher", "Gives bomb coin which can be detonated" },
            { "tremor_voucher", "Gives tremor coin which will shake the machine when placed" },
            { "better_prizes", "Increases chance of getting uncommon items by 5%" }
        };

        rareItems = new Dictionary<string, string>
        {
            { "midas_relic", "Increases value of all coins by 10%" },
            { "peg_remove_mk3", "Removes 3 normal pegs from the backboard" },
            { "diamond_peg", "Converts 1 normal peg to a diamond version, tripling value of coins that touch it" },
            { "combo_peg", "Converts 1 normal peg to a combo version, doubling value of coins that touch it (Effect stacks)" },
            { "bulldoze_voucher", "Gives bulldoze coin which will cause coin pusher to force all coins into collection" },
            { "coin_storm", "Adds 25% chance to drop an additional coin each coin placement (Max: 500%)" },
            { "great_prizes", "Increases chance of getting rare items by 1%" },
            { "vip_voucher", "Removes lowest tier coin from drop pool" },
            { "palladium_coin", "Adds 1 palladium coin, 1 styrofoam coin to drop pool, and unlocks new items" }
        };
    }

    // Responsible for determining the item the player chose, and altering values based on that
    string IntakeItem()
    {

        switch (newItem)
        {
            // --------------- MIDAS ITEMS --------------- //

            // Runs if new item is midas shard
            case "midas_shard":
                // Increases value modifier by 1%
                coinValueModifier += 0.01f;
                collectionsMenu.GetComponent<Collections>().AddItem(midasShard, "midas_shard");
                return newItem;

            case "midas_crystal":
                // Increases value modifier by 5%
                coinValueModifier += 0.05f;
                collectionsMenu.GetComponent<Collections>().AddItem(midasCrystal, "midas_crystal");
                return newItem;

            // Runs if new item is midas relic
            case "midas_relic":
                // Increases value modifier by 10%
                coinValueModifier += 0.1f;
                collectionsMenu.GetComponent<Collections>().AddItem(midasRelic, "midas_relic");
                return newItem;

            // --------------- PEG REMOVER ITEMS --------------- //

            case "peg_remove_mk1":
                pegManager.GetComponent<PegManager>().DisablePegs(1);
                collectionsMenu.GetComponent<Collections>().AddItem(pegRemoveMk1, "peg_remove_mk1");
                return newItem;

            case "peg_remove_mk2":
                pegManager.GetComponent<PegManager>().DisablePegs(2);
                collectionsMenu.GetComponent<Collections>().AddItem(pegRemoveMk2, "peg_remove_mk2");
                return newItem;

            case "peg_remove_mk3":
                pegManager.GetComponent<PegManager>().DisablePegs(3);
                collectionsMenu.GetComponent<Collections>().AddItem(pegRemoveMk3, "peg_remove_mk3");
                return newItem;

            // --------------- PEG ALTERING ITEMS --------------- //
            case "golden_peg":
                pegManager.GetComponent<PegManager>().ChangePegAttributes("gold");
                collectionsMenu.GetComponent<Collections>().AddItem(goldPeg, "golden_peg");
                return newItem;

            case "diamond_peg":
                pegManager.GetComponent<PegManager>().ChangePegAttributes("diamond");
                collectionsMenu.GetComponent<Collections>().AddItem(diamondPeg, "diamond_peg");
                return newItem;

            case "combo_peg":
                pegManager.GetComponent<PegManager>().ChangePegAttributes("combo");
                collectionsMenu.GetComponent<Collections>().AddItem(comboPeg, "combo_peg");
                return newItem;

            // --------------- SPELL ITEMS --------------- //

            case "bomb_voucher":
                collectionsMenu.GetComponent<Collections>().AddItem(bombVoucher, "bomb_voucher");
                return newItem;

            case "tremor_voucher":
                collectionsMenu.GetComponent<Collections>().AddItem(tremorVoucher, "tremor_voucher");
                return newItem;

            case "bulldoze_voucher":
                collectionsMenu.GetComponent<Collections>().AddItem(bulldozeVoucher, "bulldoze_voucher");
                return newItem;

            // --------------- ADDITIONAL COIN DROP ITEMS --------------- //

            case "more_coins":
                playerCamera.GetComponent<CoinPlacement>().additionalDropChance += 5;
                collectionsMenu.GetComponent<Collections>().AddItem(moreCoin, "more_coins");
                return newItem;

            case "coin_storm":
                playerCamera.GetComponent<CoinPlacement>().additionalDropChance += 25;
                collectionsMenu.GetComponent<Collections>().AddItem(coinStorm, "coin_storm");
                return newItem;

            // --------------- DURATION ITEMS --------------- //

            case "blitz_duration":
                eventManager.GetComponent<EventsManager>().coinBlitzDuration += 1f;
                collectionsMenu.GetComponent<Collections>().AddItem(blitzDuration, "blitz_duration");
                return newItem;

            case "surge_duration":
                eventManager.GetComponent<EventsManager>().powerSurgeDuration += 1f;
                collectionsMenu.GetComponent<Collections>().AddItem(surgeDuration, "surge_duration");
                return newItem;

            // --------------- CHANCE INFLUENCING ITEMS --------------- //

            case "better_prizes":
                gameObject.GetComponent<ItemRandomizer>().IncreaseUncommonChance();
                collectionsMenu.GetComponent<Collections>().AddItem(uncommonDice, "better_prizes");
                return newItem;

            case "great_prizes":
                gameObject.GetComponent<ItemRandomizer>().IncreaseRareChance();
                collectionsMenu.GetComponent<Collections>().AddItem(rareDice, "great_prizes");
                return newItem;

            // --------------- COIN POOL INFLUENCING ITEMS --------------- //

            case "vip_voucher":
                gameObject.GetComponent<CoinGeneration>().RemoveLowestTierCoin();
                collectionsMenu.GetComponent<Collections>().AddItem(vipVoucher, "vip_voucher");
                return newItem;

            case "palladium_coin":
                gameObject.GetComponent<CoinGeneration>().palladiumCoins += 1;
                gameObject.GetComponent<CoinGeneration>().styrofoamCoins += 1;
                collectionsMenu.GetComponent<Collections>().AddItem(palladiumCoin, "palladium_coin");
                UnlockStyroAndPalladium();
                return newItem;

            // --------------- STYROFOAM ITEMS --------------- //

            case "useful_materials":
                gameObject.GetComponent<CoinGeneration>().IncreaseStyrofoamValue(0.1f);
                collectionsMenu.GetComponent<Collections>().AddItem(usefulMaterials, "useful_materials");
                return newItem;

            case "pollution":
                gameObject.GetComponent<CoinGeneration>().styrofoamCoins += 3;
                float value = gameObject.GetComponent<CoinGeneration>().GetStyrofoamValue() * 5;
                gameObject.GetComponent<CoinGeneration>().IncreaseStyrofoamValue(value);
                collectionsMenu.GetComponent<Collections>().AddItem(pollution, "pollution");
                return newItem;

            case "polishing_kit":
                gameObject.GetComponent<CoinGeneration>().IncreasePalladiumValue(0.01f);
                collectionsMenu.GetComponent<Collections>().AddItem(polishingKit, "polishing_kit");
                return newItem;

            case "palladium_peg":
                // replace peg with palladium peg
                pegManager.GetComponent<PegManager>().ChangePegAttributes("palladium");
                collectionsMenu.GetComponent<Collections>().AddItem(palladiumPeg, "palladium_peg");
                return newItem;

            case "cleanup_initiative":
                gameObject.GetComponent<CoinGeneration>().RemoveStyrofoam(1);
                collectionsMenu.GetComponent<Collections>().AddItem(cleanupInitiative, "cleanup_initiative");
                return newItem;

            // Runs if new item's tag does not match a case in this switch statement
            default:
                Debug.LogWarning("New item not known by IntakeItem() in script on: " + gameObject.name);
                return string.Format("Unknown Item: {0}", newItem);
        }
    }

    public void CheckRemainingPrizes()
    {

        if (availablePrizes <= 0)
        {
            availablePrizes = 0;

            GetComponent<UI_Manager>().Update_UI(4);

            if (collectedItems.Contains("combo_peg"))
            {
                //GetComponent<UI_Manager>().Update_UI(9);
                //StartCoroutine(UseComboPegs());
            }
            // If no available prizes and player did NOT get combo peg, resume machine so it doesnt remain paused when menu is forcefully closed
            else
            {
                //GetComponent<UI_Manager>().Update_UI(4);
                //eventManager.GetComponent<EventsManager>().ResumeMachine();
            }
        }

    }

    public void PlayerInput()
    {
        playerInput = true;
    }

    IEnumerator UseComboPegs()
    {
        // activate pointer
        IngamePointer pointer = playerCamera.GetComponent<IngamePointer>();

        pointer.enabled = enabled;

        //eventManager.GetComponent<EventsManager>().PauseMachine();

        while (collectedItems.Contains("combo_peg"))
        {
            if (playerInput)
            {
                playerInput = false;
                collectedItems.Remove("combo_peg");
            }

            yield return null;
        }

        pointer.enabled = !enabled;

        eventManager.GetComponent<EventsManager>().ResumeMachine();

        GetComponent<UI_Manager>().Update_UI(4);
    }

    public void OpenCapsule()
    {

        if (availablePrizes > 0)
        {
            GetComponent<UI_Manager>().Update_UI(8);

            foreach (GameObject button in itemButtons)
            {
                button.GetComponent<ItemButton>().RollNew();
            }
        }

    }

    public void GetSpell(string spell)
    {
        playerCamera.GetComponent<CoinPlacement>().UseSpell(spell);

        collectedSpells.Remove(spell);
    }

    private void UnlockStyroAndPalladium()
    {
        // MAKE STYROFOAM COIN VALUE DEFAULT TO 10% OF LOWEST COIN TIER VALUE WHEN THEY GET "useful_materials"

        if (!palladiumStyroUnlocked)
        {
            palladiumStyroUnlocked = true;
            // Add items to lists of rare, uncommon and commons
            commonItems.Add("useful_materials","Makes styrofoam 10% more valuable");
            commonItems.Add("polishing_kit", "Increases value of palladium coins by 1%");
            uncommonItems.Add("palladium_peg", "Converts 1 normal peg to palladium, turning coins into palladium coins");
            commonItems.Add("pollution", "Adds 3 styrofoam coins to drop pool and increases styrofoam value by 5X");
            rareItems.Add("cleanup_initiative", "Remove 1 styrofoam coin from drop pool");
        }
    }

}
