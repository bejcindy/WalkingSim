using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorTrigger : MonoBehaviour
{
    public GameObject Door;
    public bool doorIsOpening;
    public float doorSpeed;
    private float doorOrigin;

    private void Start()
    {
        doorOrigin = transform.position.z;
    }
    private void Update()
    {
        if (doorIsOpening)
        {
            Door.transform.Translate(Vector3.right * Time.deltaTime * doorSpeed);
        }
        if (Door.transform.position.z > doorOrigin + 5f)
        {

        }
    }
}
