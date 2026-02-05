using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get Input
        float move = Input.GetAxis("Vertical") * speed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // Move the car (Simple Force)
        Vector3 forwardMove = transform.forward * move;
        rb.AddForce(forwardMove, ForceMode.Acceleration);

        // Turn the car
        if (move != 0) // Only turn if moving
        {
            Quaternion turnRotation = Quaternion.Euler(0f, turn * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}