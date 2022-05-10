using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [HideInInspector]
    public WheelCollider mywheelcollider;

    public Transform mymesh;
 

    //se ejecuta cuando un componente se resetea (desde la interfaz de ui por ejemplo)
    private void Reset()
    {
        mywheelcollider = GetComponent<WheelCollider>();
        //mymesh = transform.Find("Mesh");
        mymesh = transform.GetChild(0);
    }


    // Start is called before the first frame update
    void Start()
    {
        mywheelcollider.ConfigureVehicleSubsteps(5, 12, 15);
    }

    // Update is called once per frame
    void Update()
    {

        //Update visual rotation of the wheel
        Vector3 position;
        Quaternion rotation;
        mywheelcollider.GetWorldPose(out position, out rotation);
        mymesh.position = position;
        mymesh.rotation = rotation;
    }
}
