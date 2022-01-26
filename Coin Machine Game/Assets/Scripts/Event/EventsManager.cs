

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{


    // -------------------- EventsManager -------------------- //

    // EventsManager handles the activation of and deactivation of game states, as well as special events which affect gameplay
    // Initial build of the coins, effects which alter the coin pusher's functionality, and money multiplier events are to be primarily handled here

    // ------------------------------------------------------- //


    // Accesses player camera object
    // REPLACE THIS WITH A CAMERA OBJECT INSTEAD OF A GAME OBJECT
    public GameObject playerCamera;
    // Accesses coin pusher object
    public GameObject coinPusher;
    // Accesses coin printer object 
    public GameObject coinPrinter;
    // Accesses coin destroyer object
    public GameObject coinDestroyer;
    // Accesses the item builder object
    public GameObject itemBuilder;

    // Initialization phase is used when the game is preparing the scene
    public bool initializationPhase;
    // Gameplay phase comes after initialization
    public bool gameplayPhase;

    // Used to track when the coin printer is finished filling the game board
    public bool printerIsFinished;

    // Used to track when the item builder is finished making the initial items
    public bool itemBuilderIsFinished;

    // Used to determine when the player is ready to play
    public bool playerIsReady;

    // Used to stop the pusher from moving during initialization phase, or any specific event which requires it
    public bool allowPusherMovements;

    // Time until next event check should occour
    public float timeUntilNextEventCheck;
    // After event check occours, reset its value to waitTime
    public float waitTime;

    // Possible events given 100 total elements
    // If events work based on random chance: If event should happen 25% of the time, add 25 instances of it to the array
    // Randomly pick an element from the array
    public string[] possibleEvents = new string[100];
    //public List<string> possibleEvents = new List<string>();
    public string chosenEvent = "";

    // Number of times these events will appear in the array of possible events
    public int coinBlitzProbability;
    public int itemRainProbability;
    public int powerSurgeProbability;
    public int jackpotProbability;

    // Remaining duration of current running event
    public float currentEventDuration;

    // Total duration of these events
    public float coinBlitzDuration;
    public float itemRainDuration;
    public float powerSurgeDuration;
    public float jackpotDuration;

    // Used during the blitz event to alter the coin placement cooldown so coins can be placed faster
    public float coinPlacementCooldown;

    public float surgePusherSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Locates and assigns the player camera to playerCamera
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // Locates and assigns the coin pusher to coinPusher
        coinPusher = GameObject.FindGameObjectWithTag("coin_pusher");
        // Locates and assigns the coin printer to coinPrinter
        coinPrinter = GameObject.FindGameObjectWithTag("coin_printer");
        // Locates and assigns the coin destroyer to coinDestroyer
        coinDestroyer = GameObject.FindGameObjectWithTag("coin_destroyer");
        // Locates and assigns the item builder to itemBuilder
        itemBuilder = GameObject.FindGameObjectWithTag("item_builder");

        // Enables the initialization phase
        initializationPhase = true;
        timeUntilNextEventCheck = waitTime;
        CompileEvents();
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the if statement in InitializeGameBoard easier to read
        // It sets printerIsFinished to initialBuildFinished in the coin printer. This is used to accurately track when the coin printer has completed its initialization tasks
        printerIsFinished = coinPrinter.GetComponent<CoinRowPrinter>().initialBuildFinished;

        itemBuilderIsFinished = itemBuilder.GetComponent<ItemBuilder>().initialBuildFinished;

        // Runs the initialization method
        if (initializationPhase)
        {
            InitializeGameBoard();
        }

        // Runs the gameplay method
        if (gameplayPhase)
        {
            GameplayPhase();
        }

    }

    void InitializeGameBoard()
    {
        // Runs if printer has finished making coins
        if (printerIsFinished && itemBuilderIsFinished)
        {
            // Ensures gravity is enabled
            Physics.gravity = new Vector3(0, -20, 0);

            // Allows the coin pusher to move
            coinPusher.GetComponent<CoinPusher>().allowingMovement = true;
            // Forces the coin printer to stop with its initialization phase
            coinPrinter.GetComponent<CoinRowPrinter>().initializeCoins = false;

            // Switches from initialization to gameplay
            initializationPhase = false;
            gameplayPhase = true;
        }
        // Runs if the printer is still working
        else
        {
            // Disables gravity
            Physics.gravity = new Vector3(0, 0, 0);

            // Prevents the coin pusher from moving during the initialization phase
            coinPusher.GetComponent<CoinPusher>().allowingMovement = false;
            // Tells the coin printer to start its initialization phase
            coinPrinter.GetComponent<CoinRowPrinter>().initializeCoins = true;
        }
    }

    // Runs when player presses the "play" button
    public void ReadyUp()
    {
        playerIsReady = true;
    }

    void GameplayPhase()
    {
        // Tells the player camera that gameplay is ready (this would allow the camera to remove any "Loading" screen and prepare the UI)
        playerCamera.GetComponent<CoinPlacement>().gameplayIsReady = true;
        // Tells the coin destroyer to start tracking coins that fall into it so the player can gather money
        coinDestroyer.GetComponent<DeleteCoins>().gameplayIsReady = true;

        // If an event is still going on, the time until next event will not decrease
        if (currentEventDuration <= 0)
        {
            timeUntilNextEventCheck -= Time.deltaTime;

            playerCamera.GetComponent<CoinPlacement>().blitzEvent = false;
            coinPusher.GetComponent<CoinPusher>().surgeEvent = false;

            chosenEvent = "";

        }
        // If an event is going on, decrease its remaining duration
        else
        {
            currentEventDuration -= Time.deltaTime;
        }

        // Runs if it is time to cause a game event
        if (timeUntilNextEventCheck <= 0)
        {
            PlayEvent();

            timeUntilNextEventCheck = waitTime;
        }

    }

    void CompileEvents()
    {
        // Add all possible events
        // If events should be chosen randomly;
        // List should have a max size of 100

        for (int i = 0; i < possibleEvents.Length; ++i)
        {
            // work through each event
            // if coin blitz should happen 25% of the time, set coinBlitzProbability to 25

            // add coin blitz a total of 25 times
            // add item rain a total of X times
            // add power surge a total of Y times
            // add jackpot a total of Z times

            // For testing purposes right now, add coin blitz 100 times

            // !!!!MAKE SURE OF THIS!!!!
            // The string added to the array MUST EXACTLY match the method name of the event
            // So if the CoinBlitz() event should run, the string should be "CoinBlitz" NOT "coinblitz", "coinBlitz", "Coin Blitz", etc.
            // Remember that the combined value of all event probability variables cannot exceed 100, or events wont appear as often as desired
            // For example: coinblitz cannot be 60 if power surge is going to be 50, what will happen is coin blitz will have a 60% chance, and power surge will get a 40% chance

            // First populates possibleEvents array with coin blitz events
            if (coinBlitzProbability > 0)
            {
                --coinBlitzProbability;
                possibleEvents[i] = "CoinBlitz";
            }
            // Then populates possibleEvents array with power surge events
            else if (powerSurgeProbability > 0)
            {
                --powerSurgeProbability;
                possibleEvents[i] = "PowerSurge";
            }
            else if (itemRainProbability > 0)
            {
                --itemRainProbability;
                possibleEvents[i] = "ItemRain";
            }
            // Continues to do this for each event type
        }
    }

    void PlayEvent()
    {
        // Randomly picks an event from the array of possible events
        // Can include logic to deny an event if its already been chosen or do something similar if desired
        chosenEvent = possibleEvents[Random.Range(0, possibleEvents.Length)];
        Invoke(chosenEvent, 0);
    }

    void CoinBlitz()
    {
        // Defines the new coin placement cooldown which will be used during the blitz event
        //coinPlacementCooldown = X;
        playerCamera.GetComponent<CoinPlacement>().blitzCooldown = coinPlacementCooldown;

        playerCamera.GetComponent<CoinPlacement>().blitzEvent = true;
        currentEventDuration = coinBlitzDuration;
    }

    void PowerSurge()
    {
        coinPusher.GetComponent<CoinPusher>().surgeSpeed = surgePusherSpeed;

        coinPusher.GetComponent<CoinPusher>().surgeEvent = true;
        currentEventDuration = powerSurgeDuration;
    }

    void ItemRain()
    {
        Debug.LogWarning("Item Rain Woot Woot!");

        // DO SOMETHING LIKE THIS BUT INSTEAD RUN ITS ITEM RAIN METHOD WITH THE PARAMETER BEING THE EVENT DURATION AS AN INTEGER
        //itemBuilder.GetComponent<ItemBuilder>().itemRainEvent = true;

        StartCoroutine(itemBuilder.GetComponent<ItemBuilder>().ItemRain(Mathf.FloorToInt(itemRainDuration)));

        // item builder clock equal to integer
        // item builder build order equal to integer
        // integer taken as parameter

        currentEventDuration = itemRainDuration;

        //instantiate 1 item every second

        //tell item builder to build items

        //every second of time in the item duration adds 1 item to the itemBuilder's build order

        //item builder will make an item every second (use deltaTime)

        //item rain duration directly affects the number of items that will be built (1 every second for X seconds)
    }

}
