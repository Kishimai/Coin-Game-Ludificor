using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXPercentCalc : MonoBehaviour
{
  public Text percent;
  public float valueFloat;
  public string value;
  public Slider slider;



    // Update is called once per frame
    void Update()
    {
      valueFloat = slider.value;
      valueFloat = Mathf.Round(valueFloat*100) * 1f;
      value = (valueFloat.ToString());
      percent.text = value;
    }

}
