using UnityEngine;
using TMPro;

public class ThesisCarController : MonoBehaviour
{
    [Header("Engine Specs")]
    public float maxSpeed = 80f;        
    public float reverseSpeed = 30f;    // Slower speed for reversing
    public float acceleration = 30f;    
    public float brakePower = 60f;      // Stronger than acceleration for snappy stops
    public float coastingDrag = 15f;    // Natural slowdown when letting go of gas
    public float turnSpeed = 100f;      

    [Header("Stability")]
    public float gravity = 20f;         
    public float rideHeight = 1.0f;     
    public float raycastLength = 3.0f;
    public float smoothTime = 10f; 
    public LayerMask roadLayers;    

    [Header("Visuals")]
    public Transform[] frontWheels; 
    public Transform[] rearWheels;  
    public float wheelSpinSpeed = 200f;

    [Header("UI")]
    public TMP_Text speedometerText;

    private float currentSpeed = 0f;
    private float verticalVelocity = 0f; 

    void Update()
    {
        // Anti-Flip: Keep car upright
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        AnimateWheels();
        UpdateUI();
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplySuspension();
    }

    void HandleMovement()
    {
        float gasInput = Input.GetAxis("Vertical"); 

        // --- SMART BRAKING LOGIC ---
        if (gasInput > 0)
        {
            // Accelerating Forward
            currentSpeed += acceleration * gasInput * Time.fixedDeltaTime;
        }
        else if (gasInput < 0)
        {
            // Pressing Down/S
            if (currentSpeed > 0.5f) 
            {
                // If moving forward -> BRAKE (Reduce speed)
                currentSpeed -= brakePower * Time.fixedDeltaTime;
            }
            else
            {
                // If stopped (or already reversing) -> REVERSE
                currentSpeed += acceleration * gasInput * Time.fixedDeltaTime;
            }
        }
        else
        {
            // No Input -> Coast to a stop (Drag)
            if (currentSpeed > 0) currentSpeed -= coastingDrag * Time.fixedDeltaTime;
            else if (currentSpeed < 0) currentSpeed += coastingDrag * Time.fixedDeltaTime;

            // Snap to 0 if very slow
            if (Mathf.Abs(currentSpeed) < 0.5f) currentSpeed = 0;
        }

        // Clamp Speed (Different max for Forward vs Reverse)
        if (currentSpeed > 0) currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        else currentSpeed = Mathf.Clamp(currentSpeed, -reverseSpeed, 0);


        // --- STEERING ---
        if (Mathf.Abs(currentSpeed) > 0.5f)
        {
            float turnInput = Input.GetAxis("Horizontal");
            // Reverse steering only when actually moving backwards
            float direction = currentSpeed > 0 ? 1 : -1; 
            transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime * direction);
        }

        // --- APPLY MOVE ---
        transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);
    }

    void ApplySuspension()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + (Vector3.up * 1.0f); 

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastLength, roadLayers))
        {
            Vector3 targetPosition = transform.position;
            targetPosition.y = hit.point.y + rideHeight;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.fixedDeltaTime);
            verticalVelocity = 0; 
        }
        else
        {
            verticalVelocity -= gravity * Time.fixedDeltaTime;
            transform.Translate(Vector3.up * verticalVelocity * Time.fixedDeltaTime, Space.World);
        }
    }

    void AnimateWheels()
    {
        float turnInput = Input.GetAxis("Horizontal");
        float spin = currentSpeed * wheelSpinSpeed * Time.deltaTime;

        if (frontWheels != null) {
            foreach (Transform w in frontWheels) {
                if(w) { 
                   w.Rotate(Vector3.right, spin); 
                   w.localRotation = Quaternion.Euler(w.localRotation.eulerAngles.x, turnInput * 30f, 0);
                }
            }
        }
        if (rearWheels != null) {
            foreach (Transform w in rearWheels) {
                if(w) w.Rotate(Vector3.right, spin);
            }
        }
    }

    void UpdateUI()
    {
        if (speedometerText != null)
            speedometerText.text = Mathf.RoundToInt(currentSpeed).ToString() + " MPH";
    }
}