using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    public int coinValueModifier;
    private int defaultCoinValueModifier = 1;
    public int startingComboMultiplier = 2;

    public GameObject standardAppearance;
    public GameObject goldAppearance;
    public GameObject diamondAppearance;
    public GameObject comboAppearance;
    public GameObject selectionHighligter;

    // Used to prevent coins from constantly stacking modifiers if they bounce slightly off of this peg (Applies only to combo pegs)
    private List<GameObject> recentlyUpgradedCoins = new List<GameObject>();

    private float timeUntilObjectRemoval;

    public bool amModified;
    public bool amDisabled;
    private bool amGolden;
    private bool amDiamond;
    private bool amCombo;


    void Start()
    {
        amModified = false;
        amDisabled = false;
        coinValueModifier = defaultCoinValueModifier;
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

    public void ConvertToGilded(int pegValueModifier)
    {
        DeterminePegType("gold", pegValueModifier);
    }

    public void ConvertToDiamond(int pegValueModifier)
    {
        DeterminePegType("diamond", pegValueModifier);
    }

    public void ConvertToCombo()
    {
        DeterminePegType("combo");
    }

    public void ConvertToDisabled()
    {
        // Replace this with a amDisabled variable which stops normal function and swaps collider for a trigger
        // Coins wont interact with trigger but selection tool MUST!
        gameObject.SetActive(false);
        amDisabled = true;
    }

    private void DeterminePegType(string pegType, int modifier = 0)
    {
        amModified = true;

        if (pegType.Equals("gold"))
        {
            amGolden = true;
            standardAppearance.SetActive(false);
            goldAppearance.SetActive(true);
        }
        else if (pegType.Equals("diamond"))
        {
            amDiamond = true;
            standardAppearance.SetActive(false);
            diamondAppearance.SetActive(true);
        }
        else if (pegType.Equals("combo"))
        {
            amCombo = true;
            standardAppearance.SetActive(false);
            comboAppearance.SetActive(true);
        }
    }

    void AttemptUpgradeOnCoin(Collider other)
    {
        recentlyUpgradedCoins.Add(other.gameObject.transform.parent.gameObject);
        if (amGolden)
        {
            // Activate gilded bumper on coin and apply value modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateBumper(coinValueModifier);
        }
        else if (amDiamond)
        {
            // Activate crystal shell on coin and apply value modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateCrystalShell(coinValueModifier);
        }
        else if (amCombo)
        {
            // Multiply coin's current combo multiplier by itself
            other.gameObject.GetComponentInParent<CoinLogic>().ComboMultiplier();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Runs if colliding with coin and this peg is modified
        if (other.gameObject.tag == "coin" && amModified)
        {
            AttemptUpgradeOnCoin(other);
        }
        if (other.gameObject.tag == "pegSelectionTool")
        {
            standardAppearance.SetActive(false);
            goldAppearance.SetActive(false);
            diamondAppearance.SetActive(false);
            comboAppearance.SetActive(false);

            selectionHighligter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "pegSelectionTool")
        {
            selectionHighligter.SetActive(false);

            if (amModified)
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
            else
            {
                standardAppearance.SetActive(true);
            }
        }
    }
}
