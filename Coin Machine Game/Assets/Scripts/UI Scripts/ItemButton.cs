using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public string[] listOfItems;

    public string selectedItem;

    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        listOfItems = GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemCapsuleSelection>().items;
    }

    void Awake()
    {
        listOfItems = GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemCapsuleSelection>().items;
        selectedItem = listOfItems[Random.Range(0, listOfItems.Length)];
        buttonText.text = selectedItem;
    }

    public void GetItem()
    {
        GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>().collectedItems.Add(selectedItem);
    }
}
