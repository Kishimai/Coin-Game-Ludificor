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
    public Text canvasTextHead;
    public Text canvasTextTail;

    private int gildedModifier = 0;
    private int crystalModifier = 0;
    private int comboMultiplier = 0;
    private int comboEventMultiplier = 0;

    public int totalValueModifier = 0;

    public bool inPlinkoZone;

    public int coinLayer = 11;

    private float tremorDuration = 0;

    private bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        coinRb.constraints = RigidbodyConstraints.None;

        CoinData data = GetComponent<Data_Interp>().data;

        CheckIfGem(data);
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
        Vector3 bumpForce = new Vector3(0, 100, 0);

        coinRb.AddForce(bumpForce);
    }

    public void CheckIfGem(CoinData data)
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

            default:
                break;
        }
    }
}
