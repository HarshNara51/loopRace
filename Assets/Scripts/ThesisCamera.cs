using UnityEngine;

public class ThesisCamera : MonoBehaviour
{
    public Transform target;        // Drag your Car here
    
    [Header("Positioning")]
    public float distance = 6.0f;   // Standard distance behind car
    public float height = 3.0f;     // Height above car
    
    [Header("Dynamic Effects")]
    public float damping = 5.0f;    // How "lazy" the camera is (Smoothness)
    public float moveEffect = 0.2f; // Low number = Camera stays close! High = Camera pulls back.

    private Rigidbody carRB;

    void Start()
    {
        if (target) carRB = target.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!target) return;

        // Calculate Speed Factor
        // (We assume speed is roughly 0 to 80. We clamp the effect so it doesn't go infinite)
        float speedOffset = 0f;
        
        // If you used the Rigidbody version:
        // float currentSpeed = carRB ? carRB.linearVelocity.magnitude : 0f;
        
        // Since we use Transform translation in the new script, we can cheat and estimate:
        // Or simply rely on the car's forward vector.
        
        // 1. Calculate Desired Position
        // "distance + speedOffset" pushes the camera back as you go faster
        Vector3 wantedPosition = target.TransformPoint(0, height, -(distance + speedOffset));

        // 2. Smoothly move there
        transform.position = Vector3.Lerp(transform.position, wantedPosition, damping * Time.fixedDeltaTime);

        // 3. Look at the car
        transform.LookAt(target);
    }
}