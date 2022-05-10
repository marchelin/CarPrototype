using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsFollower : MonoBehaviour
{
    public Circuit circuit;

    public Transform currentWaypoynt;

    private int currentWaypoint = 0;

    private Vector3 nextWP_pos;

    public float distanceToWpThreshold = 1f;

    public float speed;
    public float rotationSpeed;

    void Start()
    {
        nextWP_pos = circuit.waypoints[currentWaypoint].transform.position;
    }

    void Update()
    {
        float distanceToWP = Vector3.Distance(transform.position, nextWP_pos);

        Vector3 direction = nextWP_pos - transform.position;

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        transform.LookAt(nextWP_pos);
        transform.Translate(0f, 0f, (speed / 6.85f) * Time.deltaTime);

        if (distanceToWP <= distanceToWpThreshold)
        {
            currentWaypoint = (currentWaypoint + 1) % circuit.waypoints.Length;
            nextWP_pos = circuit.waypoints[currentWaypoint].transform.position;
        }
    }
}