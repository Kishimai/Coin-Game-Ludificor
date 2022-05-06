using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinLogic : MonoBehaviour
{
    private GameObject gameManager;
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
    public GameObject uraniumAppearance;
    public GameObject iridiumAppearance;
    public GameObject thoriumAppearance;
    public GameObject mithrilAppearance;
    public GameObject adamantiteAppearance;
    public GameObject palladiumAppearance;
    public GameObject cobaltAppearance;
    public GameObject headCanvas;
    public GameObject tailCanvas;
    public GameObject coinLight;
    private Light pointLight;
    public Text canvasTextHead;
    public Text canvasTextTail;

    public Color defaultLightColor;
    public Color uranium;
    public Color iridium;
    public Color thorium;
    public Color cobalt;
    public Color mithril;
    public Color adamantite;
    public Color galaxy;
    public Color god;

    public Color whiteText;
    public Color blackText;
    public Color yellowText;
    public Color greenText;
    public Color blueText;

    private RectTransform head;
    private RectTransform tail;

    public Material styrofoamMaterial;

    private float gildedModifier = 1;
    private float crystalModifier = 1;
    private int comboMultiplier = 0;
    private int comboEventMultiplier = 0;
    public int comboChain = 0;
    public int comboEventChain = 0;
    private float palladiumModifier = 0;
    private int combinedComboMulti;
    private float combinedSpecialMulti;

    public float totalValueModifier = 0;
    private float maxValue = 500;

    public float styrofoamValue = 0;
    public float palladiumValue = 0;

    public bool inPlinkoZone;

    public int coinLayer = 11;

    public bool isPalladium = false;
    public bool isStyrofoam = false;

    private float tremorDuration = 0;

    private bool shaking = false;
    private bool activateText = false;

    private float intensityFromType = 0;

    public GameObject audioManager;
    public AudioManager audio;
    private Vector3 currentVelocity = Vector3.zero;
    private float currentSpeed = 0;
    private float speedOfLastFrame = 0;
    public float soundThreshold = 6;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio_manager");
        audio = audioManager.GetComponent<AudioManager>();

        gameManager = GameObject.FindGameObjectWithTag("game_manager");

        coinRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        coinRb.constraints = RigidbodyConstraints.None;

        pointLight = coinLight.GetComponent<Light>();
        pointLight.color = defaultLightColor;

        canvasTextHead = headCanvas.GetComponent<Text>();
        canvasTextTail = tailCanvas.GetComponent<Text>();

        //CoinData data = GetComponent<Data_Interp>().data;
        CheckIdentity();

        head = headCanvas.GetComponent<RectTransform>();
        tail = tailCanvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = coinRb.velocity;
        currentSpeed = currentVelocity.magnitude;

        float difference = Mathf.Abs(currentSpeed - speedOfLastFrame);

        if (difference > soundThreshold)
        {
            audio.PlayAudioClip("coin");
        }

        speedOfLastFrame = currentSpeed;

        CalculateTotalModifier();

        if (totalValueModifier > 9)
        {
            head.localScale = new Vector3(0.35f, 0.35f, 1);
            tail.localScale = new Vector3(0.35f, 0.35f, 1);
        }

        if (totalValueModifier != 0)
        {
            //coinCanvas.SetActive(true);
            headCanvas.SetActive(true);
            tailCanvas.SetActive(true);
            canvasTextHead.text = string.Format("{0}{1}", totalValueModifier, "x");
            canvasTextTail.text = string.Format("{0}{1}", totalValueModifier, "x");
            SetTransparency();
        }
        else
        {
            //coinCanvas.SetActive(false);
        }

        float lightIntensity = totalValueModifier / maxValue;

        pointLight.intensity = (lightIntensity + intensityFromType) * 10;
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

        comboChain += 1;

        if (comboChain > 4)
        {
            comboChain = 4;
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

        comboEventChain += 1;

        if (comboEventChain > 4)
        {
            comboEventChain = 4;
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
            SetTransparency();
        }
        // If combo multi is GREATER THAN zero, and special multi IS zero
        else if (combinedComboMulti > 0 && Mathf.Approximately(combinedSpecialMulti, 2))
        {
            totalValueModifier = comboMultiplier + comboEventMultiplier;
            SetTransparency();
        }
        // If combo multi IS zero, and special multi is GREATER THAN zero
        else if (combinedComboMulti <= 0 && !Mathf.Approximately(combinedSpecialMulti, 2))
        {
            totalValueModifier = (gildedModifier + crystalModifier) - 1;
            SetTransparency();
        }
        else
        {
            totalValueModifier = 0;
        }

        if (!Mathf.Approximately(palladiumModifier, 0))
        {
            float x = totalValueModifier * palladiumModifier;
            totalValueModifier += x;
            SetTransparency();
        }

        // Rounds to first decimal place (0.0) so number fits on coin image
        totalValueModifier = Mathf.Round(totalValueModifier * 10.0f) * 0.1f;

        if (totalValueModifier > maxValue)
        {
            totalValueModifier = maxValue;
        }
    }

    public void CheckIdentity()
    {
        if (isPalladium)
        {
            GetComponent<Data_Interp>().data = gameManager.GetComponent<CoinGeneration>().GetHighestTierCoin();
        }
        else if (isStyrofoam)
        {
            GetComponent<Data_Interp>().data = gameManager.GetComponent<CoinGeneration>().GetLowestTierCoin();
        }
        else
        {
            CoinData data = GetComponent<Data_Interp>().data;

            switch (data.Name)
            {
                case "Emerald Coin":
                    emeraldAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    //headCanvas.transform.localPosition = new Vector3(0, 0, 1.7f);
                    //tailCanvas.transform.localPosition = new Vector3(0, 0, -1.7f);
                    break;

                case "Ruby Coin":
                    rubyAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    //headCanvas.transform.localPosition = new Vector3(0, 0, 1.7f);
                    //tailCanvas.transform.localPosition = new Vector3(0, 0, -1.7f);
                    break;

                case "Sapphire Coin":
                    sapphireAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    //headCanvas.transform.localPosition = new Vector3(0, 0, 1.7f);
                    //tailCanvas.transform.localPosition = new Vector3(0, 0, -1.7f);
                    break;

                case "Diamond Coin":
                    diamondAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    //headCanvas.transform.localPosition = new Vector3(0, 0, 1.7f);
                    //tailCanvas.transform.localPosition = new Vector3(0, 0, -1.7f);
                    break;

                case "Uranium Coin":
                    uraniumAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    headCanvas.transform.localPosition = new Vector3(0, 1.4f, 0);
                    tailCanvas.transform.localPosition = new Vector3(0, -1.4f, 0);
                    intensityFromType = 0.1f;
                    pointLight.color = uranium;
                    canvasTextHead.color = yellowText;
                    canvasTextTail.color = yellowText;
                    break;

                case "Cobalt Coin":
                    cobaltAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    headCanvas.transform.localPosition = new Vector3(0, 1.4f, 0);
                    tailCanvas.transform.localPosition = new Vector3(0, -1.4f, 0);
                    intensityFromType = 0.1f;
                    pointLight.color = cobalt;
                    canvasTextHead.color = whiteText;
                    canvasTextTail.color = whiteText;
                    break;

                case "Iridium Coin":
                    iridiumAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    headCanvas.transform.localPosition = new Vector3(0, 1.4f, 0);
                    tailCanvas.transform.localPosition = new Vector3(0, -1.4f, 0);
                    intensityFromType = 0.1f;
                    pointLight.color = iridium;
                    canvasTextHead.color = whiteText;
                    canvasTextTail.color = whiteText;
                    break;

                case "Thorium Coin":
                    thoriumAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    headCanvas.transform.localPosition = new Vector3(0, 1.4f, 0);
                    tailCanvas.transform.localPosition = new Vector3(0, -1.4f, 0);
                    intensityFromType = 0.1f;
                    pointLight.color = thorium;
                    canvasTextHead.color = blackText;
                    canvasTextTail.color = blackText;
                    break;

                case "Mithril Coin":
                    mithrilAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    intensityFromType = 0.1f;
                    pointLight.color = mithril;
                    canvasTextHead.color = yellowText;
                    canvasTextTail.color = yellowText;
                    break;

                case "Adamantite Coin":
                    adamantiteAppearance.SetActive(true);
                    GetComponent<MeshRenderer>().enabled = false;
                    intensityFromType = 0.1f;
                    pointLight.color = adamantite;
                    canvasTextHead.color = yellowText;
                    canvasTextTail.color = yellowText;
                    break;

                case "Galaxy Coin":
                    pointLight.color = galaxy;
                    intensityFromType = 1f;
                    canvasTextHead.color = yellowText;
                    canvasTextTail.color = yellowText;
                    break;

                case "God Coin":
                    pointLight.color = god;
                    intensityFromType = 0.6f;
                    canvasTextHead.color = blackText;
                    canvasTextTail.color = blackText;
                    break;

                default:
                    break;
            }
        }
    }

    private void SetTransparency()
    {
        if (!isPalladium && !isStyrofoam)
        {
            CoinData data = GetComponent<Data_Interp>().data;

            Color objAppearance;

            switch (data.Name)
            {
                case "Emerald Coin":
                    objAppearance = emeraldAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                    objAppearance.a = 1;
                    emeraldAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = objAppearance;
                    //emeraldAppearance.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = objAppearance;
                    break;

                case "Ruby Coin":
                    objAppearance = rubyAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                    objAppearance.a = 1;
                    rubyAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = objAppearance;
                    //rubyAppearance.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = objAppearance;
                    break;

                case "Sapphire Coin":
                    objAppearance = sapphireAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                    objAppearance.a = 1;
                    sapphireAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = objAppearance;
                    //sapphireAppearance.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = objAppearance;
                    break;

                case "Diamond Coin":
                    objAppearance = diamondAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color;
                    objAppearance.a = 1;
                    diamondAppearance.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = objAppearance;
                    //diamondAppearance.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = objAppearance;
                    break;
            }
        }
    }

    public void ConvertToPalladium()
    {
        gameObject.GetComponent<Data_Interp>().data = gameManager.GetComponent<CoinGeneration>().GetHighestTierCoin();
        gameObject.transform.GetChild(0).gameObject.tag = "palladium_coin";
        palladiumValue = gameManager.GetComponent<CoinGeneration>().GetPalladiumValue();
        GetComponent<MeshRenderer>().enabled = false;
        palladiumAppearance.SetActive(true);
        adamantiteAppearance.SetActive(false);
        mithrilAppearance.SetActive(false);
        emeraldAppearance.SetActive(false);
        rubyAppearance.SetActive(false);
        sapphireAppearance.SetActive(false);
        diamondAppearance.SetActive(false);
        uraniumAppearance.SetActive(false);
        iridiumAppearance.SetActive(false);
        thoriumAppearance.SetActive(false);
        mithrilAppearance.SetActive(false);
        adamantiteAppearance.SetActive(false);
        cobaltAppearance.SetActive(false);
        isPalladium = true;
    }

    public void ConvertToStyrofoam()
    {
        gameObject.transform.GetChild(0).gameObject.tag = "styrofoam_coin";
        styrofoamValue = gameManager.GetComponent<CoinGeneration>().GetStyrofoamValue();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().material = styrofoamMaterial;
        palladiumAppearance.SetActive(false);
        adamantiteAppearance.SetActive(false);
        mithrilAppearance.SetActive(false);
        emeraldAppearance.SetActive(false);
        rubyAppearance.SetActive(false);
        sapphireAppearance.SetActive(false);
        diamondAppearance.SetActive(false);
        uraniumAppearance.SetActive(false);
        iridiumAppearance.SetActive(false);
        thoriumAppearance.SetActive(false);
        mithrilAppearance.SetActive(false);
        adamantiteAppearance.SetActive(false);
        cobaltAppearance.SetActive(false);
        isStyrofoam = true;
    }
}
