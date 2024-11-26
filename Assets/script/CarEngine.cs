using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    public Vector3 initialPosition;  // Store the initial position of the car
    public Quaternion initialRotation;  // Store the initial rotation of the car
    public Rigidbody carRigidbody;  // Reference to the car's Rigidbody
    public TextMeshProUGUI crashMessageTMP;  // Reference to a TextMeshProUGUI element for the crash message
    public float resetDelay = 2.0f;  // Time to wait before resetting the car

    void Start()
    {
        // Store the initial position and rotation of the car
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the car has crashed (you can add specific conditions here)
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Display the crash message using TextMeshProUGUI
            crashMessageTMP.text = "Car Crashed! Resetting...";

            // Disable the car's movement temporarily
            carRigidbody.isKinematic = true;

            // Start the reset process after a delay
            Invoke("ResetCar", resetDelay);
        }
    }

    void ResetCar()
    {
        // Reset the car's position and rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Re-enable the car's movement
        carRigidbody.isKinematic = false;

        // Clear the crash message
        crashMessageTMP.text = "";
    }
}
