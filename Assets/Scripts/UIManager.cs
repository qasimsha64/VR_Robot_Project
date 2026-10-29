using UnityEngine;
using UnityEngine.UI; // Standard Unity Text ke liye
using TMPro;          // TextMeshPro ke liye

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    private Text legacyText;
    private Transform robotTransform;
    private TrajectoryLogger logger;

    void Start()
    {
        // 1. Scene mein jiske paas bhi TrajectoryLogger hai, usay dhoond lo!
        logger = FindFirstObjectByType<TrajectoryLogger>();
        if (logger != null)
        {
            robotTransform = logger.transform;
            Debug.Log("✅ UIManager ko Robot mil gaya: " + robotTransform.name);
        }
        else
        {
            Debug.LogError("❌ Error: Scene mein koi Robot (TrajectoryLogger) nahi mila!");
        }

        // 2. Scene mein koi bhi UI Text dhoond lo (Chahe TMP ho ya Standard Text)
        tmpText = FindFirstObjectByType<TextMeshProUGUI>();
        legacyText = FindFirstObjectByType<Text>();

        if (tmpText == null && legacyText == null)
        {
            Debug.LogError("❌ Error: Scene mein koi UI Text component nahi mila! Canvas ke andar text check karein.");
        }
        else
        {
            Debug.Log("✅ UIManager ko UI Text mil gaya hai!");
        }
    }

    void Update()
    {
        string status = "NORMAL";
        float dist = 0f;
        Vector3 pos = Vector3.zero;

        if (robotTransform != null)
        {
            pos = robotTransform.position;
            if (logger != null)
            {
                dist = logger.currentObstacleDistance;
                status = logger.isRobotBlocked ? "SAFETY STOP" : "NORMAL";
            }
        }

        // Display text format
        string displayText = $"--- ROBOT TELEMETRY ---\n" +
                             $"Status: {status}\n" +
                             $"Position: X:{pos.x:F1} Z:{pos.z:F1}\n" +
                             $"Distance: {dist:F2}m";

        // Jo bhi text component available ho, us par text show kar do
        if (tmpText != null)
        {
            tmpText.text = displayText;
        }
        else if (legacyText != null)
        {
            legacyText.text = displayText;
        }
    }
}