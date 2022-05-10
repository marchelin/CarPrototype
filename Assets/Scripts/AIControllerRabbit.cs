using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
[RequireComponent(typeof(AvoidanceController))] /////////////////////////// Activar script cuando lo tenga ready y sin errores en el inspector
public class AIControllerRabbit : MonoBehaviour
{
    private CarController carController;
    private AvoidanceController avoidanceController;

    public GameObject rabbit;
    private WaypointsFollower rabbitScript;

    public float acelerationSensitivity = 1f;
    public float brakingSensitivity = 0.1f;
    public float steeringSensitivity = 0.01f;

    public float cornerDegrees;

    void Start()
    {
        if (rabbit != null)
        {
            rabbitScript = rabbit.GetComponent<WaypointsFollower>();
            carController = GetComponent<CarController>();
            avoidanceController = GetComponent<AvoidanceController>();
        }
    }

    void Update()
    {
        Debug.DrawLine(transform.position, rabbit.transform.position, Color.blue);

        float distanceToRabbit = Vector3.Distance(rabbit.transform.position, transform.position);
        rabbitScript.speed = Mathf.Lerp(0f, carController.currentSpeed * 1.1f, 1f - distanceToRabbit / 300f);
    }

    float prevTurnThreshold = 1f;

    void FixedUpdate()
    {
        Vector3 localTarget;

        if(Time.time < avoidanceController.timeForAvoiding)
        {
            localTarget = transform.right * 10f;
        }
        else
        {
            localTarget = transform.InverseTransformPoint(rabbit.transform.position);
        }

        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        float speedFactor = carController.currentSpeed / carController.maxSpeed;

        float aceleration = 1f;
        float brake = 0f;
        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1f, 1f) * Mathf.Sign(carController.currentSpeed);

        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0f, 90f);
        float corenrFactor = corner / cornerDegrees;

        float distanceToNextWP = Vector3.Distance(transform.position, rabbitScript.currentWaypoynt.position);

        if (rabbitScript.currentWaypoynt.localScale.y > 1f && (distanceToNextWP * speedFactor) < prevTurnThreshold)
        {
            brake = Mathf.Lerp(0f, 1f, 1f / distanceToNextWP);
        }

        if (corner > 10f && speedFactor > 0.1f)
        {
            brake = Mathf.Lerp(0f, 1f + (speedFactor * brakingSensitivity), corenrFactor);
        }

        if (corner > 20f && speedFactor > 0.2f)
        {
            aceleration = Mathf.Lerp(0f, 1f + acelerationSensitivity, 1f - corenrFactor);
        }

        brake = Mathf.Clamp01(brake);

        carController.ForwardMovement(aceleration);
        carController.BrakeMovement(brake);
        carController.SteerMovement(steer);
    }
}