using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mycarTarget;
    public Vector3 offset;
    public float followSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        //follow update
        Vector3 targetPosition = mycarTarget.position + 
                                 mycarTarget.forward * offset.z + 
                                 mycarTarget.right * offset.x + 
                                 ((mycarTarget.up.y >= 1f) ? mycarTarget.up * offset.y : Vector3.up * offset.y); //Evitar que la cam vaya debajo del suelo

        //transform.position = Vector3.Slerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        Vector3 smoothingVelocity = new Vector3();
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothingVelocity, followSpeed * Time.deltaTime);

        // look update
        Vector3 lookDirection = mycarTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
