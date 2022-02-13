

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

    public GameObject gameManager;

    // Glass panel on the plinko part of the board
    public GameObject glassPanel;

    public GameObject falsePusher1;
    public GameObject falsePusher2;

    public string[] commonEvents = new string[] { "CoinBlitz" };
    // Remove power surge from uncommon events and replace it with another event (maybe)
    public string[] uncommonEvents = new string[] { "PowerSurge" };
    public string[] rareEvents = new string[] { "ItemRain" };

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
    // Used to stop event countdown until animations and flashy effects from event are finished
    public bool animationFinished;

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

        gameManager = GameObject.FindGameObjectWithTag("game_manager");

        // Enables the initialization phase
        initializationPhase = true;
        timeUntilNextEventCheck = waitTime;
        animationFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the if statement in InitializeGameBoard easier to read
        // It sets printerIsFinished to initialBuildFinished in the coin printer. This is used to accurately track when the coin printer has completed its initialization tasks
        printerIsFinished = coinPrinter.GetComponent<CoinRowPrinter>().initialBuildFinished;

        itemBuilderIsFinished = itemBuilder.GetComponent<ItemBuilder>().initialBuildFinished;

        int currentMenu = gameManager.GetComponent<UI_Manager>().currentUIMenu;

        // Runs the initialization method
        if (initializationPhase)
        {
            InitializeGameBoard();
        }

        // Runs the gameplay method
        if (gameplayPhase && currentMenu != 9 && currentMenu != 8 && currentMenu != 6)
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

            GameObject[] coins = GameObject.FindGameObjectsWithTag("whole_coin");

            // Re-enables movement on all coins after initialization is over
            foreach (GameObject coin in coins)
            {
                Rigidbody coinRb = coin.GetComponent<Rigidbody>();
                coinRb.constraints = RigidbodyConstraints.None;
            }

            // Switches from initialization to gameplay
            initializationPhase = false;
            gameplayPhase = true;

            gameManager.GetComponent<UI_Manager>().Update_UI(3);
            // ^ Code I moved from UI_Manager ^

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

    // PAUSE EVENT FUNCTIONALITY DURING THIS TIME
    // ALSO PAUSE MOVEMENT OF ITEM CAPSULES!
    public void PauseMachine()
    {
        coinPusher.GetComponent<CoinPusher>().allowingMovement = false;

        falsePusher1.GetComponent<FalsePusher>().allowingMovement = false;
        falsePusher2.GetComponent<FalsePusher>().allowingMovement = false;

        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("whole_coin");

        foreach (GameObject coin in allCoins)
        {
            Rigidbody coinRb = coin.GetComponent<Rigidbody>();
            coinRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void ResumeMachine()
    {
        coinPusher.GetComponent<CoinPusher>().allowingMovement = true;

        falsePusher1.GetComponent<FalsePusher>().allowingMovement = true;
        falsePusher2.GetComponent<FalsePusher>().allowingMovement = true;

        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("whole_coin");

        foreach (GameObject coin in allCoins)
        {
            Rigidbody coinRb = coin.GetComponent<Rigidbody>();
            coinRb.constraints = RigidbodyConstraints.None;
        }
    }

    void GameplayPhase()
    {
        // Tells the player camera that gameplay is ready (this would allow the camera to remove any "Loading" screen and prepare the UI)
        playerCamera.GetComponent<CoinPlacement>().gameplayIsReady = true;
        // Tells the coin destroyer to start tracking coins that fall into it so the player can gather money
        coinDestroyer.GetComponent<DeleteCoins>().gameplayIsReady = true;

        // If an event is not going on, decrease time until next event check
        if (currentEventDuration <= 0 && animationFinished)
        {
            timeUntilNextEventCheck -= Time.deltaTime;

            if (chosenEvent != "")
            {
                EndEvent();
            }
        }
        // If an event is going on, decrease its remaining duration
        else if (currentEventDuration > 0 && animationFinished)
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

    void PlayEvent()
    {
        // Randomly picks an event from the array of possible events
        // Can include logic to deny an event if its already been chosen or do something similar if desired
        //chosenEvent = possibleEvents[Random.Range(0, possibleEvents.Length)];
        chosenEvent = GetComponent<EventRandomizer>().RollNewEvent();
        Invoke(chosenEvent, 0);
    }

    void EndEvent()
    {

        playerCamera.GetComponent<CoinPlacement>().blitzEvent = false;
        coinPusher.GetComponent<CoinPusher>().surgeEvent = false;

        if (chosenEvent == "CoinBlitz")
        {
            StartCoroutine(glassPanel.GetComponent<GlassRemover>().RebuildGlass());
        }

        chosenEvent = "";
    }

    void CoinBlitz()
    {
        // Defines the new coin placement cooldown which will be used during the blitz event
        //coinPlacementCooldown = X;
        playerCamera.GetComponent<CoinPlacement>().blitzCooldown = coinPlacementCooldown;

        playerCamera.GetComponent<CoinPlacement>().blitzEvent = true;
        currentEventDuration = coinBlitzDuration;

        StartCoroutine(glassPanel.GetComponent<GlassRemover>().RemoveGlass());
    }

    void PowerSurge()
    {
        coinPusher.GetComponent<CoinPusher>().surgeSpeed = surgePusherSpeed;

        coinPusher.GetComponent<CoinPusher>().surgeEvent = true;
        currentEventDuration = powerSurgeDuration;
    }

    void ItemRain()
    {

        StartCoroutine(itemBuilder.GetComponent<ItemBuilder>().ItemRain(Mathf.FloorToInt(itemRainDuration)));

        currentEventDuration = itemRainDuration;

    }

}
