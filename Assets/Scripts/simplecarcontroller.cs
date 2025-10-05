using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    public float acceleration = 500f;
    public float steering = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
    }

    void FixedUpdate()
    {
        // Simple legacy input
        float moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Move car forward/backward
        Vector3 force = transform.forward * moveInput * acceleration * Time.fixedDeltaTime;
        rb.AddForce(force);

        // Steer the car
        transform.Rotate(0, turnInput * steering * Time.fixedDeltaTime, 0);
    }
}