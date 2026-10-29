using UnityEngine;

public class Robot_Color_Check : MonoBehaviour
{
    private Renderer robotRenderer;

    // Materials assign karne ke liye Unity Inspector mein fields
    public Material greenMaterial;
    public Material redMaterial;

    private Rigidbody rb;

    void Start()
    {
        robotRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb == null) return;

        // Check karein ke robot move kar raha hai ya ruka hua hai (velocity ya speed ke zariye)
        float currentSpeed = rb.linearVelocity.magnitude;

        if (currentSpeed > 0.1f)
        {
            // Jab robot move kar raha ho -> Green
            if (robotRenderer.material != greenMaterial)
                robotRenderer.material = greenMaterial;
        }
        else
        {
            // Jab robot ruka ho -> Red
            if (robotRenderer.material != redMaterial)
                robotRenderer.material = redMaterial;
        }
    }
}