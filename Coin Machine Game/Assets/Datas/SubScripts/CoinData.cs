using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="data", menuName ="Game Data/Coin")]
public class CoinData : ScriptableObject
{
    [BoxGroup("Core Information")]
    public string Name;
    [BoxGroup("Core Information")]
    public double BaseCost;
    [BoxGroup("Core Information")]
    public double StartingValue;
    [BoxGroup("Core Information")]
    public double AddPerLevel;
    [BoxGroup("Core Information")]
    public int Order; // Order by which they are Unlocked
    [BoxGroup("Core Information")]
    public Material materialColor;
    [BoxGroup("Core Information")]
    public Sprite CoinArt;
    [BoxGroup("Core Information")]
    [TextArea(8, 8)]
    public string CoinDescription;

    [BoxGroup("Current Information")]
    public double currentCost;
    [BoxGroup("Current Information")]
    public double currentValue;
    [BoxGroup("Current Information")]
    public bool Unlocked;
    [BoxGroup("Current Information")]
    public int CurrentLevel; // Max 5 current is one once passed upgraded
    [BoxGroup("Current Information")]
    public int levelsForFree;

}
