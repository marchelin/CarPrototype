using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Circuit circuit;
    private CarController carController;

    public float minAceleration = 0.33f;
    public float brakingSensitivity = 0.1f;
    public float minSpeedToBreak = 8f;
    public float steeringSensitivity = 0.01f;
    private Vector3 actualWP_pos, nextWP_pos; // targetWP, nextTargetWP
    public int actualWaypointIndex = 0;

    public float distanceToNextWP_Threshold = 4f;
    public float distanceToAdvanceBreak = 10f;
    private float currentSectioniDistance;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        actualWP_pos = circuit.waypoints[actualWaypointIndex].transform.position;
        nextWP_pos = circuit.waypoints[(actualWaypointIndex + 1) % circuit.waypoints.Length].transform.position;
        
        //cu
    }

    void Update()
    {
        Debug.DrawLine(transform.position, actualWP_pos, Color.blue);
    }

    void FixedUpdate()
    {
        actualWP_pos = circuit.waypoints[actualWaypointIndex].transform.position;
        nextWP_pos = circuit.waypoints[(actualWaypointIndex + 1) % circuit.waypoints.Length].transform.position;

        Vector3 localTarget = transform.InverseTransformPoint(actualWP_pos);
        Vector3 nextLocalTarget = transform.InverseTransformPoint(nextWP_pos);

        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        float nextTargetAngle = Mathf.Atan2(nextLocalTarget.x, nextLocalTarget.z) * Mathf.Rad2Deg;
        float distanceToNextWP = Vector3.Distance(actualWP_pos, transform.position);

        float currentSectionFactor = distanceToNextWP / currentSectioniDistance;
        float speedFactor = carController.currentSpeed / carController.maxSpeed;

        float aceleration = 1f;
        float brake = 0f;
        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1f, 1f) * Mathf.Sign(carController.currentSpeed);


        aceleration = Mathf.Lerp(minAceleration, 1f, distanceToNextWP_Threshold);

        if (currentSectionFactor < 0.5f && carController.currentSpeed > minSpeedToBreak)
        {
            //brake = Mathf.Lerp(0f, 1f, 1f - currentSectionFactor );
            brake = Mathf.Lerp((-1f - Mathf.Abs(nextTargetAngle)) * brakingSensitivity, 1f + speedFactor, 1f - distanceToNextWP_Threshold);
        }

        brake = Mathf.Clamp01(brake);

        carController.ForwardMovement(aceleration);
        carController.BrakeMovement(brake);
        carController.SteerMovement(steer);

        if(distanceToNextWP < distanceToNextWP_Threshold)
        {
            actualWaypointIndex = (actualWaypointIndex + 1) % circuit.waypoints.Length;
            actualWP_pos = circuit.waypoints[actualWaypointIndex].transform.position;
            nextWP_pos = circuit.waypoints[(actualWaypointIndex + 1) % circuit.waypoints.Length].transform.position;

            currentSectioniDistance = Vector3.Distance(nextWP_pos, transform.position);
        }
    }
}
