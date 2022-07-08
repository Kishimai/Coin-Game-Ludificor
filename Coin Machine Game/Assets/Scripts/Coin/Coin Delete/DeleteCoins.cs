using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCoins : MonoBehaviour
{


    // -------------------- DeleteCoins -------------------- //

    // DeleteCoins is assigned to the Coin Destroyer or Coin Collector objects, and deals with coins or items that fall into the collection area of the game
    // Any item that falls into this area, will have its information collected and passed to any script which uses it (player inventory for example)
    // After the collection, the item will be destroyed to prevent lag buildup

    // ----------------------------------------------------- //

    // Used to prevent coins from adding to the player's money value when gameplay is not yet ready
    // !(EventsManager is responsible for deciding game states)!
    public bool gameplayIsReady;

    // Used to track a players collected coins (ideally this info would be sent to another script which deals with player inventory)
    // Every time this number gets +X, give +X to the number stored in the player inventory script
    public int coinCounter;

    // Modifier for coin value (items like midas shard/relic affect this)
    public float valueModifier = 1.0f;

    public int itemLayer = 9;

    public GameObject gameManager;

    public GameObject eventManager;

    private GameObject steamManager;

    // Manager Script used for getting Coin Variable
    public UI_Manager _manager;

    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        // Ensures that this script knows that gameplay is not yet ready
        gameplayIsReady = false;

        gameManager = GameObject.FindGameObjectWithTag("game_manager");
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");

        cam = GameObject.FindGameObjectWithTag("MainCamera");

        steamManager = GameObject.FindGameObjectWithTag("steam_manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplayIsReady)
        {
            // TRACK COINS GATHERED IN HERE! NOT IN COLLISION EVENTS!
            
        }
    }

    // Calculates modifier for coin value
    private float CalculateModifier(Collider other)
    {
        float modifier = 0;

        float valueFromPegEffects = other.GetComponentInParent<CoinLogic>().totalValueModifier;

        if (valueFromPegEffects >= 500)
        {
            steamManager.GetComponent<SteamManager>().CheckAchievement("ComboTime");
        }

        modifier = valueModifier + valueFromPegEffects;

        return modifier;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Runs if object that collides with Coin Destroyer is a coin
        if (other.CompareTag("coin") && eventManager.GetComponent<EventsManager>().playerIsReady)
        {
            System.Math.Floor(_manager._currentCoin += other.GetComponentInParent<Data_Interp>().data.currentValue * CalculateModifier(other));
            ++coinCounter;

            other.GetComponentInParent<CoinLogic>().StopAllCoroutines();

            // Destroys the coin in the most recent collision event
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.CompareTag("item") && eventManager.GetComponent<EventsManager>().playerIsReady)
        {

            // Adds 1 available prize to ItemInventory
            gameManager.GetComponent<ItemInventory>().availablePrizes += 1;

            // Destroy item capsule
            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.CompareTag("bomb_coin") && eventManager.GetComponent<EventsManager>().playerIsReady)
        {
            gameManager.GetComponent<ItemInventory>().GetSpell("bomb");

            if (cam.GetComponent<CoinPlacement>().activeBombs.Count > 0)
            {
                cam.GetComponent<CoinPlacement>().activeBombs.RemoveAt(0);
            }

            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.CompareTag("black_hole"))
        {
            // get spell
            gameManager.GetComponent<ItemInventory>().GetSpell("blackhole");

            if (cam.GetComponent<CoinPlacement>().activeBlackHoles.Count > 0)
            {
                cam.GetComponent<CoinPlacement>().activeBlackHoles.RemoveAt(0);
            }

            Destroy(other.gameObject.transform.parent.gameObject);
            // remove active instances
            // destroy
        }

        if (other.gameObject.tag == "tremor_coin" && eventManager.GetComponent<EventsManager>().playerIsReady)
        {
            gameManager.GetComponent<ItemInventory>().GetSpell("tremor");

            Destroy(other.gameObject.transform.parent.gameObject);
        }
        
        if (other.CompareTag("bulldoze_coin") && eventManager.GetComponent<EventsManager>().playerIsReady)
        {
            gameManager.GetComponent<ItemInventory>().GetSpell("bulldoze");

            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.CompareTag("palladium_coin"))
        {
            double dataValue = other.GetComponentInParent<Data_Interp>().data.currentValue;
            float palladiumModifier = other.GetComponentInParent<CoinLogic>().palladiumValue;

            double additionalCoin = System.Math.Ceiling(dataValue * palladiumModifier);

            dataValue += additionalCoin;

            double coinValue = System.Math.Round(dataValue * CalculateModifier(other));
            
            if (coinValue < 0)
            {
                coinValue = 0;
            }

            _manager._currentCoin += coinValue;

            Destroy(other.gameObject.transform.parent.gameObject);
        }

        if (other.CompareTag("styrofoam_coin"))
        {
            // Calculate modifier
            // If value is 0, do not add coins
            // If value is greater than zero, calculate modifier

            double dataValue = other.GetComponentInParent<Data_Interp>().data.currentValue;

            dataValue *= other.GetComponentInParent<CoinLogic>().styrofoamValue;

            double coinValue = System.Math.Round(dataValue * CalculateModifier(other));

            if (coinValue < 0)
            {
                coinValue = 0;
            }

            _manager._currentCoin += coinValue;

            Destroy(other.gameObject.transform.parent.gameObject);
        }

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "popped_peg")
        {
            double coinValue = 0;

            CoinData highestVal = gameManager.GetComponent<CoinGeneration>().GetHighestTierCoin();

            //double val = highestVal.currentValue * 0.2f;

            //coinValue = highestVal.currentValue + val;

            coinValue = highestVal.currentValue * 5;

            _manager._currentCoin += coinValue;

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "dark_matter")
        {
            _manager._currentCoin += collision.gameObject.GetComponent<DarkMatter>().value;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.tag == "coin")
        //{
        //    Destroy(collision.collider.gameObject);
        //}
    }

}
