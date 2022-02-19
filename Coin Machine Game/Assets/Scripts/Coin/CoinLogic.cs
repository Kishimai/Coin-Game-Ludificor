using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinLogic : MonoBehaviour
{
    public Rigidbody coinRb;
    public GameObject eventManager;
    public GameObject coin;
    public GameObject guildedBumper;
    public GameObject crystalShell;
    public GameObject coinCanvas;
    public Text canvasTextHead;
    public Text canvasTextTail;

    private int gildedModifier = 0;
    private int crystalModifier = 0;
    private int comboMultiplier = 0;
    private int comboEventMultiplier = 0;

    public int totalValueModifier = 0;

    public bool inPlinkoZone;

    public int coinLayer = 11;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        coinRb.constraints = RigidbodyConstraints.None;
    }

    // Update is called once per frame
    void Update()
    {

        totalValueModifier = gildedModifier + crystalModifier + comboMultiplier + comboEventMultiplier;

        if (totalValueModifier != 0)
        {
            coinCanvas.SetActive(true);
            canvasTextHead.text = string.Format("{0}{1}", totalValueModifier, "x");
            canvasTextTail.text = string.Format("{0}{1}", totalValueModifier, "x");
        }
        else
        {
            coinCanvas.SetActive(false);
        }
    }

    public void ActivateBumper(int multiplier)
    {
        gildedModifier = multiplier;
        guildedBumper.SetActive(true);
    }

    public void ActivateCrystalShell(int multiplier)
    {
        crystalModifier = multiplier;
        crystalShell.SetActive(true);
    }

    public void ComboMultiplier()
    {
        if (comboMultiplier == 0)
        {
            comboMultiplier = 2;
        }
        else
        {
            comboMultiplier += comboMultiplier;
        }
    }

    public void ComboEvent()
    {
        guildedBumper.SetActive(true);

        comboEventMultiplier += 2;
    }
}
