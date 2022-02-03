using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public string selectedItem;

    public Text buttonText;

    private void OnEnable()
    {
        selectedItem = GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemRandomizer>().RollNewItem();
        buttonText.text = selectedItem;
    }

    public void GetItem()
    {
        GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>().newItem = selectedItem;
    }
}
