using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{

    UI_Manager uiManager;
    ItemInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = gameObject.GetComponent<UI_Manager>();
        inventory = gameObject.GetComponent<ItemInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            uiManager._currentCoin += 1000000;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            inventory.availablePrizes += 5;
        }
    }
}
