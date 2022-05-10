using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheRabbit : MonoBehaviour
{
    public WaypointsFollower rabbit;
    public Transform rabbitTransform;

    public float distanceToFollow;

    public float rotationSpeed;

    void Start()
    {
        rabbitTransform = rabbit.transform;
    }

    void LateUpdate()
    {
        float targetRotationAngle = rabbitTransform.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;

        float rotationY = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationSpeed * Time.deltaTime);
        Quaternion rotation = Quaternion.Euler(0f, rotationY, 0f);

        transform.position = rabbitTransform.position;
        transform.position -= (rotation * Vector3.forward) * distanceToFollow;

        transform.LookAt(rabbitTransform);

        Vector3 direction = rabbitTransform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        transform.Translate(0f, 0f, rabbit.speed * Time.deltaTime);
    }
}