using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCapsuleSelection : MonoBehaviour
{

    public bool selectingItem;

    public GameObject gameManager;

    public string[] items = new string[] {"midas_shard", "midas_relic"};

    // Start is called before the first frame update
    void Start()
    {
        selectingItem = false;
        gameManager = GameObject.FindGameObjectWithTag("game_manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (selectingItem)
        {
            // Calls Update_UI method in UI_Manager with the int value of 6 (6 shows the UI for selecting an item from an item capsule)
            gameManager.GetComponent<UI_Manager>().Update_UI(6);
            selectingItem = false;
        }
    }
}
