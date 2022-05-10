using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public CarController carController;
    public Rigidbody carRb;
    public AudioSource carAudio;

    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        currentSpeed = carController.myCarBody.velocity.magnitude * 5;
        pitchFromCar = carRb.velocity.magnitude / 40f;

        if(currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if(currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
