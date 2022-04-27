using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collections : MonoBehaviour
{

    public List<Sprite> itemImages = new List<Sprite>();
    public List<string> itemNames = new List<string>();
    public List<string> formattedName = new List<string>();
    public List<int> itemCounts = new List<int>();
    public GameObject[] menuItems;
    public GameObject menuCollection;

    private GameObject gameManager;

    private int itemMenuIndex = 0;

    private bool selecting = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game_manager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(Sprite image, string name)
    {
        //
        if (itemNames.Contains(name))
        {
            int index = itemNames.IndexOf(name);
            itemCounts[index] += 1;

            GameObject itemCount = menuItems[index].transform.GetChild(1).gameObject;

            itemCount.GetComponent<TextMeshProUGUI>().text = itemCounts[index].ToString();
        }
        else
        {
            if (itemMenuIndex < menuItems.Length)
            {
                itemImages.Add(image);
                itemNames.Add(name);
                formattedName.Add(FormatName(name));
                itemCounts.Add(1);

                menuCollection.transform.GetChild(itemMenuIndex).gameObject.SetActive(true);
                menuCollection.transform.GetChild(itemMenuIndex).gameObject.GetComponent<ToolTipTrigger>().nameOfItem = itemNames[itemMenuIndex];
                menuCollection.transform.GetChild(itemMenuIndex).gameObject.GetComponent<ToolTipTrigger>().formattedName = formattedName[itemMenuIndex];

                GameObject itemSprite = menuItems[itemMenuIndex].transform.GetChild(0).gameObject;
                GameObject itemCount = menuItems[itemMenuIndex].transform.GetChild(1).gameObject;

                itemSprite.GetComponent<Image>().sprite = itemImages[itemMenuIndex];
                itemCount.GetComponent<TextMeshProUGUI>().text = itemCounts[itemMenuIndex].ToString();

                ++itemMenuIndex;

            }
        }
    }

    public void ShowMenu()
    {
        menuCollection.SetActive(true);
    }

    public void HideMenu()
    {
        if (gameManager.GetComponent<ItemInventory>().availablePrizes > 0)
        {
            menuCollection.SetActive(false);
        }
    }

    public string GetItemName()
    {
        string itemName = "";



        return itemName;
    }

    public string FormatName(string itemName)
    {
        string formattedName = "";

        string[] words = itemName.Split('_');

        for (int i = 0; i < words.Length; ++i)
        {
            string capitalizedWord = char.ToUpper(words[i][0]) + words[i].Substring(1);
            words[i] = capitalizedWord;
        }

        formattedName = string.Join(" ", words);

        return formattedName;
    }

}
