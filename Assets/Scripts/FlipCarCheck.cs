using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
[RequireComponent(typeof(Rigidbody))]
public class FlipCarCheck : MonoBehaviour
{
    private CarController carController;
    private Rigidbody rb;
    private CarCircuitManager carCircuitManager;

    private float lastTimeCheckOk;
    private float timeToResetCar = 3f;

    void Awake()
    {
        carController = GetComponent<CarController>();
        rb = GetComponent<Rigidbody>();
        carCircuitManager = GetComponent<CarCircuitManager>();
    }

    void Update()
    {
        if (carController.transform.up.y > 0.5f || rb.velocity.magnitude > 1f)
        {
            lastTimeCheckOk = Time.time;
        }

        if (Time.time > lastTimeCheckOk + timeToResetCar)
        {
            carController.ResetCar(carCircuitManager.lastGatewayPassed);
        }
    }
}