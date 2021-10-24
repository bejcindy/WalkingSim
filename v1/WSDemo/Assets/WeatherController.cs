using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public Animator weatherAnimator;
    public bool weatherPopped = false;
    private void OnTriggerEnter(Collider other)
    {
        weatherAnimator.SetBool("IsOpen", true);
        weatherPopped = true;
    }

    public void HideWeather()
    {
        weatherAnimator.SetBool("IsOpen", false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (weatherPopped == true)
        {
            HideWeather();
        }
    }
    void Start()
    {
        weatherAnimator = this.transform.parent.GetComponent<Animator>();
    }
}
