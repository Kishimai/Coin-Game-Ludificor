using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPlacement : MonoBehaviour
{

    // -------------------- CoinPlacement -------------------- //

    // CoinPlacement is attached to the player camera, and is used to detect the location of the player's mouse, to allow them to place coins into the machine
    // This script will take information from the player inventory, and use that to give them control over item and coin placement

    // ------------------------------------------------------- //

    // Replace this. (it is currently used to show the image of a coin, which helps the player see where they are about to place it)
    public GameObject testCoin;
    // Holds the player's currently selected coin: >> I'll be changing its Components on each placement
    public GameObject selectedCoin;

    public GameObject coinParent;
    public GameObject specialEffect;
    public GameObject eventManager;

    public GameObject spellCoin;
    public GameObject bombCoin;
    public GameObject tremorCoin;
    public GameObject palladiumCoin;
    public GameObject styrofoamCoin;
    public GameObject blackHoleCoin;
    public GameObject detonateButton;
    public GameObject succButton;
    public GameObject bulldozeCoin;
    public GameObject blitzSparkle;
    public GameObject bombCoinMarker;
    public GameObject shortFade, longFade;
    public List<string> spells = new List<string>();
    public List<GameObject> activeBombs = new List<GameObject>();
    public List<GameObject> activeBlackHoles = new List<GameObject>();

    private GameObject itemBuilder;

    // Used for generation codes
    public CoinGeneration generation;

    // Shows the image of the currently selected coin so the player can visualize their placements better
    private GameObject coinGuide;
    private GameObject coinPointer;
    private GameObject blitzPointer;
    // Prevents the use of this script when gameplay is not ready yet
    // !(EventsManager is responsible for deciding game states)!
    public bool gameplayIsReady;
    private bool gameplayPaused;

    private bool usingSpell = false;

    private GameObject gameManager;

    public bool enableAutoDrop = false;

    // Drop zone layer is to be assigned to the "Drop Zone" object, which is responsible for allowing the player to place coins at all
    // When the mouse cursor is hovering over the "Drop Zone" the player will be allowed to place coins
    private int dropZoneLayer = 6;
    // The layer mask is used to ensure that the mouse cursor is ONLY looking for the "Drop Zone" when the player is placing coins
    public LayerMask dropZoneLayerMask;
    // Responsible for preventing the player from dropping coins rapidly
    private float dropCooldown;
    // Drop cooldown gets its value from maxCooldown (set maxCooldown to the wanted value depending on game states, events, or other situations)
    public float maxCooldown;

    public float minXDropClamp;
    public float maxXDropClamp;
    public float yClamp;
    public float zClamp;

    public bool blitzEvent = false;
    public float blitzCooldown;

    public int additionalDropChance = 0;
    public int guaranteedDrops = 0;
    public int maxAdditionalDrops = 69;

    public int numBombCoins = 0;
    public int numTremorCoins = 0;
    public int numBulldozeCoins = 0;
    public int numBlackHoleCoins = 0;

    public float bombCoinCooldown = 0;
    public float defaultBombCooldown = 180f;

    public float tremorCoinCooldown = 0;
    public float defaultTremorCooldown = 180f;

    public float bulldozeCoinCooldown = 0;
    public float defaultBulldozeCoinCooldown = 300f;

    public float blackHoleCoinCooldown = 0;
    public float defaultBlackholeCooldown = 180f;

    public float radiusIncrease = 0.25f;
    public float forceIncrease = 150;
    public float tremorDurationIncrease = 0;

    public float radiusOfExplosion = 6;
    public float explosionForce = 1200;
    public float upwardsForce = 0.1f;
    public Vector3 expansionRate = new Vector3(25,25,25);

    public Vector3 blackHoleSize = new Vector3(0,0,0);
    public Vector3 defaultBlackHoleSize = new Vector3(2,2,2);

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure this script knows that gameplay is not yet ready
        gameplayIsReady = false;

        blackHoleSize = defaultBlackHoleSize;

        // Creates the coinGuide object (Change this so that the coinGuide constantly reflects the currently selected coin)!
        coinGuide = Instantiate(testCoin, gameObject.transform.position, Quaternion.Euler(90, 0, 0));
        coinPointer = coinGuide.transform.GetChild(0).gameObject;
        blitzPointer = coinGuide.transform.GetChild(1).gameObject;
        blitzPointer.SetActive(false);

        // Disables the coinGuide object so its not constantly shown in the scene
        coinGuide.SetActive(false);

        // Sets the layer mask to the drop zone layer
        // Doing this ensures that ONLY the drop zone layer is saved to the layer mask
        dropZoneLayerMask = (1 << dropZoneLayer);

        gameManager = GameObject.FindGameObjectWithTag("game_manager");

        itemBuilder = GameObject.FindGameObjectWithTag("item_builder");

        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
    }

    // Update is called once per frame
    void Update()
    {
        if (bombCoinCooldown >= 0)
        {
            bombCoinCooldown -= Time.deltaTime;
        }
        else
        {
            bombCoinCooldown = defaultBombCooldown;
            if (numBombCoins > 0 && !spells.Contains("bomb"))
            {
                spells.Add("bomb");
            }
        }

        if (tremorCoinCooldown >= 0)
        {
            tremorCoinCooldown -= Time.deltaTime;
        }
        else
        {
            tremorCoinCooldown = defaultTremorCooldown;
            if (numTremorCoins > 0 && !spells.Contains("tremor"))
            {
                spells.Add("tremor");
            }
        }

        if (bulldozeCoinCooldown >= 0)
        {
            bulldozeCoinCooldown -= Time.deltaTime;
        }
        else
        {
            bulldozeCoinCooldown = defaultBulldozeCoinCooldown;
            if (numBulldozeCoins > 0 && !spells.Contains("bulldoze"))
            {
                spells.Add("bulldoze");
            }
        }

        if (blackHoleCoinCooldown >= 0)
        {
            blackHoleCoinCooldown -= Time.deltaTime;
        }
        else
        {
            blackHoleCoinCooldown = defaultBlackholeCooldown;
            if (numBlackHoleCoins > 0 && !spells.Contains("blackhole"))
            {
                spells.Add("blackhole");
            }
        }

        if (gameManager.GetComponent<UI_Manager>().currentUIMenu != 4 || gameManager.GetComponent<UI_Manager>().ShopInGame.activeSelf)
        {
            gameplayPaused = true;
        }
        else
        {
            gameplayPaused = false;
        }

        if (blitzEvent)
        {
            coinPointer.SetActive(false);
            //blitzPointer.SetActive(true);
            shortFade.SetActive(true);
            longFade.SetActive(true);
        }
        else
        {
            coinPointer.SetActive(true);
            //blitzPointer.SetActive(false);
            shortFade.SetActive(false);
            longFade.SetActive(false);
        }

        // Runs if gameplay is ready
        if (gameplayIsReady && !gameplayPaused)
        {
            if (dropCooldown > 0)
            {
                dropCooldown -= Time.deltaTime;
            }

            // Makes coin guide appear like the selected coin (Need to strip all properties and physics interactions from the guide!)
            //coinGuide = selectedCoin;

            MouseTracking();
        }

        if (activeBombs.Count == 0)
        {
            detonateButton.SetActive(false);
        }
        if (activeBlackHoles.Count == 0)
        {
            succButton.SetActive(false);
        }
        
        CalculateDropChance();

    }

    // Responsible for tracking mouse position for coin and item placement
    void MouseTracking()
    {
        // Defines a new ray, which shoots from the camera's origin to the tip of the player's cursor at all times
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Defines a raycast hit point, which stores information for the position the cursor is hovering over
        RaycastHit hit;

        // Runs only if the mouse cursor is currently hovering over the "Drop Zone" object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, dropZoneLayerMask))
        {
            // If coin is on cooldown, the coin guide dissapears
            if (dropCooldown <= 0 && !blitzEvent)
            {
                // Enables the coinGuide object so that the player can see where they are about to place a coin
                coinGuide.SetActive(true);
            }
            else
            {
                coinGuide.SetActive(false);
            }

            float clampedX = Mathf.Clamp(hit.point.x, minXDropClamp, maxXDropClamp);
            float clampedY = Mathf.Clamp(hit.point.y, yClamp, yClamp + 0.1f);
            float clampedZ = Mathf.Clamp(hit.point.z, zClamp, zClamp + 0.1f);

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, clampedZ);

            // Ensures that the coin guide's position is always where the mouse cursor is
            coinGuide.transform.position = clampedPosition;

            if (enableAutoDrop && blitzEvent != true)
            {
                if (Input.GetButton("Fire1"))
                {
                    DropLogic(clampedPosition);
                }
            }
            else
            {
                // Runs if the player clicks the left mouse button
                if (Input.GetButtonUp("Fire1"))
                {
                    DropLogic(clampedPosition);
                }
            }
            
        }

        // Runs if the mouse cursor is NOT hovering over the "Drop Zone" object
        else
        {
            // Disables the coin guide
            coinGuide.SetActive(false);
        }
    }

    void DropLogic(Vector3 clampedPosition)
    {

        GameObject newCoin;
        GameObject newEffect;

        if (dropCooldown <= 0)
        {
            generation.GetPlacementData();
        }

        if (spells.Count == 0 && dropCooldown <= 0)
        {
            if (dropCooldown <= 0 && blitzEvent == false)
            {
                //generation.GetPlacementData();
                // Places the currently selected coin >> Changing its Component every placement
                newCoin = Instantiate(selectedCoin, clampedPosition, Quaternion.Euler(90, 0, 0));
                dropCooldown = maxCooldown;

                newCoin.transform.SetParent(coinParent.transform);
            }
            else if (dropCooldown <= 0 && blitzEvent == true)
            {
                //generation.GetPlacementData();
                //Vector3 blitzPosition = new Vector3(clampedPosition.x, clampedPosition.y, clampedPosition.z + 1.25f);

                Vector3 blitzPosition;

                float randX = Random.Range(minXDropClamp, maxXDropClamp);

                blitzPosition = new Vector3(randX, clampedPosition.y, clampedPosition.z + 1.25f);

                // !Play particle effect at instantiation position!
                blitzSparkle.transform.position = blitzPosition;
                blitzSparkle.GetComponent<ParticleSystem>().Play();
                newCoin = Instantiate(selectedCoin, blitzPosition, Quaternion.Euler(90, 0, 0));
                dropCooldown = blitzCooldown;

                newCoin.transform.SetParent(coinParent.transform);
            }

            if (additionalDropChance > 0)
            {
                AttemptAdditionalCoin(clampedPosition);
            }

        }
        else if (dropCooldown <= 0)
        {
            string randomSpell = spells[Random.Range(0, spells.Count)];

            if (randomSpell.Equals("bomb"))
            {
                spellCoin = bombCoin;
                activeBombs.Add(spellCoin);
                specialEffect = bombCoinMarker;
                spells.Remove("bomb");
            }
            //else if (randomSpell.Equals("tremor"))
            //{
            //    spellCoin = tremorCoin;
            //}
            else if (randomSpell.Equals("bulldoze"))
            {
                spellCoin = bulldozeCoin;
                spells.Remove("bulldoze");
            }
            else if (randomSpell.Equals("palladium"))
            {
                spellCoin = palladiumCoin;
                spellCoin.GetComponent<Data_Interp>().data = generation.GetHighestTierCoin();
                spellCoin.GetComponent<CoinLogic>().palladiumValue = generation.GetPalladiumValue();
                spells.Remove("palladium");
            }
            else if (randomSpell.Equals("styrofoam"))
            {
                spellCoin = styrofoamCoin;
                spellCoin.GetComponent<CoinLogic>().styrofoamValue = generation.GetStyrofoamValue();
                spells.Remove("styrofoam");
            }
            else if (randomSpell.Equals("blackhole"))
            {
                spellCoin = blackHoleCoin;
                spells.Remove("blackhole");
                activeBlackHoles.Add(spellCoin);
                // Make marker for black hole coin appear
                // Make detonate button appear for black hole
            }

            spells.Remove(randomSpell);

            Vector3 blitzPosition;

            float randX = Random.Range(minXDropClamp, maxXDropClamp);

            blitzPosition = new Vector3(randX, clampedPosition.y, clampedPosition.z + 1.25f);

            if (blitzEvent)
            {
                newCoin = Instantiate(spellCoin, blitzPosition, Quaternion.Euler(90, 0, 0));
            }
            else
            {
                newCoin = Instantiate(spellCoin, clampedPosition, Quaternion.Euler(90, 0, 0));
            }

            if (randomSpell.Equals("bomb"))
            {
                newEffect = Instantiate(specialEffect, newCoin.transform.position, Quaternion.identity);
                newEffect.GetComponent<FadedBeamMarker>().parentCoin = newCoin;
            }

            Transform identification = spellCoin.transform.GetChild(0);

            if (identification.gameObject.tag == "bomb_coin")
            {
                detonateButton.SetActive(true);
            }
            if (identification.gameObject.tag == "black_hole")
            {
                succButton.SetActive(true);
            }

            newCoin.transform.SetParent(coinParent.transform);

            if (blitzEvent)
            {
                dropCooldown = blitzCooldown;
            }
            else
            {
                dropCooldown = maxCooldown;
            }

            if (additionalDropChance > 0)
            {
                AttemptAdditionalCoin(clampedPosition);
            }
        }
    }

    public void UseSpell(string spell)
    {
        if (spell.Equals("bomb") && !spells.Contains("bomb"))
        {
            spells.Add(spell);
        }
        if (spell.Equals("tremor") && !spells.Contains("tremor"))
        {
            spells.Add(spell);
        }
        if (spell.Equals("bulldoze") && !spells.Contains("bulldoze"))
        {
            spells.Add(spell);
        }
        if (spell.Equals("blackhole") && !spells.Contains("blackhole"))
        {
            spells.Add(spell);
        }

        //Debug.Log("Got Spell: " + spell);
    }

    public void AttemptAdditionalCoin(Vector3 clampedPosition)
    {
        // rolls random number ranging from 0 to 99
        int randInt = Random.Range(0, 99);
        int additionalDrops = 0;

        // If number rolled is less than or equal to additonalDropChance, make additional coin
        if (randInt <= Mathf.CeilToInt(additionalDropChance))
        {
            additionalDrops = 1;
        }

        // Adds guaranteeDrops to additionalDrops
        additionalDrops += guaranteedDrops;

        // Plays particle effect if player dropped at least one additional coin
        if (additionalDrops > 0)
        {
            blitzSparkle.transform.position = coinGuide.transform.position;
            blitzSparkle.GetComponent<ParticleSystem>().Play();
        }

        
        // Builds coins equal to additionalDrops
        for (int i = 0; i < additionalDrops; ++i)
        {
            if (generation.styrofoamCoins > 0)
            {
                List<string> coins = new List<string>();
                foreach (CoinData coin in generation.CoinsAvail){
                    coins.Add("coin");
                }
                for (int j = 0; j < generation.styrofoamCoins; ++j)
                {
                    coins.Add("styrofoam");
                }

                string chosenCoin = coins[Random.Range(0, coins.Count)];

                if (chosenCoin.Equals("styrofoam"))
                {
                    itemBuilder.GetComponent<ItemBuilder>().BuildCoin(styrofoamCoin);
                }
                else
                {
                    itemBuilder.GetComponent<ItemBuilder>().BuildCoin(selectedCoin);
                }

            }
            else
            {
                itemBuilder.GetComponent<ItemBuilder>().BuildCoin(selectedCoin);
            }
        }
        
    }

    private void CalculateDropChance()
    {
        // If additionalDropChance is greater than 100, subtract 100 from it and add 1 to guaranteedDrops
        // The purpose of subtracting instead of setting to zero is to preserve any leftover value from the item collected
        if (additionalDropChance > 100)
        {
            additionalDropChance -= 100;
            ++guaranteedDrops;

            if (guaranteedDrops > maxAdditionalDrops)
            {
                guaranteedDrops = maxAdditionalDrops;
            }
        }
    }

    public void ReduceDropCooldown(float value)
    {

        float fractOfCooldown = blitzCooldown / maxCooldown;
        // 0.1 / 4 = 0.025 (0.025 is 1/4th blitzCooldown)
        float blitzValue = value * fractOfCooldown;

        maxCooldown -= value;
        //blitzCooldown -= blitzValue;

        if (maxCooldown < 0.5)
        {
            maxCooldown = 0.5f;
        }
        if (blitzCooldown < 0.25)
        {
            //blitzCooldown = 0.25f;
        }
    }

    public void EnableAutoDrop()
    {
        enableAutoDrop = true;
    }

    public void IntakeBombCoin()
    {
        numBombCoins++;
        //bombCoin.GetComponent<BombCoin>().IncreaseExplosionRadius(radiusIncrease, forceIncrease);
        radiusOfExplosion += radiusIncrease;
        explosionForce += forceIncrease;
        upwardsForce = 0.5f;
        expansionRate += Vector3.one;
    }

    public void IntakeTremorCoin()
    {
        if (numTremorCoins > 0)
        {
            eventManager.GetComponent<TremorShake>().tremorDuration += 1;
        }
        numTremorCoins++;
    }

    public void IntakeBulldozeCoin()
    {
        numBulldozeCoins++;
        if (defaultBulldozeCoinCooldown > 30)
        {
            defaultBulldozeCoinCooldown -= 30;
        }
    }

    public void IntakeBlackHoleCoin()
    {
        numBlackHoleCoins++;
        // Increase black hole stats
        blackHoleSize += Vector3.one;
    }
}
