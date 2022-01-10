

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
    }

}
