using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TooltipSystem system;
    public string nameOfItem;
    public string formattedName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        system.Show(nameOfItem, formattedName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        system.Hide();
    }
}
