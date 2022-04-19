using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public GameObject gameManager;

    public GameObject textObject;
    private TextMesh textMesh;

    private string toolTipText = "";
    private string itemName = "";
    private RectTransform backgroundRectTransform;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("game_manager");
        textMesh = textObject.GetComponent<TextMesh>();
    }

    private void Awake()
    {
        backgroundRectTransform = gameObject.GetComponent<RectTransform>();
        toolTipText = gameManager.GetComponent<ItemInventory>().GetItemDescription(itemName);
    }

    private void ShowTooltip()
    {
        gameObject.SetActive(true);

        textObject.GetComponent<TextMesh>().text = toolTipText;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
