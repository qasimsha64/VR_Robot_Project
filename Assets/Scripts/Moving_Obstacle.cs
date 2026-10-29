using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3.0f;
    public float distance = 100.0f; // Kitni door tak move karega

    private Vector3 startPosition;

    void Start()
    {
        // Initial position save karna jahan se obstacle shuru hua tha
        startPosition = transform.position;
    }

    void Update()
    {
        // Mathf.PingPong use karke obstacle ko aage-peeche move karwana
        float zMovement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(startPosition.x + zMovement, startPosition.y, startPosition.z );
    }
}