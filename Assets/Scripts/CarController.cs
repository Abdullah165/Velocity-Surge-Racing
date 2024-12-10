using System;
using Fusion;
using UnityEngine;

public class CarController : NetworkBehaviour
{
    [SerializeField] private WheelCollider[] frontWheels;
    [SerializeField] private WheelCollider[] backWheels;
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private Vector3 centerOfMass;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    public override void FixedUpdateNetwork()
    {
        var motoT = Input.GetAxis("Vertical") * 1000 * maxAcceleration * Runner.DeltaTime;
        backWheels[0].motorTorque = motoT;
        backWheels[1].motorTorque = motoT;
        
        var steerA = Input.GetAxis("Horizontal") * maxSteeringAngle;
        frontWheels[0].steerAngle = steerA;
        frontWheels[1].steerAngle = steerA;

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.001f)
        {
            backWheels[0].brakeTorque = 0;
            backWheels[1].brakeTorque = 0;
        }
        else
        {
            backWheels[0].brakeTorque = 1000;
            backWheels[1].brakeTorque = 1000;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            GameManager.instance.ShowGameOverScreen();
            GameManager.instance.ISGameOver = true;
        }
    }
}
