using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleration = 500f;
    public float steering = 15f;
    public float brakeForce = 800f;

    [Header("Wheels & Physics")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;
    public Transform flMesh;
    public Transform frMesh;
    public Transform rlMesh;
    public Transform rrMesh;

    [Header("Camera")]
    public Transform cameraTarget;
    public float cameraSmooth = 5f;
    public Vector3 cameraOffset = new Vector3(0, 5, -10);

    private Rigidbody rb;
    private CarControls controls;
    private float throttleInput;
    private float steerInput;
    private float brakeInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new CarControls();

        // Bind inputs
        controls.Driving.Throttle.performed += ctx => throttleInput = ctx.ReadValue<float>();
        controls.Driving.Throttle.canceled += ctx => throttleInput = 0;

        controls.Driving.Steer.performed += ctx => steerInput = ctx.ReadValue<float>();
        controls.Driving.Steer.canceled += ctx => steerInput = 0;

        controls.Driving.Brake.performed += ctx => brakeInput = 1f;
        controls.Driving.Brake.canceled += ctx => brakeInput = 0;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void FixedUpdate()
    {
        // Apply motor torque
        float motor = throttleInput * acceleration;
        frontLeft.motorTorque = motor;
        frontRight.motorTorque = motor;

        // Apply steering
        float steer = steerInput * steering;
        frontLeft.steerAngle = steer;
        frontRight.steerAngle = steer;

        // Apply brakes
        float brake = brakeInput * brakeForce;
        frontLeft.brakeTorque = brake;
        frontRight.brakeTorque = brake;
        rearLeft.brakeTorque = brake;
        rearRight.brakeTorque = brake;

        // Update wheel meshes
        UpdateWheelPose(frontLeft, flMesh);
        UpdateWheelPose(frontRight, frMesh);
        UpdateWheelPose(rearLeft, rlMesh);
        UpdateWheelPose(rearRight, rrMesh);
    }

    void LateUpdate()
    {
        // Simple follow camera
        if (Camera.main != null && cameraTarget != null)
        {
            Vector3 targetPos = cameraTarget.position + cameraOffset;
            Camera.main.transform.position = Vector3.Lerp(
                Camera.main.transform.position,
                targetPos,
                Time.deltaTime * cameraSmooth
            );
            Camera.main.transform.LookAt(cameraTarget);
        }
    }

    void UpdateWheelPose(WheelCollider collider, Transform mesh)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        mesh.position = pos;
        mesh.rotation = quat;
    }
}