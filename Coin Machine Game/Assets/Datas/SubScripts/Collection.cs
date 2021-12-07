using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Samples/Collection")]
public class Collection : ScriptableObject
{
    [BoxGroup("Information")]
    public string colleciton_name;
    [BoxGroup("Information")]
    public Sprite collection_art;
    [BoxGroup("Information")]
    public float collection_baseValue;
}
