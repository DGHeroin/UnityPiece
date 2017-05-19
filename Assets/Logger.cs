using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour {

    Queue<string> logQueue = new Queue<string>();
    string currentLog = string.Empty;
    private static Logger _ins;
    private static Logger Instance{
        get{ 
            if (_ins == null) {
                var go = new GameObject ("Logger");
                _ins = go.AddComponent<Logger> ();
                DontDestroyOnLoad (go);
            }
            return _ins;
        }
    }

    void OnApplicationQuit() {
        DestroyImmediate (_ins);
    }

    private void AppendLog(object message) {
        string new_str = message.ToString() + "\n";
        logQueue.Enqueue (new_str);

        if (logQueue.Count > 10) {
            logQueue.Dequeue ();
        }

        currentLog = string.Empty;
        foreach (string l in logQueue) {
            currentLog += l;
        }
    }
    public static void Log(object message) {
        Instance.AppendLog (message);
    }

    void OnGUI() {
        GUILayout.Label (currentLog);
    }
}
