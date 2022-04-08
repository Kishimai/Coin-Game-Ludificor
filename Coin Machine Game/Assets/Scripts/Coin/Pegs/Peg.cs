using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Peg : MonoBehaviour
{
    private float coinValueModifier;

    public GameObject standardAppearance;
    public GameObject goldAppearance;
    public GameObject diamondAppearance;
    public GameObject comboAppearance;
    public GameObject palladiumAppearance;
    public GameObject selectionHighligter;

    public Material standardMaterial;

    public Material goldMaterial;
    public Material goldFlash;

    public Material diamondMaterial;
    public Material diamondFlash;

    public Material comboMaterial;
    public Material comboFlash;

    public GameObject comboEventAppearance;

    public AudioSource comboPing;

    public GameObject comboSphere;
    public GameObject comboEventSphere;

    private GameObject manager;

    // Used to prevent coins from constantly stacking modifiers if they bounce slightly off of this peg (Applies only to combo pegs)
    private List<GameObject> recentlyUpgradedCoins = new List<GameObject>();

    private float timeUntilObjectRemoval;

    public bool amModified = false;
    private bool amDisabled = false;
    private bool amGolden = false;
    private bool amDiamond = false;
    private bool amCombo = false;
    private bool amPalladium = false;

    private bool amComboEvent;

    private float recordedValueModifier = 0;
    private bool recordedAmModified = false;
    private bool recordedAmDisabled = false;
    private bool recordedAmGolden = false;
    private bool recordedAmDiamond = false;
    private bool recordedAmCombo = false;
    private bool recordedAmPalladium = false;
    private bool recordingTaken = false;

    private float timeUntilBump;
    private float bumpLimit = 1f;

    private int hitCounter;
    public int maxHitsBeforeMoving;


    void Start()
    {
        amModified = false;
        amDisabled = false;

        timeUntilBump = bumpLimit;

        manager = GameObject.FindGameObjectWithTag("peg_manager");
    }

    void Update()
    {
        // After 1 second has passed the oldest object in the recent upgrade list will be removed so it doesnt fill forever
        timeUntilObjectRemoval -= Time.deltaTime;
        if (timeUntilObjectRemoval <= 0 && recentlyUpgradedCoins.Count > 0)
        {
            timeUntilObjectRemoval = 1;
            recentlyUpgradedCoins.RemoveAt(0);
        }
    }

    public void ConvertToGilded(float pegValueModifier)
    {
        DeterminePegType("gold", pegValueModifier);
    }

    public void ConvertToDiamond(float pegValueModifier)
    {
        DeterminePegType("diamond", pegValueModifier);
    }

    public void ConvertToCombo()
    {
        DeterminePegType("combo");
    }

    public void ConvertToPalladium()
    {
        DeterminePegType("palladium");
    }

    public void ConvertToDisabled()
    {
        // Replace this with a amDisabled variable which stops normal function and swaps collider for a trigger
        // Coins wont interact with trigger but selection tool MUST!
        //gameObject.SetActive(false);
        amDisabled = true;
        recordedAmDisabled = true;
        GetComponent<CapsuleCollider>().isTrigger = true;
        standardAppearance.SetActive(false);
        goldAppearance.SetActive(false);
        diamondAppearance.SetActive(false);
        comboAppearance.SetActive(false);
    }

    private void DeterminePegType(string pegType, float modifier = 0)
    {
        amModified = true;

        coinValueModifier = modifier;

        // Replace this with a amDisabled variable which stops normal function and swaps collider for a trigger
        // Coins wont interact with trigger but selection tool MUST!
        //gameObject.SetActive(true);
        amDisabled = false;
        recordedAmDisabled = false;

        if (pegType.Equals("gold"))
        {
            if (comboEventAppearance.activeSelf == false)
            {
                amGolden = true;
                amCombo = false;
                amDiamond = false;
                amPalladium = false;
                standardAppearance.SetActive(false);
                comboAppearance.SetActive(false);
                palladiumAppearance.SetActive(false);
                diamondAppearance.SetActive(false);
                goldAppearance.SetActive(true);
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
            else
            {
                recordedAmModified = true;

                recordedAmGolden = true;
                recordedAmCombo = false;
                recordedAmDiamond = false;
                recordedAmPalladium = false;
                recordedValueModifier = modifier;
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
        else if (pegType.Equals("diamond"))
        {
            if (comboEventAppearance.activeSelf == false)
            {

                amGolden = false;
                amCombo = false;
                amDiamond = true;
                amPalladium = false;
                standardAppearance.SetActive(false);
                comboAppearance.SetActive(false);
                palladiumAppearance.SetActive(false);
                diamondAppearance.SetActive(true);
                goldAppearance.SetActive(false);
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
            else
            {
                recordedAmModified = true;

                recordedAmGolden = false;
                recordedAmCombo = false;
                recordedAmDiamond = true;
                recordedAmPalladium = false;
                recordedValueModifier = modifier;
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
        else if (pegType.Equals("combo"))
        {
            if (comboEventAppearance.activeSelf == false)
            {
                amGolden = false;
                amCombo = true;
                amDiamond = false;
                amPalladium = false;
                standardAppearance.SetActive(false);
                comboAppearance.SetActive(true);
                palladiumAppearance.SetActive(false);
                diamondAppearance.SetActive(false);
                goldAppearance.SetActive(false);
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
            else
            {
                recordedAmModified = true;

                recordedAmGolden = false;
                recordedAmCombo = true;
                recordedAmDiamond = false;
                recordedAmPalladium = false;
                recordedValueModifier = modifier;
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
        else if (pegType.Equals("palladium"))
        {
            if (comboEventAppearance.activeSelf == false)
            {
                amGolden = false;
                amCombo = false;
                amDiamond = false;
                amPalladium = true;
                standardAppearance.SetActive(false);
                comboAppearance.SetActive(false);
                palladiumAppearance.SetActive(true);
                diamondAppearance.SetActive(false);
                goldAppearance.SetActive(false);
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
            else
            {
                recordedAmModified = true;

                recordedAmGolden = false;
                recordedAmCombo = false;
                recordedAmDiamond = false;
                recordedAmPalladium = true;
                recordedValueModifier = 1;
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
        }
    }

    void AttemptUpgradeOnCoin(Collider other)
    {

        GameObject parent = other.gameObject.transform.parent.gameObject;
        GameObject childID = parent.transform.GetChild(0).gameObject;


        recentlyUpgradedCoins.Add(other.gameObject.transform.parent.gameObject);
        if (amGolden)
        {
            // Activate gilded bumper on coin and apply value modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateBumper(coinValueModifier);
            ++hitCounter;
        }
        else if (amDiamond)
        {
            // Activate crystal shell on coin and apply value modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateCrystalShell(coinValueModifier);
            ++hitCounter;
        }
        else if (amCombo)
        {
            // Multiply coin's current combo multiplier by itself
            other.gameObject.GetComponentInParent<CoinLogic>().ComboMultiplier();
            ++hitCounter;
            comboPing.Play();
        }
        else if (amComboEvent)
        {
            other.gameObject.GetComponentInParent<CoinLogic>().ComboEvent();
        }
        else if (amPalladium)
        {
            if (other.gameObject.tag != "palladium_coin" && other.gameObject.tag != "styrofoam_coin")
            {
                other.transform.GetComponentInParent<CoinLogic>().ConvertToPalladium();
                ++hitCounter;
            }
        }

        if (hitCounter == maxHitsBeforeMoving)
        {
            Relocate();
        }
    }

    // Records current state of peg to return to after an event, item, or spell interaction
    public void RecordCurrentAttributes()
    {
        recordedValueModifier = coinValueModifier;
        recordedAmModified = amModified;
        recordedAmDisabled = amDisabled;

        if (amGolden)
        {
            recordedAmGolden = amGolden;
        }
        else if (amDiamond)
        {
            recordedAmDiamond = amDiamond;
        }
        else if (amCombo)
        {
            recordedAmCombo = amCombo;
        }
        else if (amPalladium)
        {
            recordedAmPalladium = amPalladium;
        }

        recordingTaken = true;
    }

    // Returns peg to its previously recorded state after an event, spell, or item's influence ends
    public void RevertToRecordedAttributes()
    {

        // If an actual recording was taken, returns peg to that recorded state
        // (prevents issues or bugs resulting from no recording being taken)
        if (recordingTaken)
        {
            coinValueModifier = recordedValueModifier;
            amComboEvent = false;
            amModified = recordedAmModified;
            amDisabled = recordedAmDisabled;

            if (amDisabled)
            {
                comboEventAppearance.SetActive(false);
                ConvertToDisabled();
            }
            else if (recordedAmGolden)
            {
                amGolden = recordedAmGolden;
                comboEventAppearance.SetActive(false);
                goldAppearance.SetActive(true);
            }
            else if (recordedAmDiamond)
            {
                amDiamond = recordedAmDiamond;
                comboEventAppearance.SetActive(false);
                diamondAppearance.SetActive(true);
            }
            else if (recordedAmCombo)
            {
                amCombo = recordedAmCombo;
                comboEventAppearance.SetActive(false);
                comboAppearance.SetActive(true);
            }
            else if (recordedAmPalladium)
            {
                amPalladium = recordedAmPalladium;
                comboEventAppearance.SetActive(false);
                palladiumAppearance.SetActive(true);
            }
            else
            {
                comboEventAppearance.SetActive(false);
                standardAppearance.SetActive(true);
            }
        }
        // If no recording was taken but this method was called, it throws an error and lists the object
        else
        {
            Debug.LogWarning("Cannot revert to recorded attributes, as nothing has been recorded (did you call RecordCurrentAttributes() beforehand?): " + gameObject.name);
        }

        // Resets the recordingTaken value to false since it just used that recording
        recordingTaken = false;
    }

    // Converts peg to combo event peg which lasts until combo event is over
    public void ConvertToComboEventPeg()
    {
        RecordCurrentAttributes();

        amModified = false;
        amDisabled = false;
        amGolden = false;
        amDiamond = false;
        amCombo = false;
        amPalladium = false;

        amComboEvent = true;

        standardAppearance.SetActive(false);
        goldAppearance.SetActive(false);
        diamondAppearance.SetActive(false);
        comboAppearance.SetActive(false);
        palladiumAppearance.SetActive(false);

        comboEventAppearance.SetActive(true);
        GetComponent<CapsuleCollider>().isTrigger = false;

        comboSphere.SetActive(false);
        comboEventSphere.SetActive(false);

    }

    public IEnumerator FlashColor()
    {
        //Debug.Log("Flash Color!");

        float flashDuration = 0.25f;

        Material storedMaterial = standardMaterial;

        GameObject goldRing = goldAppearance.transform.GetChild(1).gameObject;

        while (flashDuration > 0)
        {
            flashDuration -= Time.deltaTime;

            if (amGolden)
            {
                storedMaterial = goldMaterial;

                goldRing.GetComponent<Renderer>().material = goldFlash;
            }
            else if (amDiamond)
            {
                storedMaterial = diamondMaterial;

                diamondAppearance.GetComponent<Renderer>().material = diamondFlash;
            }
            else if (amCombo)
            {
                storedMaterial = comboMaterial;

                //comboAppearance.GetComponent<Renderer>().material = comboFlash;
                comboSphere.SetActive(true);
            }
            else if (amComboEvent)
            {
                comboEventSphere.SetActive(true);
            }
            else if (amPalladium)
            {
                // make palladium have special effect
                // play particle effect
                // sparkles or something
            }
            else
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        if (amGolden)
        {
            goldRing.GetComponent<Renderer>().material = storedMaterial;
        }
        else if (amDiamond)
        {
            diamondAppearance.GetComponent<Renderer>().material = storedMaterial;
        }
        else if (amCombo)
        {
            comboAppearance.GetComponent<Renderer>().material = storedMaterial;
            comboSphere.SetActive(false);
        }
        else if (amComboEvent)
        {
            comboEventSphere.SetActive(false);
        }

    }

    private void BumpCoin(Collider other)
    {

        GameObject parent = other.gameObject.transform.parent.gameObject;
        GameObject childID = parent.transform.GetChild(0).gameObject;

        if (childID.tag == "coin")
        {
            parent.GetComponent<CoinLogic>().GetBumped();
        }
        else if (childID.tag == "bomb_coin")
        {
            parent.GetComponent<BombCoin>().GetBumped();
        }
        else if (childID.tag == "tremor_coin")
        {
            parent.GetComponent<TremorCoin>().GetBumped();
        }
        else if (childID.tag == "bulldoze_coin")
        {
            parent.GetComponent<BulldozeCoin>().GetBumped();
        }
        else if (childID.tag == "palladium_coin")
        {
            parent.GetComponent<CoinLogic>().GetBumped();
        }
        else if (childID.tag == "styrofoam_coin")
        {
            parent.GetComponent<CoinLogic>().GetBumped();
        }
    }

    void Relocate()
    {
        hitCounter = 0;
        manager.GetComponent<PegManager>().RelocatePeg(gameObject);
    }

    public void ResetHitCounter()
    {
        hitCounter = 0;
    }

    public string GetPegIdentity()
    {
        string identity = "none";

        if (amGolden)
        {
            identity = "gold";
        }
        else if (amDiamond)
        {
            identity = "diamond";
        }
        else if (amCombo)
        {
            identity = "combo";
        }
        else if (amPalladium)
        {
            identity = "palladium";
        }

        return identity;
    }

    void OnTriggerEnter(Collider other)
    {
        // Runs if colliding with coin and this peg is modified
        if (other.gameObject.tag == "coin" && amModified)
        {
            AttemptUpgradeOnCoin(other);
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "coin" && amComboEvent)
        {
            AttemptUpgradeOnCoin(other);
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "palladium_coin" && amModified)
        {
            AttemptUpgradeOnCoin(other);
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "palladium_coin" && amComboEvent)
        {
            AttemptUpgradeOnCoin(other);
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "styrofoam_coin" && amModified)
        {
            AttemptUpgradeOnCoin(other);
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "styrofoam_coin" && amComboEvent)
        {
            AttemptUpgradeOnCoin(other);
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "pegSelectionTool")
        {
            standardAppearance.SetActive(false);
            goldAppearance.SetActive(false);
            diamondAppearance.SetActive(false);
            comboAppearance.SetActive(false);

            selectionHighligter.SetActive(true);
        }
        if (other.gameObject.tag == "bomb_coin")
        {
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "tremor_coin")
        {
            StartCoroutine(FlashColor());
        }
        if (other.gameObject.tag == "bulldoze_coin")
        {
            StartCoroutine(FlashColor());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "bump_sphere")
        {

            if (timeUntilBump > 0)
            {
                timeUntilBump -= Time.deltaTime;
            }
            else
            {
                timeUntilBump = bumpLimit;
                BumpCoin(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "pegSelectionTool")
        {
            selectionHighligter.SetActive(false);

            if (amDisabled)
            {
                ConvertToDisabled();
            }
            else if (amModified)
            {
                if (amGolden)
                {
                    goldAppearance.SetActive(true);
                }
                else if (amDiamond)
                {
                    diamondAppearance.SetActive(true);
                }
                else
                {
                    comboAppearance.SetActive(true);
                }
            }
            else if (!comboEventAppearance.activeSelf)
            {
                standardAppearance.SetActive(true);
            }
        }

        if (other.gameObject.tag == "bump_sphere")
        {
            timeUntilBump = bumpLimit;
        }
    }
}
