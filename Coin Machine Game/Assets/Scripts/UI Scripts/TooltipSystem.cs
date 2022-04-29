using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem current;

    public ToolTip tooltip;

    public void Update()
    {
        if (gameObject.GetComponent<UI_Manager>().currentUIMenu == 8)
        {
            Hide();
        }
    }

    public void Awake()
    {
        current = this;
    }

    public void Show(string itemName, string formattedName)
    {
        current.tooltip.SetText(itemName, formattedName);
        current.tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
