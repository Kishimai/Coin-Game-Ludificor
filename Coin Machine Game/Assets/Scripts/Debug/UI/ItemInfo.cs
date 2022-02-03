using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

    public GameObject gameManager;

    public Text valueModifier;
    private float modifier;

    // Start is called before the first frame update

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game_manager");
        valueModifier = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        modifier = gameManager.GetComponent<ItemInventory>().coinValueModifier;
        valueModifier.text = string.Format("Coin Value: {0}%", Mathf.CeilToInt(modifier * 100));


    }
}
