using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    private Animator doorAnimator;
    public bool locked;
    public bool doorOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (locked == false)
        {
            doorAnimator.SetBool("IsOpen", true);
            doorOpened = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (locked == false && doorOpened == true)
        {
            doorAnimator.SetBool("IsOpen", false);
        }
    }

    void Start()
    {
        doorAnimator = this.transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
