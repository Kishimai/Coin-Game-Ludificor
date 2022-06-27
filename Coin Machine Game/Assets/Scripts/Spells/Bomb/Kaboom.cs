using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaboom : MonoBehaviour
{
    public void DetonateAllBombCoins()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

        GameObject[] interactionSpheres = GameObject.FindGameObjectsWithTag("bomb_coin");

        foreach (GameObject item in interactionSpheres)
        {

            item.GetComponentInParent<BombCoin>().Explode();

        }

        gameObject.SetActive(false);

        cam.GetComponent<CoinPlacement>().activeBombs = new List<GameObject>();
    }
}
