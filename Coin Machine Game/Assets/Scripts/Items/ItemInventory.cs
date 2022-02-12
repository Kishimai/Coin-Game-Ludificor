using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    // ------------------------- All possible items in the game (scripts dealing with items may pull from this) ------------------------- //
    public string[] commonItems = new string[] { "midas_shard", "peg_remove_mk1" };
    public string[] uncommonItems = new string[] { "midas_crystal", "peg_remove_mk2", "golden_peg", "combo_peg" };
    public string[] rareItems = new string[] { "midas_relic", "peg_remove_mk3", "diamond_peg" };

    // ------------------------- Item Inventory ------------------------- //
    public List<string> collectedItems = new List<string>();
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

    // Start is called before the first frame update
    void Start()
    {
        availablePrizes = 0;
        coinValueModifier = 1.0f;
        collector = GameObject.FindGameObjectWithTag("coin_destroyer");
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
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

            CheckRemainingPrizes();

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
                return newItem;

            case "midas_crystal":
                // Increases value modifier by 5%
                coinValueModifier += 0.05f;
                return newItem;

            // Runs if new item is midas relic
            case "midas_relic":
                // Increases value modifier by 10%
                coinValueModifier += 0.1f;
                return newItem;

            // --------------- PEG REMOVER ITEMS --------------- //

            case "peg_remove_mk1":
                pegManager.GetComponent<PegManager>().DisablePegs(1);
                return null;

            case "peg_remove_mk2":
                pegManager.GetComponent<PegManager>().DisablePegs(2);
                return null;

            case "peg_remove_mk3":
                pegManager.GetComponent<PegManager>().DisablePegs(3);
                return null;

            // --------------- PEG ALTERING ITEMS --------------- //
            case "golden_peg":
                pegManager.GetComponent<PegManager>().ChangePegAttributes("gold");
                // Run peg modify method in peg script
                // Peg names are pegX-pegY, where x is a number (keeps dictionary keys unique)
                // Randomly pick peg in dictionary
                // Value of key is a bool, true or false
                // Take peg in question and change its value to the opposite of what it currently has
                // Then set its active state to that bool

                // First look for keys with the (false) value
                // Compile all into new dict
                // If new dict has at least 1 key, pick a random one
                // Activate that peg and convert it to a golden version

                // If no pegs are found in that new dict, pick a random one in the normal dictionary and do the above process 
                // (minus setting active state and compilation of new dict)
                return newItem;

            case "diamond_peg":
                pegManager.GetComponent<PegManager>().ChangePegAttributes("diamond");
                // Run peg modify method in peg script
                // Peg names are pegX-pegY, where x is a number (keeps dictionary keys unique)
                // Randomly pick peg in dictionary
                // Value of key is a bool, true or false
                // Then set its active state to that bool
                // Take peg in question and change its value to the opposite of what it currently has

                // First look for keys with the (false) value
                // Compile all into new dict
                // If new dict has at least 1 key, pick a random one
                // Activate that peg and convert it to a diamond version

                // If no pegs are found in that new dict, pick a random one in the normal dictionary and do the above process
                // (minus setting active state and compilation of new dict)
                return newItem;

            case "combo_peg":
                return newItem;

            // --------------- TO BE ADDED ITEMS --------------- //

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

            if (collectedItems.Contains("combo_peg"))
            {
                GetComponent<UI_Manager>().Update_UI(9);
                StartCoroutine(UseComboPegs());
            }
            else
            {
                GetComponent<UI_Manager>().Update_UI(4);
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

        //GetComponent<UI_Manager>().Update_UI(9);

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

        //eventManager.GetComponent<EventsManager>().ResumeMachine();

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

}
