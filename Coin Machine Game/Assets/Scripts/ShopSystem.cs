using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ShopSystem : MonoBehaviour
{
    

    public void CalculatePayRate(float BaseUp, float UpgradesHave, float FreeUpgrades){
        BaseUp *= 1.15f * UpgradesHave - FreeUpgrades;
    }
}
