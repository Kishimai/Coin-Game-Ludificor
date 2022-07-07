using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugLogger : MonoBehaviour
{

    string fileName = "";

    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            Application.logMessageReceived += Log;
        }

    }

    private void OnDisable()
    {
        if (!Application.isEditor)
        {
            Application.logMessageReceived -= Log;
        }

    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (!Application.isEditor)
        {
            string saveDir = Application.streamingAssetsPath + "/errorlogs/";

            Directory.CreateDirectory(saveDir);

            fileName = saveDir + "LogFile.txt";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(fileName, true);

        tw.WriteLine("[" + System.DateTime.Now + "] " + logString);

        tw.Close();
    }
}
