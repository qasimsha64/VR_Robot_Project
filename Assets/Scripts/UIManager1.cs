using UnityEngine;
using TMPro; // TextMeshPro use karne ke liye

public class UIManager1 : MonoBehaviour
{
    [Header("UI Text Component")]
    public TextMeshProUGUI telemetryText; // TextMeshPro Reference

    [Header("Robot Script References")]
    public Robot_Mover1 robotMover;
    public TrajectoryLogger logger;

    void Update()
    {
        if (robotMover != null && telemetryText != null)
        {
            Vector3 pos = robotMover.transform.position;
            float dist = logger != null ? logger.currentObstacleDistance : 0f;
            bool isBlocked = logger != null ? logger.isRobotBlocked : false;

            // Telemetry dashboard text formatting
            string statusColor = isBlocked ? "<color=red>SAFETY STOP</color>" : "<color=green>NORMAL</color>";

            telemetryText.text = $"<b>--- ROBOT TELEMETRY DASHBOARD ---</b>\n" +
                                 $"Status: {statusColor}\n" +
                                 $"Position: X: {pos.x:F2} | Y: {pos.y:F2} | Z: {pos.z:F2}\n" +
                                 $"Obstacle Distance: {dist:F2} meters";
        }
    }
}
