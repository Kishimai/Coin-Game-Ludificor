using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestFriend : MonoBehaviour
{
    public GameObject deleter;
    public GameObject lightBar;
    public GameObject lightRail;
    public GameObject spotlight;
    public GameObject dropArea;
    public GameObject coin;
    public CoinGeneration generation;
    public float xMin;
    public float xMax;
    public float yClamp;
    public float zClamp;
    public float dropCooldown;
    public float defaultCooldown = 8f;
    public float timeUntilReady;
    private bool readyToPlay = false;

    public List<GameObject> lights = new List<GameObject>();

    public Material lightBarOff;
    public Material lightBarOn;

    public Material lightRailOff;
    public Material lightRailOn;

    // Start is called before the first frame update
    void Start()
    {
        lightBar.GetComponent<MeshRenderer>().material = lightBarOff;
        CompileLights();
        TurnRailOff();

        timeUntilReady = Random.Range(0.5f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToPlay)
        {
            if (timeUntilReady > 0)
            {
                timeUntilReady -= Time.deltaTime;
            }
            else
            {
                if (dropCooldown > 0)
                {
                    dropCooldown -= Time.deltaTime;
                }
                else
                {
                    DropCoin();
                    timeUntilReady = Random.Range(0.5f, 1.25f);
                }
            }
        }
    }

    public void ActivateFriend()
    {
        readyToPlay = true;
        TurnRailOn();
        lightBar.GetComponent<MeshRenderer>().material = lightBarOn;
        spotlight.SetActive(true);
        deleter.SetActive(false);
    }

    private void CompileLights()
    {
        foreach (Transform child in lightRail.transform)
        {
            lights.Add(child.gameObject);
        }
    }

    private void TurnRailOff()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<MeshRenderer>().material = lightRailOff;
        }
    }
    private void TurnRailOn()
    {
        foreach (GameObject light in lights)
        {
            light.GetComponent<MeshRenderer>().material = lightRailOn;
        }
    }

    private void DropCoin()
    {
        GameObject newCoin;
        Vector3 clampedPosition;

        float x = Random.Range(xMin, xMax);
        float y = yClamp;
        float z = zClamp;

        newCoin = Instantiate(coin, dropArea.transform.position, Quaternion.Euler(90, 0, 0));

        newCoin.GetComponent<Data_Interp>().data = generation.GetFriendCoin();

        newCoin.transform.position = dropArea.transform.position;

        newCoin.transform.position = new Vector3(newCoin.transform.position.x + x, y, z);

        dropCooldown = defaultCooldown;
    }
}
