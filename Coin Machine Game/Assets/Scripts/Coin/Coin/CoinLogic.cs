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
    public GameObject emeraldAppearance;
    public GameObject rubyAppearance;
    public GameObject sapphireAppearance;
    public GameObject diamondAppearance;
    public GameObject obsidianAppearance;
    public GameObject headCanvas;
    public GameObject tailCanvas;
    public Text canvasTextHead;
    public Text canvasTextTail;

    private RectTransform head;
    private RectTransform tail;

    private float gildedModifier = 1;
    private float crystalModifier = 1;
    private int comboMultiplier = 0;
    private int comboEventMultiplier = 0;
    private int combinedComboMulti;
    private float combinedSpecialMulti;

    public float totalValueModifier = 0;

    public bool inPlinkoZone;

    public int coinLayer = 11;

    private float tremorDuration = 0;

    private bool shaking = false;
    private bool activateText = false;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        coinRb.constraints = RigidbodyConstraints.None;

        CoinData data = GetComponent<Data_Interp>().data;

        head = headCanvas.GetComponent<RectTransform>();
        tail = tailCanvas.GetComponent<RectTransform>();

        CheckIdentity(data);
    }

    // Update is called once per frame
    void Update()
    {

        CalculateTotalModifier();

        if (totalValueModifier > 9)
        {
            head.localScale = new Vector3(0.35f, 0.35f, 1);
            tail.localScale = new Vector3(0.35f, 0.35f, 1);
        }

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

    public void ActivateBumper(float multiplier)
    {
        gildedModifier += multiplier;
        guildedBumper.SetActive(true);
    }

    public void ActivateCrystalShell(float multiplier)
    {
        crystalModifier += multiplier;
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
            comboMultiplier *= 2;
        }
    }

    public void ComboEvent()
    {
        guildedBumper.SetActive(true);

        if (comboEventMultiplier == 0)
        {
            comboEventMultiplier = 2;
        }
        else
        {
            comboEventMultiplier *= 2;
        }
    }

    public void TremorEvent(float duration, float power)
    {
        tremorDuration = duration;
        if (!shaking)
        {
            StartCoroutine(Tremor(power));
        }
    }

    public IEnumerator Tremor(float tremorPower)
    {
        shaking = true;
        while (tremorDuration > 0)
        {
            tremorDuration -= Time.deltaTime;

            float xForce = Random.Range(-tremorPower, tremorPower);
            float yForce = Random.Range(0.5f, 0.5f);
            float zForce = Random.Range(-tremorPower, tremorPower);

            Vector3 randomForce = new Vector3(xForce, 1f, zForce);

            if (coinRb != null)
            {
                coinRb.AddForce(randomForce);
            }
            else
            {
                break;
            }

            yield return new WaitForFixedUpdate();

        }
        shaking = false;
    }

    public void GetBumped()
    {
        Vector3 bumpForce = new Vector3(0, 200, 0);

        coinRb.AddForce(bumpForce);
    }
    
    public void CalculateTotalModifier()
    {
        //totalValueModifier = (comboMultiplier + comboEventMultiplier) * (gildedModifier + crystalModifier);
        combinedComboMulti = comboMultiplier + comboEventMultiplier;
        combinedSpecialMulti = gildedModifier + crystalModifier;

        // If both multipliers are greater than zero
        if (combinedComboMulti > 0 && !Mathf.Approximately(combinedSpecialMulti, 2))
        {
            totalValueModifier = (comboMultiplier + comboEventMultiplier) * ((gildedModifier + crystalModifier) - 1);
        }
        // If combo multi is GREATER THAN zero, and special multi IS zero
        else if (combinedComboMulti > 0 && Mathf.Approximately(combinedSpecialMulti, 2))
        {
            totalValueModifier = comboMultiplier + comboEventMultiplier;
        }
        // If combo multi IS zero, and special multi is GREATER THAN zero
        else if (combinedComboMulti <= 0 && !Mathf.Approximately(combinedSpecialMulti, 2))
        {
            totalValueModifier = (gildedModifier + crystalModifier) - 1;
        }
        else
        {
            totalValueModifier = 0;
        }

        // Rounds to first decimal place (0.0) so number fits on coin image
        totalValueModifier = Mathf.Round(totalValueModifier * 10.0f) * 0.1f;

        if (totalValueModifier > 500)
        {
            totalValueModifier = 500;
        }
    }

    public void CheckIdentity(CoinData data)
    {
        switch (data.Name) {
            case "Emerald Coin":
                emeraldAppearance.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                break;

            case "Ruby Coin":
                rubyAppearance.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                break;

            case "Sapphire Coin":
                sapphireAppearance.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                break;

            case "Diamond Coin":
                diamondAppearance.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                break;

            case "Obsidian Coin":
                obsidianAppearance.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;
                break;

            case "BITCOIN":
                canvasTextHead.color = Color.black;
                canvasTextTail.color = Color.black;
                break;

            default:
                break;
        }
    }
}
