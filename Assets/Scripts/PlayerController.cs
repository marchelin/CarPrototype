using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CarCircuitManager carCircuitManager;
    private CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();
        carCircuitManager = GetComponent<CarCircuitManager>();
    }

    void Update()
    {
        // Reset Car
        if (Input.GetKeyDown(KeyCode.R))
        {
            carController.ResetCar(carCircuitManager.lastGatewayPassed);
        }
    }

    private void FixedUpdate()
    {
        float torqueInput = Input.GetAxis("Vertical");
        float brakeInput  = Input.GetAxis("Jump");
        float steerInput  = Input.GetAxis("Horizontal");

        carController.ForwardMovement(torqueInput);
        carController.SteerMovement(steerInput);
        carController.BrakeMovement(brakeInput);
    }
}