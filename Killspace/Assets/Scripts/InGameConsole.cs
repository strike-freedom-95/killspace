using UnityEngine;
using System.Text;

public class InGameConsole : MonoBehaviour
{
    private StringBuilder logBuilder = new StringBuilder();
    private bool showConsole = false;
    private Vector2 scrollPos;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logBuilder.AppendLine($"[{type}] {logString}");

        if (type == LogType.Error || type == LogType.Exception)
        {
            logBuilder.AppendLine(stackTrace);
        }
    }

    void Update()
    {
        // Toggle console with 3-finger tap (mobile-friendly)
        if (Input.touchCount == 3)
        {
            showConsole = !showConsole;
        }
    }

    void OnGUI()
    {
        if (!showConsole) return;

        GUI.backgroundColor = Color.black;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 300));
        GUILayout.Label("IN-GAME CONSOLE");

        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.TextArea(logBuilder.ToString());
        GUILayout.EndScrollView();

        if (GUILayout.Button("Clear"))
        {
            logBuilder.Clear();
        }

        GUILayout.EndArea();
    }
}
