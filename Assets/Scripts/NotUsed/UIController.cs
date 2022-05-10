using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text textUISpeed;
    public Text textGearSpeed;       

    public CarController myCar;

    void Update()
    {
        textUISpeed.text = (myCar.myCarBody.velocity.magnitude * 3.6).ToString("F2");
        textGearSpeed.text = myCar.currentGear.ToString();
    }
}