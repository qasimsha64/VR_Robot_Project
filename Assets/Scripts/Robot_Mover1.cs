using UnityEngine;

public class Robot_Mover1 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5.0f;
    public float safeDistance = 2.0f;

    private Rigidbody rb;
    private bool isBlocked = false;
    private VRCommandListener commandListener; // Listener ka reference

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Robot par lagi hui VRCommandListener script ko fetch karna
        commandListener = GetComponent<VRCommandListener>();
    }

    void Update()
    {
        // Raycast sensor check (Safety Stop)
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, safeDistance))
        {
            if (hit.distance < safeDistance)
            {
                isBlocked = true;
                Debug.Log("Safety Stop! Obstacle at: " + hit.distance.ToString("F2") + "m");
            }
        }
        else
        {
            isBlocked = false;
        }
    }

    void FixedUpdate()
    {
        float moveVertical = 0.0f;
        float moveHorizontal = 0.0f;

        // --- NETWORK COMMAND CHECK ---
        if (commandListener != null)
        {
            string cmd = commandListener.latestCommand;

            // Agar network se command aayi hai
            if (cmd.Contains("MOVE_FORWARD"))
            {
                moveVertical = 1.0f; // Forward move karo
            }
            else if (cmd.Contains("STOP"))
            {
                moveVertical = 0.0f;
            }
        }
        else
        {
            // Agar listener nahi hai toh fallback to keyboard
            moveVertical = Input.GetAxis("Vertical");
            moveHorizontal = Input.GetAxis("Horizontal");
        }

        // Safety check: Agar obstacle qareeb hai aur forward command hai, toh rok do
        if (isBlocked && moveVertical > 0)
        {
            moveVertical = 0.0f;
        }

        // Velocity apply karna
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 targetVelocity = movement * speed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }
}