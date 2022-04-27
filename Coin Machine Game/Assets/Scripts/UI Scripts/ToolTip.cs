using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public GameObject gameManager;
    public ItemInventory inventory;

    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("game_manager").GetComponent<ItemInventory>();
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        mousePos = new Vector2(mousePos.x + 20, mousePos.y - 20);

        transform.position = mousePos;
    }

    public void SetText(string itemName, string formattedName)
    {
        string description;

        description = inventory.GetDescription(itemName);

        headerField.text = formattedName;
        contentField.text = description;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        if (headerLength > characterWrapLimit || contentLength > characterWrapLimit)
        {
            layoutElement.enabled = true;
        }
        else
        {
            layoutElement.enabled = false;
        }
    }

}
