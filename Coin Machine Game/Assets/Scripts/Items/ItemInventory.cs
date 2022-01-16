using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{

    public List<string> collectedItems = new List<string>();
    public float coinValueModifier;
    public string newItem = "";
    public GameObject collector;

    // Start is called before the first frame update
    void Start()
    {
        coinValueModifier = 1.0f;
        collector = GameObject.FindGameObjectWithTag("coin_destroyer");
    }

    // Update is called once per frame
    void Update()
    {

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
            // Runs if new item is midas shard
            case "midas_shard":
                // Increases value modifier by 1%
                coinValueModifier += 0.01f;
                break;

            // Runs if new item is midas relic
            case "midas_relic":
                // Increases value modifier by 5%
                coinValueModifier += 0.05f;
                break;

            // Runs if new item's tag does not match a case in this switch statement
            default:
                Debug.LogWarning("New item not known by IntakeItem() in script on: " + gameObject.name);
                break;
        }

        // Returns the new item, assigning it to the list
        return newItem;
    }

}
