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

    public int gildedModifier = 0;
    public int crystalModifier = 0;

    public int totalValueModifier = 0;

    public bool inPlinkoZone;

    public int coinLayer = 11;

    // Start is called before the first frame update
    void Start()
    {
        coinRb = GetComponent<Rigidbody>();
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
    }

    // Update is called once per frame
    void Update()
    {
        // Stops coins from flying around randomly when the game is still in initialization phase
        if (eventManager.GetComponent<EventsManager>().initializationPhase)
        {
            coinRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            coinRb.constraints = RigidbodyConstraints.None;
        }

        totalValueModifier = gildedModifier + crystalModifier;

        if (!Mathf.Approximately(totalValueModifier, 0))
        {
            coinCanvas.SetActive(true);
            canvasTextHead.text = string.Format("{0}x", totalValueModifier);
            canvasTextTail.text = string.Format("{0}x", totalValueModifier);
        }
        else
        {
            coinCanvas.SetActive(false);
        }
    }

    public void ActivateBumper()
    {
        guildedBumper.SetActive(true);
    }

    public void ActivateCrystalShell()
    {
        crystalShell.SetActive(true);
    }


}
