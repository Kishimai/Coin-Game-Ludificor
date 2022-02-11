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

    // Used to prevent coins from constantly stacking modifiers if they bounce slightly off of this peg (Applies only to combo pegs)
    private List<GameObject> recentlyUpgradedCoins = new List<GameObject>();

    private float timeUntilObjectRemoval;

    private bool amModified;
    private bool amGolden;
    private bool amDiamond;

    void Start()
    {
        amModified = false;
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

    public void ModifyPeg(int pegValueModifier, string pegType, bool golden, bool diamond)
    {
        DeterminePegType(pegType);
        coinValueModifier = pegValueModifier;
        amModified = true;
        amGolden = golden;
        amDiamond = diamond;
    }

    private void DeterminePegType(string pegType)
    {
        if (pegType.Equals("gold"))
        {
            standardAppearance.SetActive(false);
            goldAppearance.SetActive(true);
        }
        else if (pegType.Equals("diamond"))
        {
            standardAppearance.SetActive(false);
            diamondAppearance.SetActive(true);
        }
        else if (pegType.Equals("combo"))
        {
            standardAppearance.SetActive(false);
            comboAppearance.SetActive(true);
        }
    }
    
    public void ConvertToCombo()
    {
        //Activate combo text
        standardAppearance.SetActive(false);
        comboAppearance.SetActive(true);
        coinValueModifier = 1;
        amGolden = false;
        amDiamond = false;
    }

    void USEFORCOMBOFUNCTIONALITYINSTEAD(Collider other)
    {
        // Runs if the collided coin does NOT exist in the list of recently upgraded coins
        if (!recentlyUpgradedCoins.Contains(other.gameObject.transform.parent.gameObject))
        {
            recentlyUpgradedCoins.Add(other.gameObject.transform.parent.gameObject);
            // multiply combo
            // combo value starts at 2x
        }

    }

    void AttemptUpgradeOnCoin(Collider other)
    {
        recentlyUpgradedCoins.Add(other.gameObject.transform.parent.gameObject);
        if (amGolden)
        {
            // Activate gilded bumper on coin and apply value modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateBumper();
            other.gameObject.transform.parent.GetComponent<CoinLogic>().gildedModifier = coinValueModifier;
        }
        else if (amDiamond)
        {
            // Activate crystal shell on coin and apply value modifier
            other.gameObject.GetComponentInParent<CoinLogic>().ActivateCrystalShell();
            other.gameObject.transform.parent.GetComponent<CoinLogic>().crystalModifier = coinValueModifier;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Runs if colliding with coin and this peg is modified
        if (other.gameObject.tag == "coin" && amModified)
        {
            AttemptUpgradeOnCoin(other);
        }
    }
}
