

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

    public GameObject pegManager;

    public GameObject gameManager;

    // Glass panel on the plinko part of the board
    public GameObject glassPanel;

    public GameObject falsePusher1;
    public GameObject falsePusher2;

    public GameObject blitzIcon;
    public GameObject surgeIcon;
    public GameObject comboIcon;
    public GameObject background;

    public float itemGravity;
    public float maxGravity = -40;

    public string[] commonEvents = new string[] { "CoinBlitz" };
    // Remove power surge from uncommon events and replace it with another event (maybe)
    public string[] uncommonEvents = new string[] { "PowerSurge" };
    public string[] rareEvents = new string[] { "ItemRain", "PegCombo"};

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

    // Remaining duration of current running event
    public float currentEventDuration;

    // Total duration of these events
    public float coinBlitzDuration;
    public float itemRainDuration;
    public float powerSurgeDuration;
    public float pegComboDuration;
    public float jackpotDuration;

    // Used during the blitz event to alter the coin placement cooldown so coins can be placed faster
    public float coinPlacementCooldown;

    public float surgePusherSpeed;

    private AudioSource comboPing;

    public GameObject sfxMaster;

    public GameObject audioManager;

    private float eventTracker = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerIsReady = true;

        audioManager = GameObject.FindGameObjectWithTag("audio_manager");

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

        comboPing = GameObject.FindGameObjectWithTag("combo_sound").GetComponent<AudioSource>();

        // Enables the initialization phase
        initializationPhase = true;
        timeUntilNextEventCheck = waitTime;
        animationFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (chosenEvent.Equals("CoinBlitz"))
        {
            eventTracker += Time.deltaTime;
            if (eventTracker > coinBlitzDuration + 1f)
            {
                EndEvent();
            }
        }
        else
        {
            eventTracker = 0;
        }

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
            Physics.gravity = new Vector3(0, itemGravity, 0);

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

            StartCoroutine(Wait(8));

            //gameManager.GetComponent<UI_Manager>().Update_UI(3);
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

    private IEnumerator Wait(float seconds)
    {
        float timeRemaining = seconds;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        gameManager.GetComponent<UI_Manager>().Update_UI(3);
        gameManager.GetComponent<ItemInventory>().startup = false;
        sfxMaster.GetComponent<AudioMuter>().MuteToggle();
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
        coinPusher.GetComponent<CoinPusher>().PausePusher();

        falsePusher1.GetComponent<FalsePusher>().PausePusher();
        falsePusher2.GetComponent<FalsePusher>().PausePusher();
        itemBuilder.GetComponent<ItemBuilder>().isPaused = true;

        if (chosenEvent.Equals("PegCombo"))
        {
            pegManager.GetComponent<PegManager>().PauseComboEvent();
        }

        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("whole_coin");
        GameObject[] allItemCapsules = GameObject.FindGameObjectsWithTag("item_capsule");

        foreach (GameObject coin in allCoins)
        {
            Rigidbody coinRb = coin.GetComponent<Rigidbody>();
            coinRb.constraints = RigidbodyConstraints.FreezeAll;
            coinRb.detectCollisions = false;
        }

        foreach (GameObject item in allItemCapsules)
        {
            Rigidbody itemRb = item.GetComponent<Rigidbody>();
            itemRb.constraints = RigidbodyConstraints.FreezeAll;
            itemRb.detectCollisions = false;
            itemRb.useGravity = false;
        }
    }

    public void ResumeMachine()
    {
        coinPusher.GetComponent<CoinPusher>().UnpausePusher();

        falsePusher1.GetComponent<FalsePusher>().UnpausePusher();
        falsePusher2.GetComponent<FalsePusher>().UnpausePusher();
        itemBuilder.GetComponent<ItemBuilder>().isPaused = false;

        if (chosenEvent.Equals("PegCombo"))
        {
            pegManager.GetComponent<PegManager>().ResumeComboEvent();
        }

        GameObject[] allCoins = GameObject.FindGameObjectsWithTag("whole_coin");
        GameObject[] allItemCapsules = GameObject.FindGameObjectsWithTag("item_capsule");

        foreach (GameObject coin in allCoins)
        {
            Rigidbody coinRb = coin.GetComponent<Rigidbody>();
            coinRb.constraints = RigidbodyConstraints.None;
            coinRb.detectCollisions = true;
        }

        foreach (GameObject item in allItemCapsules)
        {
            Rigidbody itemRb = item.GetComponent<Rigidbody>();
            itemRb.constraints = RigidbodyConstraints.None;
            itemRb.detectCollisions = true;
            itemRb.useGravity = true;
        }
    }

    void GameplayPhase()
    {
        // Tells the player camera that gameplay is ready (this would allow the camera to remove any "Loading" screen and prepare the UI)
        playerCamera.GetComponent<CoinPlacement>().gameplayIsReady = true;
        // Tells the coin destroyer to start tracking coins that fall into it so the player can gather money
        coinDestroyer.GetComponent<DeleteCoins>().gameplayIsReady = true;

        Physics.gravity = new Vector3(0, itemGravity, 0);

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
        StartCoroutine(FlashEventName(chosenEvent));
    }

    void EndEvent()
    {
        StopAllCoroutines();

        //background.SetActive(false);

        gameManager.GetComponent<DotLightManager>().Idle();

        blitzIcon.SetActive(false);
        surgeIcon.SetActive(false);
        comboIcon.SetActive(false);
        playerCamera.GetComponent<CoinPlacement>().blitzEvent = false;
        coinPusher.GetComponent<CoinPusher>().surgeEvent = false;
        itemBuilder.GetComponent<ItemBuilder>().itemRainEvent = false;

        if (chosenEvent == "CoinBlitz")
        {
            StartCoroutine(glassPanel.GetComponent<GlassRemover>().RebuildGlass());
        }
        if (chosenEvent.Equals("PowerSurge"))
        {
            audioManager.GetComponent<AudioManager>().StopAudioClip("surge_drone");
        }
        
        //gameManager.GetComponent<DotLightManager>().Idle();

        chosenEvent = "";
    }

    void CoinBlitz()
    {
        // Defines the new coin placement cooldown which will be used during the blitz event
        //coinPlacementCooldown = X;
        //playerCamera.GetComponent<CoinPlacement>().blitzCooldown = coinPlacementCooldown;

        playerCamera.GetComponent<CoinPlacement>().blitzEvent = true;
        currentEventDuration = coinBlitzDuration;

        StartCoroutine(glassPanel.GetComponent<GlassRemover>().RemoveGlass());

        StartCoroutine(gameManager.GetComponent<DotLightManager>().Flash(coinBlitzDuration));

        audioManager.GetComponent<AudioManager>().PlayAudioClip("blitz");
    }

    void PowerSurge()
    {
        audioManager.GetComponent<AudioManager>().PlayAudioClip("surge_click");
        audioManager.GetComponent<AudioManager>().PlayAudioClip("surge_drone");
        //coinPusher.GetComponent<CoinPusher>().surgeSpeed = surgePusherSpeed;

        coinPusher.GetComponent<CoinPusher>().surgeEvent = true;
        currentEventDuration = powerSurgeDuration;

        StartCoroutine(gameManager.GetComponent<DotLightManager>().Frenzy(powerSurgeDuration));
    }

    void ItemRain()
    {

        StartCoroutine(itemBuilder.GetComponent<ItemBuilder>().ItemRain(Mathf.FloorToInt(itemRainDuration)));

        currentEventDuration = itemRainDuration;

        StartCoroutine(gameManager.GetComponent<DotLightManager>().Flash(2));

    }

    void PegCombo()
    {
        StartCoroutine(pegManager.GetComponent<PegManager>().ComboEvent(pegComboDuration));

        currentEventDuration = pegComboDuration;

        StartCoroutine(gameManager.GetComponent<DotLightManager>().Scroll(pegComboDuration));

        comboPing.Play();
    }

    public void IncreaseGravity(float gravityIncrease)
    {
        if (itemGravity > maxGravity)
        {
            itemGravity -= gravityIncrease;
        }
        else
        {
            itemGravity = maxGravity;
        }
    }

    public void IncreasePushSpeed(float speed)
    {
        coinPusher.GetComponent<CoinPusher>().IncreasePushSpeed(speed);
    }

    private IEnumerator FlashEventName(string eventType)
    {
        GameObject image = blitzIcon;

        if (!eventType.Equals("ItemRain"))
        {
            if (eventType.Equals("CoinBlitz"))
            {
                image = blitzIcon;
            }
            else if (eventType.Equals("PowerSurge"))
            {
                image = surgeIcon;
            }
            else if (eventType.Equals("PegCombo"))
            {
                image = comboIcon;
            }

            //background.SetActive(true);

            while (true)
            {
                image.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                image.SetActive(false);
                yield return new WaitForSeconds(0.3f);
            }
        }

    }
}
