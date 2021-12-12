using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ShopSystem : MonoBehaviour
{
    [BoxGroup("Core Input")]
    public List<GameObject> placeholder_data = new List<GameObject>();

    /// <sumamry>
    /// Calculates the next Total pay rate using the Algorithm
    /// </summary>
    public void CalculatePayRate(float BaseUp, float UpgradesHave, float FreeUpgrades){ 
        BaseUp *= 1.15f * UpgradesHave - FreeUpgrades;
    }

    public void Start() {
       // Update Current Datas   
    }

    public void Update_Data(){

    }
}
