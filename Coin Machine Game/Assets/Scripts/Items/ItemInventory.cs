using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    // ------------------------- All possible items in the game (scripts dealing with items may pull from this) ------------------------- //
    public string[] commonItems = new string[] { "midas_shard" };
    public string[] uncommonItems = new string[] { "midas_crystal" };
    public string[] rareItems = new string[] { "midas_relic" };

    // ------------------------- Item Inventory ------------------------- //
    public List<string> collectedItems = new List<string>();
    public float coinValueModifier;
    public string newItem = "";
    public GameObject collector;
    // Number of prizes that the player can pick from the prize counter
    public int availablePrizes;
    public Text availablePrizesText;

    // Start is called before the first frame update
    void Start()
    {
        availablePrizes = 0;
        coinValueModifier = 1.0f;
        collector = GameObject.FindGameObjectWithTag("coin_destroyer");
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
            // Adds returned item to list of collected items
            collectedItems.Add(IntakeItem());

            // Makes sure to remove item from newItem after its been added to the list
            newItem = "";

        }

        collector.GetComponent<DeleteCoins>().valueModifier = coinValueModifier;
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
                break;

            case "midas_crystal":
                // Increases value modifier by 5%
                coinValueModifier += 0.05f;
                break;

            // Runs if new item is midas relic
            case "midas_relic":
                // Increases value modifier by 10%
                coinValueModifier += 0.1f;
                break;

            // --------------- TO BE ADDED ITEMS --------------- //

            // Runs if new item's tag does not match a case in this switch statement
            default:
                Debug.LogWarning("New item not known by IntakeItem() in script on: " + gameObject.name);
                break;
        }

        // Returns the new item, assigning it to the list
        return newItem;
    }

    public void OpenCapsule()
    {
        if (availablePrizes > 0)
        {
            --availablePrizes;
            GetComponent<UI_Manager>().Update_UI(8);
        }
    }

}
