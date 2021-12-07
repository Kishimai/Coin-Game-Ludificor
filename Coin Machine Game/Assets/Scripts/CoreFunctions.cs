using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CoreFunctions : MonoBehaviour
{
    [BoxGroup("Core")]
    public List<CoinData> coinDatas = new List<CoinData>();

    #region  debugSystem
    [BoxGroup("Debug System")]
    public float BaseCoin;
    [BoxGroup("Debug System")]
    public float upgradesHave, buildingHaveFree;

    [ContextMenu("Calculate")]
    public void Calculate() {
        Debug.Log(BaseCoin *= 1.15f * upgradesHave - buildingHaveFree);
    }
    #endregion
}
