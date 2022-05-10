using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
[RequireComponent(typeof(Rigidbody))]
public class AntiRollBar : MonoBehaviour
{
    private CarController carController;
    private Rigidbody rb;
    public Transform centerOfMassTr;
    public float antiRollForce = 5000f;

    void Awake()
    {
        carController = GetComponent<CarController>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (centerOfMassTr)
        {
            rb.centerOfMass = centerOfMassTr.localPosition;
        }
        else
        {
            Debug.Log("This car does not have CenterOfMass gameObject");
        }
    }

    private void FixedUpdate()
    {
        CheckWheelGrounded(carController.frontWheels[0].mywheelcollider, carController.frontWheels[1].mywheelcollider);
        CheckWheelGrounded(carController.backWheels[0].mywheelcollider, carController.backWheels[1].mywheelcollider);
    }

    private void CheckWheelGrounded(WheelCollider rightWC, WheelCollider leftWC)
    {
        WheelHit hit;
        float leftPressure = 1f;
        float rightPressure = 1f;

        bool leftWheelGrounded = leftWC.GetGroundHit(out hit);
        if (leftWheelGrounded)
        {
            //distancia entre el centro de la rueda y el suelo
            leftPressure = (-leftWC.transform.InverseTransformPoint(hit.point).y - leftWC.radius) / leftWC.suspensionDistance;
        }

        bool rightWheelGrounded = rightWC.GetGroundHit(out hit);
        if (rightWheelGrounded)
        {
            //distancia entre el centro de la rueda y el suelo
            rightPressure = (-rightWC.transform.InverseTransformPoint(hit.point).y - rightWC.radius) / rightWC.suspensionDistance;
        }

        float antiRollForceAux = (leftPressure - rightPressure) * antiRollForce;

        if (leftWheelGrounded)
        {
            rb.AddForceAtPosition(leftWC.transform.up * -antiRollForceAux, leftWC.transform.position);
        }

        if (rightWheelGrounded)
        {
            rb.AddForceAtPosition(rightWC.transform.up * -antiRollForceAux, rightWC.transform.position);
        }
    }
}