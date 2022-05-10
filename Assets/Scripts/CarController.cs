using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarController : MonoBehaviour
{
    [SerializeField] public WheelController[] frontWheels;
    [SerializeField] public WheelController[] backWheels;
    [SerializeField]  public WheelController[] wheels;

    public enum tractionType
    {
        RearWD,
        FrontWD,
    }

    [Header("Car Settings")]
    public tractionType traction = tractionType.RearWD;
    public float torque = 300f;
    public float maxAngleSteer = 30f;
    public float braketorque = 500f;
    public float velocity = 0;
    [HideInInspector]
    public Rigidbody myCarBody;
    public float currentSpeed = 100f;
    public float maxSpeed = 100f;

    [Header("Car Extras")]
    public float skidOffset = 0.8f;
    public Transform skidPrefab;
    public Transform[] skidTrails;
    public Transform smokePartyclePrefab;
    public Transform[] smokePartycleTrails;
    public GameObject[] breakLights;

    [Header("Car AudioSettings")]
    public AudioSource skidAudio;
    public float gearLenght = 3f;
    public float currentGearSpeed { get { return myCarBody.velocity.magnitude * gearLenght; } }
    public float maxGearSpeed = 300f;
    public int numGears = 5;
    private float gearProp;
    public float rpm;
    [HideInInspector]
    public int currentGear = 1;
    private float currentGearProp;
    public float lowMotorAudioPitch = 1f;
    public float highMotorAudioPitch = 3f;
    public AudioSource engineSound;

    public AnimationCurve enginePitchCurve;

    void Start()
    {
        skidTrails = new Transform[wheels.Length];
        smokePartycleTrails = new Transform[wheels.Length];

        gearProp = 1f / numGears;
    }

    void Update()
    {
        CalculateEngineSound();
    }

    void FixedUpdate()
    {
        CheckWheelSkid();
    }

    public void ResetCar(GameObject gateWay)
    {
        myCarBody.velocity = Vector3.zero;
        myCarBody.angularVelocity = Vector3.zero;

        foreach (WheelController wheel in wheels)
        {
            wheel.mywheelcollider.brakeTorque = Mathf.Infinity;
        }

        transform.position = new Vector3(gateWay.transform.position.x, gateWay.transform.position.y, transform.position.z);
        transform.rotation = Quaternion.LookRotation(gateWay.transform.forward);
    }

    public void ForwardMovement(float torqueInput)
    {
        float thrustTorque = (myCarBody.velocity.magnitude < maxSpeed) ? torqueInput * torque : 0f;

        switch (traction)
        {
            case tractionType.RearWD:
                foreach (WheelController wheel in backWheels)
                {
                    wheel.mywheelcollider.motorTorque = thrustTorque;
                }
                break;

            case tractionType.FrontWD:
                foreach (WheelController wheel in frontWheels)
                {
                    wheel.mywheelcollider.motorTorque = thrustTorque;
                }
                break;

            default: break;
        }

        foreach (WheelController wheel in wheels)
        {
            wheel.mywheelcollider.motorTorque = torqueInput * torque;
        }
    }

    public void BrakeMovement(float brakeInput)
    {
        foreach (WheelController wheel in wheels)
        {
            wheel.mywheelcollider.brakeTorque = brakeInput * braketorque;
        }

        if (brakeInput > float.Epsilon)
        {
            foreach (GameObject brakeLightGO in breakLights) { brakeLightGO.SetActive(true); }
        }
        else
        {
            foreach (GameObject brakeLightGO in breakLights) { brakeLightGO.SetActive(false); }
        }
    }

    public void SteerMovement(float steerInput)
    {
        foreach (WheelController wheel in frontWheels)
        {
            wheel.mywheelcollider.steerAngle = steerInput * maxAngleSteer;
        }
    }

    private void CheckWheelSkid()
    {
        int wheelsSkidding = 0;

        for (int i = 0; i < wheels.Length; i++)
        {
            WheelHit wheelHit;
            wheels[i].mywheelcollider.GetGroundHit(out wheelHit);

            if (Mathf.Abs(wheelHit.forwardSlip) >= skidOffset || Mathf.Abs(wheelHit.sidewaysSlip) >= skidOffset)
            {
                wheelsSkidding++;
                StartSkidTrail(i);
                StartSmokeTrail(i);
            }
            else
            {
                EndSkidTrail(i);
                EndSmokeTrail(i);
            }
        }

        if(wheelsSkidding > 0)
        {
            skidAudio.volume = wheelsSkidding / (float)wheels.Length;

            if (!skidAudio.isPlaying)
            {
                skidAudio.Play();
            }
            else if (skidAudio.isPlaying)
            {
                skidAudio.Stop();
            }
        }
    }

    private void StartSkidTrail(int i)
    {
        if (skidTrails[i] == null)
        {
            skidTrails[i] = Instantiate(skidPrefab);
        }

        skidTrails[i].parent = wheels[i].transform;
        skidTrails[i].localRotation = Quaternion.Euler(90f, 0f, 0f);
        skidTrails[i].localPosition = -Vector3.up * wheels[i].mywheelcollider.radius;
    }
    private void EndSkidTrail(int i) 
    {
        if (skidTrails[i] == null)
            return;

        Transform skidTrail = skidTrails[i];
        skidTrails[i] = null;
        skidTrail.parent = null;
        skidTrail.rotation = Quaternion.Euler(90f, 0f, 0f);
        Destroy(skidTrail.gameObject, 30);
    }

    private void StartSmokeTrail(int i)
    {
        if (smokePartycleTrails[i] == null)
        {
            smokePartycleTrails[i] = Instantiate(smokePartyclePrefab);
        }

        smokePartycleTrails[i].parent = wheels[i].transform;
        smokePartycleTrails[i].localRotation = Quaternion.Euler(-90f, 0f, 0f);
        smokePartycleTrails[i].localPosition = -Vector3.up * wheels[i].mywheelcollider.radius;
    }
    private void EndSmokeTrail(int i)
    {
        if (smokePartycleTrails[i] == null)
        {
            return;
        }

        Transform smokeTrail = smokePartycleTrails[i];
        smokePartycleTrails[i] = null;
        smokeTrail.parent = null;
        smokeTrail.rotation = Quaternion.Euler(90f, 0f, 0f);
        Destroy(smokeTrail.gameObject, 5);
    }

    private void CalculateEngineSound()
    {
        float speedProp = currentGearSpeed / maxGearSpeed;
        float targetGearFactor = Mathf.InverseLerp(gearProp * currentGear, gearProp * (currentGear + 1), speedProp);
        currentGearProp = Mathf.Lerp(currentGearProp, targetGearFactor, Time.deltaTime * 5f);

        float gearNumFactor = currentGear / (float)numGears;
        rpm = Mathf.Lerp(gearNumFactor, 1, currentGearProp);

        float upperGearMax = gearProp * (currentGear + 1);
        float downGearMax = gearProp * currentGear;

        if (currentGear > 0 && speedProp < downGearMax)
            currentGear--;

        if (currentGear < (numGears - 1) && speedProp > upperGearMax)
            currentGear++;

        engineSound.pitch = Mathf.Lerp(lowMotorAudioPitch, highMotorAudioPitch, rpm * 7.8f);
        engineSound.pitch = (enginePitchCurve.Evaluate(speedProp) * highMotorAudioPitch) + lowMotorAudioPitch * 0.5f;
    }
}