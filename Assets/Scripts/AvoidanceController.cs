using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceController : MonoBehaviour
{
    private LayerMask layerMask;

    private CarController carController;

    public float avoidanceDistance = 10f;
    public float distanceToLookPerSpeed = 1f; // meters / second
    public float timeToLaunchRays = 0.5f;
    private float timeToLaunchRaysAux = 0f;
    public float timeToAvoid = 2f; // seconds
    [HideInInspector] public float timeForAvoiding;

    private Transform mainAvoidanceRaycaster;
    private Transform leftAvoidanceRaycaster, rightAvoidanceRaycaster;

    [HideInInspector] public bool isAvoiding = false;
    [HideInInspector] public Vector3 avoidanceVector;
    [HideInInspector] public Vector3 hitPosition;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("car");

        mainAvoidanceRaycaster = transform.Find("MainAvoidanceRayCaster");
        leftAvoidanceRaycaster = transform.Find("LeftAvoidanceRayCaster");
        rightAvoidanceRaycaster = transform.Find("RightAvoidanceRayCaster");
    }

    void Update()
    {
        if (!isAvoiding)
        {
            timeToLaunchRaysAux += Time.deltaTime;

            if (timeToLaunchRaysAux >= timeToLaunchRays)
            {
                float distanceToLook = (distanceToLookPerSpeed * carController.currentSpeed) + 2f;

                Debug.DrawRay(mainAvoidanceRaycaster.position, mainAvoidanceRaycaster.forward * avoidanceDistance, Color.yellow);

                RaycastHit hit;
                if (Physics.Raycast(mainAvoidanceRaycaster.position, mainAvoidanceRaycaster.forward, out hit, avoidanceDistance, layerMask))
                {
                    Debug.DrawRay(mainAvoidanceRaycaster.position, mainAvoidanceRaycaster.forward * hit.distance, Color.red);

                    hitPosition = hit.point;
                    timeForAvoiding = Time.time + timeToAvoid;

                    avoidanceVector = Vector3.zero;
                    isAvoiding = true;
                }
                else
                {
                    hitPosition = Vector3.zero;
                }

                timeToLaunchRaysAux = 0f;
            }
        }
        else // is avoiding
        {
            if (avoidanceVector == Vector3.zero)
            {
                float distanceTolook = distanceToLookPerSpeed * carController.currentSpeed;

                // first determine the avoidance vector
                Debug.DrawRay(leftAvoidanceRaycaster.position, leftAvoidanceRaycaster.forward * distanceTolook, Color.yellow);
                Debug.DrawRay(rightAvoidanceRaycaster.position, rightAvoidanceRaycaster.forward * distanceTolook, Color.yellow);

                RaycastHit hit;
                if (Physics.Raycast(leftAvoidanceRaycaster.position, leftAvoidanceRaycaster.forward, out hit, distanceTolook, layerMask))
                {
                    // there is a car on the left, go to right
                    Debug.DrawRay(leftAvoidanceRaycaster.position, leftAvoidanceRaycaster.forward * hit.distance, Color.red);
                    avoidanceVector = new Vector3(avoidanceDistance, 0f, 0f);
                }
                else if (Physics.Raycast(rightAvoidanceRaycaster.position, rightAvoidanceRaycaster.forward, out hit, avoidanceDistance, layerMask))
                {
                    // there is a car on the right, go to left
                    Debug.DrawRay(rightAvoidanceRaycaster.position, rightAvoidanceRaycaster.forward * hit.distance, Color.red);
                    avoidanceVector = new Vector3(-avoidanceDistance, 0f, 0f);

                }
                else
                {
                    // car not found on the left or in the right ¿supose to be in full front?
                    avoidanceVector = new Vector3(-avoidanceDistance, 0f, 0f);
                }
            }
            else
            {
                if(Time.time > timeForAvoiding)
                {
                    // stop avoiding
                    avoidanceVector = Vector3.zero;
                    isAvoiding = false;
                }
            }
        }
    }
}