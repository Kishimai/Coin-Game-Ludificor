

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

    // Initialization phase is used when the game is preparing the scene
    public bool initializationPhase;
    // Gameplay phase comes after initialization
    public bool gameplayPhase;

    // Used to track when the coin printer is finished filling the game board
    public bool printerIsFinished;
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
        if (printerIsFinished)
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

            // temporary
            // Used to deactivate the blitz event when the event duration reaches 0
            // Ideally would have system to use the currentEvent to change this
            playerCamera.GetComponent<CoinPlacement>().blitzEvent = false;
        }
        // If an event is going on, decrease its remaining duration
        else
        {
            currentEventDuration -= Time.deltaTime;
        }

        // Runs if it is time to cause a game event
        if (timeUntilNextEventCheck <= 0)
        {
            // IF random number rolled is a certain value, run PlayEvent(), otherwise dont
            if (true)
            {
                PlayEvent();
            }
            
            timeUntilNextEventCheck = waitTime;
        }

    }

    void CompileEvents()
    {
        // Add all possible events
        // If events should be chosen randomly;
        // List should have a max size of 100

        // Defines the new coin placement cooldown which will be used during the blitz event
        //coinPlacementCooldown = X;
        playerCamera.GetComponent<CoinPlacement>().blitzCooldown = coinPlacementCooldown;

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

            possibleEvents[i] = "CoinBlitz";
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
        playerCamera.GetComponent<CoinPlacement>().blitzEvent = true;
        currentEventDuration = coinBlitzDuration;
    }

}
