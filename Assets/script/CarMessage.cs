using UnityEngine;

public class CarMessage : MonoBehaviour
{
    public float crashRotationThreshold = 90f; // Threshold for detecting if the car is flipped
    public float minVelocity = 0.1f; // Minimum velocity to consider the car as not moving
    public float impactForceThreshold = 10f; // Threshold for detecting significant impact force

    private bool hasCrashed = false;
    private Rigidbody carRigidbody;
    private Quaternion initialRotation;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        if (carRigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on car object.");
        }

        // Store the initial rotation of the car
        initialRotation = carRigidbody.transform.rotation;
    }

    void Update()
    {
        if (!hasCrashed && IsCarCrashed())
        {
            hasCrashed = true;
            Debug.Log("Car Crashed!!");
        }
    }

    private bool IsCarCrashed()
    {
        if (carRigidbody == null) return false;

        // Check if the car is significantly rotated from its initial rotation
        if (IsCarFlipped())
        {
            // Check if the car is moving slowly
            bool isNotMoving = carRigidbody.linearVelocity.magnitude < minVelocity;

            // Check if there was a significant impact force
            bool hadImpact = carRigidbody.linearVelocity.magnitude > impactForceThreshold;

            // If the car is flipped, not moving, and had an impact, it's considered crashed
            return isNotMoving && hadImpact;
        }

        return false;
    }

    private bool IsCarFlipped()
    {
        // Calculate the car's current rotation relative to its initial rotation
        Vector3 currentEulerAngles = carRigidbody.transform.eulerAngles;
        Vector3 initialEulerAngles = initialRotation.eulerAngles;

        // Calculate rotation differences
        float xDifference = Mathf.Abs(NormalizeAngle(currentEulerAngles.x - initialEulerAngles.x));
        float yDifference = Mathf.Abs(NormalizeAngle(currentEulerAngles.y - initialEulerAngles.y));
        float zDifference = Mathf.Abs(NormalizeAngle(currentEulerAngles.z - initialEulerAngles.z));

        // Determine if the car is flipped beyond the threshold
        bool isFlipped = xDifference >= crashRotationThreshold ||
                          yDifference >= crashRotationThreshold ||
                          zDifference >= crashRotationThreshold;

        return isFlipped;
    }

    // Normalize angle to range [0, 360) degrees
    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }
}
