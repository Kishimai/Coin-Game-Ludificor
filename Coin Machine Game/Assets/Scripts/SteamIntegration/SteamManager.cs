using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{

    private void Awake()
    {
        try
        {
            Steamworks.SteamClient.Init(2075770);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Couldn't Initialize Steam Client");
        }

        DontDestroyOnLoad(gameObject);

    }

    private void OnDisable()
    {
        Steamworks.SteamClient.Shutdown();
    }

    // Update is called once per frame
    void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }
}
