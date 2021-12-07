using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class CollectionPlaceholder : MonoBehaviour
{
    [BoxGroup("Information")]
    public Collection _dataCollection;

    public void Start() {
        if(_dataCollection.collection_art != null)
            GetComponent<Image>().sprite = _dataCollection.collection_art;
    } 
}
