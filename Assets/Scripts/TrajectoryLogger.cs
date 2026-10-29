using UnityEngine;
using System.IO;

public class TrajectoryLogger : MonoBehaviour
{
    private string filePath;
    private Rigidbody rb;

    [Header("Sensor Reference")]
    public float currentObstacleDistance = 0.0f;
    public bool isRobotBlocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // CSV file path in persistent data path
        filePath = Path.Combine(Application.persistentDataPath, "AdvancedRobotLog.csv");

        // Write CSV Header if file doesn't exist
        if (!File.Exists(filePath))
        {
            string header = "Timestamp,PosX,PosY,PosZ,ObstacleDistance,IsBlocked\n";
            File.WriteAllText(filePath, header);
        }

        Debug.Log("Advanced Log saved at: " + filePath);
    }

    void Update()
    {
        // Continuously check raycast data for logging
        CheckSensorData();

        // Record data every 30 frames
        if (Time.frameCount % 30 == 0)
        {
            LogDetailedData();
        }
    }

    void CheckSensorData()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 5.0f))
        {
            currentObstacleDistance = hit.distance;
            isRobotBlocked = (hit.distance < 2.0f);
        }
        else
        {
            currentObstacleDistance = 5.0f; // Max range default
            isRobotBlocked = false;
        }
    }

    void LogDetailedData()
    {
        float timestamp = Time.time;
        Vector3 pos = transform.position;

        // Formatting data line with position and sensor metrics
        string dataLine = $"{timestamp:F2},{pos.x:F2},{pos.y:F2},{pos.z:F2},{currentObstacleDistance:F2},{isRobotBlocked}\n";

        // Append to CSV file
        File.AppendAllText(filePath, dataLine);
    }
}