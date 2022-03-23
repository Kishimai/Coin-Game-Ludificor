using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collections : MonoBehaviour
{

    public List<Sprite> itemImages = new List<Sprite>();
    public List<string> itemNames = new List<string>();
    public List<int> itemCounts = new List<int>();
    public GameObject[] menuItems;

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Sprite image, string name)
    {
        if (itemNames.Contains(name))
        {
            int index = itemNames.IndexOf(name);
            itemCounts[index] += 1;
        }
        else
        {
            itemImages.Add(image);
            itemNames.Add(name);
            itemCounts.Add(1);

            GameObject itemSprite = menuItems[currentIndex].transform.GetChild(0).gameObject;
            GameObject itemCount = menuItems[currentIndex].transform.GetChild(1).gameObject;

            itemSprite.GetComponent<Image>().sprite = itemImages[currentIndex];
            itemCount.GetComponent<TextMeshProUGUI>().text = itemCounts[currentIndex].ToString();

            ++currentIndex;
        }
    }
}
