using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Keyboard_Use : MonoBehaviour
{
    [Header("Keyboard Movement Settings")]
    public float keyboardSpeed = 4.0f;
    public float turnSpeed = 90.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            // Freeze X and Z rotations so the robot doesn't tip over
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void FixedUpdate()
    {
        // WASD / Arrow Keys Input
        float moveInput = Input.GetAxis("Vertical");   // W / S or Up / Down
        float turnInput = Input.GetAxis("Horizontal"); // A / D or Left / Right

        // 1. Forward / Backward Movement
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            Vector3 moveDirection = transform.forward * moveInput * keyboardSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveDirection);
        }

        // 2. Left / Right Steering
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            float turnDegrees = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turnDegrees, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}