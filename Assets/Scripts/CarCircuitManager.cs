using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCircuitManager : MonoBehaviour
{
    public Circuit circuit;
    public int lastGatewayPassedIndex = 0;
    public GameObject lastGatewayPassed;

    private CarController carController;

    void Awake()
    {
        carController = GetComponent<CarController>();
    }

    void Start()
    {
        lastGatewayPassedIndex = circuit.gateWays.Count - 1;
        lastGatewayPassed = circuit.gateWays[lastGatewayPassedIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Gateway"))
        {
            int currentGateWayIndex = circuit.CarpassThroughGateway(other.gameObject, lastGatewayPassedIndex);

            if (currentGateWayIndex >= 0)
            {
                lastGatewayPassedIndex = currentGateWayIndex;
                lastGatewayPassed = other.gameObject;
            }
            else
            {
                carController.ResetCar(lastGatewayPassed);
            }
        }
    }
}