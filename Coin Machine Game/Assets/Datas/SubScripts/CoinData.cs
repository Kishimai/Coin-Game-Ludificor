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
    public float BaseCost;
    [BoxGroup("Core Information")]
    public float StartingValue;
    [BoxGroup("Core Information")]
    public float AddPerLevel;
    [BoxGroup("Core Information")]
    public bool Unlocked;
    [BoxGroup("Core Information")]
    public Sprite Art;
    [BoxGroup("Core Information")]
    [TextArea(8, 8)]
    public string CoinDescription;

}
