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

    public void CheckAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);

        if (achievement.State == false)
        {
            UnlockAchievement(id);
        }
    }

    public void UnlockAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Trigger();
    }

    public void DevRemoveAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Clear();
    }
}
