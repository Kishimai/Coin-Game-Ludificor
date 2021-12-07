using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="data", menuName ="Samples/Coin")]
public class CoinData : ScriptableObject
{
    [BoxGroup("Main Information")]
    public string CoinName;
    [BoxGroup("Main Information")]
    public float baseCost;
    [BoxGroup("Main Information")]
    public int currentUpgrades, currentRankLvl;
}
