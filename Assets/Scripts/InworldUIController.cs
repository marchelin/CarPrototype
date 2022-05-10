using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InworldUIController : MonoBehaviour
{
    public Text Speed_text;
    //public Text Gear_text;
    public Text RPM_text;

    public CarController carController;
    public GameObject SpeedParticles;

    private float r_p_m;

    void Update()
    {
        r_p_m = carController.rpm * 10;

        Speed_text.text = (carController.myCarBody.velocity.magnitude * 5).ToString("F2");
        //Gear_text.text = carController.currentGear.ToString();
        RPM_text.text = r_p_m.ToString();

        SpeedParticlesFX();
    }

    private void SpeedParticlesFX()
    {
        if ((carController.myCarBody.velocity.magnitude * 5) > 150)
        {
            //SpeedParticles.Play();
            SpeedParticles.SetActive(true); // es más responsive con esto

        }
        else
        {
            //SpeedParticles.Stop();
            SpeedParticles.SetActive(false); // es más responsive con esto
        }
    }
}