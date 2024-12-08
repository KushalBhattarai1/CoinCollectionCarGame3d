using UnityEngine;
using UnityEngine.InputSystem;

public class carController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private WheelCollider wheelFrontLeft, wheelFrontRight, wheelBackLeft, wheelBackRight;

    [SerializeField] private Transform wheelForwardLeft, wheelForwardRight, wheelBackwardLeft, wheelBackwardRight;

    private Vector2 carmovement = Vector2.zero;

    [SerializeField] private float motorSpeed, breakForce, streeingAngle;

    void OnMovement(InputValue keyvalue)
    {
        carmovement = keyvalue.Get<Vector2>();
    }

    void OnBreak(InputValue breakctx)
    {
        if (breakctx.isPressed)
        {
            wheelFrontLeft.brakeTorque = breakForce;
            wheelFrontRight.brakeTorque = breakForce;
            wheelBackLeft.brakeTorque = breakForce;
            wheelBackRight.brakeTorque = breakForce;
        }
        else
        {
            wheelFrontLeft.brakeTorque = 0;
            wheelFrontRight.brakeTorque = 0;
            wheelBackLeft.brakeTorque = 0;
            wheelBackRight.brakeTorque = 0;
        }

    }


    private void Update()
    {
        //add motorForce
        wheelFrontLeft.motorTorque = motorSpeed * carmovement.y;
        wheelFrontRight.motorTorque = motorSpeed * carmovement.y;

        //steering wheel
        wheelFrontRight.steerAngle = streeingAngle * carmovement.x;
        wheelFrontLeft.steerAngle = streeingAngle * carmovement.x;

        //update colider data to visual 
        updateVisual(wheelFrontLeft, wheelForwardLeft);
        updateVisual(wheelBackLeft, wheelBackwardLeft);
        updateVisual(wheelFrontRight, wheelForwardRight);
        updateVisual(wheelBackRight, wheelBackwardRight);
    }


    void updateVisual(WheelCollider wheelcol, Transform wheeltrans)
    {

        Vector3 pos;
        Quaternion rot;

        wheelcol.GetWorldPose(out pos, out rot);

        wheeltrans.position = pos;
        wheeltrans.rotation = rot;
    }
}
