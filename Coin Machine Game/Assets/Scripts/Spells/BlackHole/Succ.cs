using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succ : MonoBehaviour
{
    public void ActivateCoins()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

        GameObject[] blackHoleCoins = GameObject.FindGameObjectsWithTag("black_hole");

        foreach (GameObject child in blackHoleCoins)
        {
            child.GetComponentInParent<BlackHoleCoin>().Absorb();
        }

        gameObject.SetActive(false);

        cam.GetComponent<CoinPlacement>().activeBlackHoles = new List<GameObject>();
    }
}
